namespace OneCardSln.Components.Serialize.Protobuf.Meta
{
    using OneCardSln.Components.Serialize.Protobuf.Compiler;
    using OneCardSln.Components.Serialize.Protobuf.Protobuf;
    using OneCardSln.Components.Serialize.Protobuf.Serializers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public sealed class RuntimeTypeModel : TypeModel
    {
        private BasicList basicTypes = new BasicList();
        private int contentionCounter = 1;
        private MethodInfo defaultFactory;
        private const int KnownTypes_Array = 1;
        private const int KnownTypes_ArrayCutoff = 20;
        private const int KnownTypes_Dictionary = 2;
        private const int KnownTypes_Hashtable = 3;
        private int metadataTimeoutMilliseconds = 0x1388;
        private byte options;
        private const byte OPTIONS_AllowParseableTypes = 0x40;
        private const byte OPTIONS_AutoAddMissingTypes = 8;
        private const byte OPTIONS_AutoAddProtoContractTypesOnly = 0x80;
        private const byte OPTIONS_AutoCompile = 0x10;
        private const byte OPTIONS_Frozen = 4;
        private const byte OPTIONS_InferTagFromNameDefault = 1;
        private const byte OPTIONS_IsDefaultModel = 2;
        private const byte OPTIONS_UseImplicitZeroDefaults = 0x20;
        private readonly BasicList types = new BasicList();

        public event LockContentedEventHandler LockContended;

        internal RuntimeTypeModel(bool isDefault)
        {
            this.AutoAddMissingTypes = true;
            this.UseImplicitZeroDefaults = true;
            this.SetOption(2, isDefault);
            this.AutoCompile = true;
        }

        public MetaType Add(Type type, bool applyDefaultBehaviour)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            MetaType type2 = this.FindWithoutAdd(type);
            if (type2 == null)
            {
                int opaqueToken = 0;
                if ((type.IsInterface && base.MapType(MetaType.ienumerable).IsAssignableFrom(type)) && (TypeModel.GetListItemType(this, type) == null))
                {
                    throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
                }
                try
                {
                    type2 = this.RecogniseCommonTypes(type);
                    if (type2 != null)
                    {
                        if (!applyDefaultBehaviour)
                        {
                            throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.FullName, "applyDefaultBehaviour");
                        }
                        applyDefaultBehaviour = false;
                    }
                    if (type2 == null)
                    {
                        type2 = this.Create(type);
                    }
                    type2.Pending = true;
                    this.TakeLock(ref opaqueToken);
                    if (this.FindWithoutAdd(type) != null)
                    {
                        throw new ArgumentException("Duplicate type", "type");
                    }
                    this.ThrowIfFrozen();
                    this.types.Add(type2);
                    if (applyDefaultBehaviour)
                    {
                        type2.ApplyDefaultBehaviour();
                    }
                    type2.Pending = false;
                }
                finally
                {
                    this.ReleaseLock(opaqueToken);
                }
            }
            return type2;
        }

        private void AddContention()
        {
            Interlocked.Increment(ref this.contentionCounter);
        }

        private void BuildAllSerializers()
        {
            for (int i = 0; i < this.types.Count; i++)
            {
                MetaType type = (MetaType) this.types[i];
                if (type.Serializer == null)
                {
                    throw new InvalidOperationException("No serializer available for " + type.Type.Name);
                }
            }
        }

        private void CascadeDependents(BasicList list, MetaType metaType)
        {
            MetaType surrogateOrBaseOrSelf;
            if (metaType.IsList)
            {
                WireType type3;
                Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
                if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out type3, false, false, false, false) == null)
                {
                    int num = this.FindOrAddAuto(listItemType, false, false, false);
                    if (num >= 0)
                    {
                        surrogateOrBaseOrSelf = ((MetaType) this.types[num]).GetSurrogateOrBaseOrSelf(false);
                        if (!list.Contains(surrogateOrBaseOrSelf))
                        {
                            list.Add(surrogateOrBaseOrSelf);
                            this.CascadeDependents(list, surrogateOrBaseOrSelf);
                        }
                    }
                }
            }
            else
            {
                if (metaType.IsAutoTuple)
                {
                    MemberInfo[] infoArray;
                    if (MetaType.ResolveTupleConstructor(metaType.Type, out infoArray) != null)
                    {
                        for (int i = 0; i < infoArray.Length; i++)
                        {
                            WireType type5;
                            Type type = null;
                            if (infoArray[i] is PropertyInfo)
                            {
                                type = ((PropertyInfo) infoArray[i]).PropertyType;
                            }
                            else if (infoArray[i] is FieldInfo)
                            {
                                type = ((FieldInfo) infoArray[i]).FieldType;
                            }
                            if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out type5, false, false, false, false) == null)
                            {
                                int num3 = this.FindOrAddAuto(type, false, false, false);
                                if (num3 >= 0)
                                {
                                    surrogateOrBaseOrSelf = ((MetaType) this.types[num3]).GetSurrogateOrBaseOrSelf(false);
                                    if (!list.Contains(surrogateOrBaseOrSelf))
                                    {
                                        list.Add(surrogateOrBaseOrSelf);
                                        this.CascadeDependents(list, surrogateOrBaseOrSelf);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (ValueMember member in metaType.Fields)
                    {
                        WireType type7;
                        Type itemType = member.ItemType;
                        if (itemType == null)
                        {
                            itemType = member.MemberType;
                        }
                        if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, itemType, out type7, false, false, false, false) == null)
                        {
                            int num4 = this.FindOrAddAuto(itemType, false, false, false);
                            if (num4 >= 0)
                            {
                                surrogateOrBaseOrSelf = ((MetaType) this.types[num4]).GetSurrogateOrBaseOrSelf(false);
                                if (!list.Contains(surrogateOrBaseOrSelf))
                                {
                                    list.Add(surrogateOrBaseOrSelf);
                                    this.CascadeDependents(list, surrogateOrBaseOrSelf);
                                }
                            }
                        }
                    }
                }
                if (metaType.HasSubtypes)
                {
                    foreach (SubType type8 in metaType.GetSubtypes())
                    {
                        surrogateOrBaseOrSelf = type8.DerivedType.GetSurrogateOrSelf();
                        if (!list.Contains(surrogateOrBaseOrSelf))
                        {
                            list.Add(surrogateOrBaseOrSelf);
                            this.CascadeDependents(list, surrogateOrBaseOrSelf);
                        }
                    }
                }
                surrogateOrBaseOrSelf = metaType.BaseType;
                if (surrogateOrBaseOrSelf != null)
                {
                    surrogateOrBaseOrSelf = surrogateOrBaseOrSelf.GetSurrogateOrSelf();
                }
                if ((surrogateOrBaseOrSelf != null) && !list.Contains(surrogateOrBaseOrSelf))
                {
                    list.Add(surrogateOrBaseOrSelf);
                    this.CascadeDependents(list, surrogateOrBaseOrSelf);
                }
            }
        }

        public TypeModel Compile()
        {
            return this.Compile(null, null);
        }

        public TypeModel Compile(CompilerOptions options)
        {
            string str3;
            string str4;
            int num;
            bool flag2;
            SerializerPair[] pairArray;
            CompilerContext.ILVersion version;
            ILGenerator generator;
            int num2;
            FieldBuilder builder4;
            Type type;
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            string typeName = options.TypeName;
            string outputPath = options.OutputPath;
            this.BuildAllSerializers();
            this.Freeze();
            bool flag = !Helpers.IsNullOrEmpty(outputPath);
            if (Helpers.IsNullOrEmpty(typeName))
            {
                if (flag)
                {
                    throw new ArgumentNullException("typeName");
                }
                typeName = Guid.NewGuid().ToString();
            }
            if (outputPath == null)
            {
                str3 = typeName;
                str4 = str3 + ".dll";
            }
            else
            {
                str3 = new FileInfo(Path.GetFileNameWithoutExtension(outputPath)).Name;
                str4 = str3 + Path.GetExtension(outputPath);
            }
            AssemblyName name = new AssemblyName {
                Name = str3
            };
            AssemblyBuilder asm = AppDomain.CurrentDomain.DefineDynamicAssembly(name, flag ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
            ModuleBuilder module = flag ? asm.DefineDynamicModule(str4, outputPath) : asm.DefineDynamicModule(str4);
            this.WriteAssemblyAttributes(options, str3, asm);
            TypeBuilder builder3 = this.WriteBasicTypeModel(options, typeName, module);
            this.WriteSerializers(options, str3, builder3, out num, out flag2, out pairArray, out version);
            this.WriteGetKeyImpl(builder3, flag2, pairArray, version, out generator, out num2, out builder4, out type);
            CompilerContext ctx = this.WriteSerializeDeserialize(str3, builder3, pairArray, version, ref generator);
            this.WriteConstructors(builder3, ref num, pairArray, ref generator, num2, builder4, type, ctx);
            Type type2 = builder3.CreateType();
            if (!Helpers.IsNullOrEmpty(outputPath))
            {
                asm.Save(outputPath);
            }
            return (TypeModel) Activator.CreateInstance(type2);
        }

        public TypeModel Compile(string name, string path)
        {
            CompilerOptions options = new CompilerOptions {
                TypeName = name,
                OutputPath = path
            };
            return this.Compile(options);
        }

        public void CompileInPlace()
        {
            BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ((MetaType) enumerator.Current).CompileInPlace();
            }
        }

        private MetaType Create(Type type)
        {
            this.ThrowIfFrozen();
            return new MetaType(this, type, this.defaultFactory);
        }

        protected internal override object Deserialize(int key, object value, ProtoReader source)
        {
            IProtoSerializer serializer = (IProtoSerializer)((MetaType)this.types[key]).Serializer;
            if (((value == null) && Helpers.IsValueType(serializer.ExpectedType)) && serializer.RequiresOldValue)
            {
                value = Activator.CreateInstance(serializer.ExpectedType);
            }
            return serializer.Read(value, source);
        }

        private static MethodBuilder EmitBoxedSerializer(TypeBuilder type, int i, Type valueType, SerializerPair[] methodPairs, TypeModel model, CompilerContext.ILVersion ilVersion, string assemblyName)
        {
            MethodInfo deserialize = methodPairs[i].Deserialize;
            MethodBuilder builder = type.DefineMethod("_" + i.ToString(), MethodAttributes.Static, CallingConventions.Standard, model.MapType(typeof(object)), new Type[] { model.MapType(typeof(object)), model.MapType(typeof(ProtoReader)) });
            CompilerContext ctx = new CompilerContext(builder.GetILGenerator(), true, false, methodPairs, model, ilVersion, assemblyName, model.MapType(typeof(object)));
            ctx.LoadValue(ctx.InputValue);
            CodeLabel label = ctx.DefineLabel();
            ctx.BranchIfFalse(label, true);
            Type type2 = valueType;
            ctx.LoadValue(ctx.InputValue);
            ctx.CastFromObject(type2);
            ctx.LoadReaderWriter();
            ctx.EmitCall(deserialize);
            ctx.CastToObject(type2);
            ctx.Return();
            ctx.MarkLabel(label);
            using (Local local = new Local(ctx, type2))
            {
                ctx.LoadAddress(local, type2);
                ctx.EmitCtor(type2);
                ctx.LoadValue(local);
                ctx.LoadReaderWriter();
                ctx.EmitCall(deserialize);
                ctx.CastToObject(type2);
                ctx.Return();
            }
            return builder;
        }

        internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
        {
            MetaType type2;
            MetaTypeFinder predicate = new MetaTypeFinder(type);
            int index = this.types.IndexOf(predicate);
            if (index >= 0)
            {
                type2 = (MetaType) this.types[index];
                if (type2.Pending)
                {
                    this.WaitOnLock(type2);
                }
                return index;
            }
            bool flag = this.AutoAddMissingTypes || addEvenIfAutoDisabled;
            if (!Helpers.IsEnum(type) && (this.TryGetBasicTypeSerializer(type) != null))
            {
                if (flag && !addWithContractOnly)
                {
                    throw MetaType.InbuiltType(type);
                }
                return -1;
            }
            Type type3 = TypeModel.ResolveProxies(type);
            if (type3 != null)
            {
                predicate = new MetaTypeFinder(type3);
                index = this.types.IndexOf(predicate);
                type = type3;
            }
            if (index < 0)
            {
                int opaqueToken = 0;
                try
                {
                    this.TakeLock(ref opaqueToken);
                    type2 = this.RecogniseCommonTypes(type);
                    if (type2 == null)
                    {
                        MetaType.AttributeFamily family = MetaType.GetContractFamily(this, type, null);
                        if (family == MetaType.AttributeFamily.AutoTuple)
                        {
                            flag = addEvenIfAutoDisabled = true;
                        }
                        if (!flag || ((!Helpers.IsEnum(type) && addWithContractOnly) && (family == MetaType.AttributeFamily.None)))
                        {
                            if (demand)
                            {
                                TypeModel.ThrowUnexpectedType(type);
                            }
                            return index;
                        }
                        type2 = this.Create(type);
                    }
                    type2.Pending = true;
                    bool flag2 = false;
                    int num3 = this.types.IndexOf(predicate);
                    if (num3 < 0)
                    {
                        this.ThrowIfFrozen();
                        index = this.types.Add(type2);
                        flag2 = true;
                    }
                    else
                    {
                        index = num3;
                    }
                    if (flag2)
                    {
                        type2.ApplyDefaultBehaviour();
                        type2.Pending = false;
                    }
                }
                finally
                {
                    this.ReleaseLock(opaqueToken);
                }
            }
            return index;
        }

        internal MetaType FindWithoutAdd(Type type)
        {
            BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
            while (enumerator.MoveNext())
            {
                MetaType current = (MetaType) enumerator.Current;
                if (current.Type == type)
                {
                    if (current.Pending)
                    {
                        this.WaitOnLock(current);
                    }
                    return current;
                }
            }
            Type type3 = TypeModel.ResolveProxies(type);
            if (type3 != null)
            {
                return this.FindWithoutAdd(type3);
            }
            return null;
        }

        public void Freeze()
        {
            if (this.GetOption(2))
            {
                throw new InvalidOperationException("The default model cannot be frozen");
            }
            this.SetOption(4, true);
        }

        private int GetContention()
        {
            return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
        }

        internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
        {
            int num = this.FindOrAddAuto(type, false, false, false);
            if (num >= 0)
            {
                return ((MetaType) this.types[num]).GetEnumMap();
            }
            return null;
        }

        internal int GetKey(Type type, bool demand, bool getBaseKey)
        {
            int num2;
            try
            {
                int num = this.FindOrAddAuto(type, demand, true, false);
                if (num >= 0)
                {
                    MetaType source = (MetaType) this.types[num];
                    if (getBaseKey)
                    {
                        source = MetaType.GetRootType(source);
                        num = this.FindOrAddAuto(source.Type, true, true, false);
                    }
                }
                num2 = num;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                if (exception.Message.IndexOf(type.FullName) >= 0)
                {
                    throw;
                }
                throw new ProtoException(exception.Message + " (" + type.FullName + ")", exception);
            }
            return num2;
        }

        protected override int GetKeyImpl(Type type)
        {
            return this.GetKey(type, false, true);
        }

        private bool GetOption(byte option)
        {
            return ((this.options & option) == option);
        }

        public override string GetSchema(Type type)
        {
            BasicList list = new BasicList();
            MetaType type2 = null;
            bool flag = false;
            if (type == null)
            {
                BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    MetaType surrogateOrBaseOrSelf = ((MetaType) enumerator.Current).GetSurrogateOrBaseOrSelf(false);
                    if (!list.Contains(surrogateOrBaseOrSelf))
                    {
                        list.Add(surrogateOrBaseOrSelf);
                        this.CascadeDependents(list, surrogateOrBaseOrSelf);
                    }
                }
            }
            else
            {
                WireType type6;
                Type underlyingType = Helpers.GetUnderlyingType(type);
                if (underlyingType != null)
                {
                    type = underlyingType;
                }
                flag = ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out type6, false, false, false, false) != null;
                if (!flag)
                {
                    int num = this.FindOrAddAuto(type, false, false, false);
                    if (num < 0)
                    {
                        throw new ArgumentException("The type specified is not a contract-type", "type");
                    }
                    type2 = ((MetaType) this.types[num]).GetSurrogateOrBaseOrSelf(false);
                    list.Add(type2);
                    this.CascadeDependents(list, type2);
                }
            }
            StringBuilder builder = new StringBuilder();
            string str = null;
            if (!flag)
            {
                IEnumerable enumerable = (type2 == null) ? this.types : list;
                foreach (MetaType type7 in enumerable)
                {
                    if (!type7.IsList)
                    {
                        string str2 = type7.Type.Namespace;
                        if (!Helpers.IsNullOrEmpty(str2) && !str2.StartsWith("System."))
                        {
                            if (str == null)
                            {
                                str = str2;
                            }
                            else if (!(str == str2))
                            {
                                str = null;
                                break;
                            }
                        }
                    }
                }
            }
            if (!Helpers.IsNullOrEmpty(str))
            {
                builder.Append("package ").Append(str).Append(';');
                Helpers.AppendLine(builder);
            }
            bool requiresBclImport = false;
            StringBuilder builder2 = new StringBuilder();
            MetaType[] array = new MetaType[list.Count];
            list.CopyTo(array, 0);
            Array.Sort<MetaType>(array, MetaType.Comparer.Default);
            if (flag)
            {
                Helpers.AppendLine(builder2).Append("message ").Append(type.Name).Append(" {");
                MetaType.NewLine(builder2, 1).Append("optional ").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref requiresBclImport)).Append(" value = 1;");
                Helpers.AppendLine(builder2).Append('}');
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    MetaType type8 = array[i];
                    if (!type8.IsList || (type8 == type2))
                    {
                        type8.WriteSchema(builder2, 0, ref requiresBclImport);
                    }
                }
            }
            if (requiresBclImport)
            {
                builder.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
                Helpers.AppendLine(builder);
            }
            return Helpers.AppendLine(builder.Append(builder2)).ToString();
        }

        internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
        {
            WireType type2;
            Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
            if (underlyingType != null)
            {
                effectiveType = underlyingType;
            }
            if (effectiveType == base.MapType(typeof(byte[])))
            {
                return "bytes";
            }
            IProtoSerializer serializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out type2, false, false, false, false);
            if (serializer == null)
            {
                if (!asReference && !dynamicType)
                {
                    return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
                }
                requiresBclImport = true;
                return "bcl.NetObjectProxy";
            }
            if (serializer is ParseableSerializer)
            {
                if (asReference)
                {
                    requiresBclImport = true;
                }
                if (!asReference)
                {
                    return "string";
                }
                return "bcl.NetObjectProxy";
            }
            switch (Helpers.GetTypeCode(effectiveType))
            {
                case ProtoTypeCode.Boolean:
                    return "bool";

                case ProtoTypeCode.Char:
                case ProtoTypeCode.Byte:
                case ProtoTypeCode.UInt16:
                case ProtoTypeCode.UInt32:
                    if (dataFormat != DataFormat.FixedSize)
                    {
                        return "uint32";
                    }
                    return "fixed32";

                case ProtoTypeCode.SByte:
                case ProtoTypeCode.Int16:
                case ProtoTypeCode.Int32:
                    switch (dataFormat)
                    {
                        case DataFormat.ZigZag:
                            return "sint32";

                        case DataFormat.FixedSize:
                            return "sfixed32";
                    }
                    break;

                case ProtoTypeCode.Int64:
                    switch (dataFormat)
                    {
                        case DataFormat.ZigZag:
                            return "sint64";

                        case DataFormat.FixedSize:
                            return "sfixed64";
                    }
                    return "int64";

                case ProtoTypeCode.UInt64:
                    if (dataFormat != DataFormat.FixedSize)
                    {
                        return "uint64";
                    }
                    return "fixed64";

                case ProtoTypeCode.Single:
                    return "float";

                case ProtoTypeCode.Double:
                    return "double";

                case ProtoTypeCode.Decimal:
                    requiresBclImport = true;
                    return "bcl.Decimal";

                case ProtoTypeCode.DateTime:
                    requiresBclImport = true;
                    return "bcl.DateTime";

                case ProtoTypeCode.String:
                    if (asReference)
                    {
                        requiresBclImport = true;
                    }
                    if (!asReference)
                    {
                        return "string";
                    }
                    return "bcl.NetObjectProxy";

                case ProtoTypeCode.TimeSpan:
                    requiresBclImport = true;
                    return "bcl.TimeSpan";

                case ProtoTypeCode.Guid:
                    requiresBclImport = true;
                    return "bcl.Guid";

                default:
                    throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
            }
            return "int32";
        }

        internal ProtoSerializer GetSerializer(IProtoSerializer serializer, bool compiled)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            if (compiled)
            {
                return CompilerContext.BuildSerializer(serializer, this);
            }
            return new ProtoSerializer(serializer.Write);
        }

        public IEnumerable GetTypes()
        {
            return this.types;
        }

        internal bool IsPrepared(Type type)
        {
            MetaType type2 = this.FindWithoutAdd(type);
            return ((type2 != null) && type2.IsPrepared());
        }

        private static ILGenerator Override(TypeBuilder type, string name)
        {
            MethodInfo method = type.BaseType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            ParameterInfo[] parameters = method.GetParameters();
            Type[] parameterTypes = new Type[parameters.Length];
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                parameterTypes[i] = parameters[i].ParameterType;
            }
            MethodBuilder methodInfoBody = type.DefineMethod(method.Name, (method.Attributes & ~MethodAttributes.Abstract) | MethodAttributes.Final, method.CallingConvention, method.ReturnType, parameterTypes);
            ILGenerator iLGenerator = methodInfoBody.GetILGenerator();
            type.DefineMethodOverride(methodInfoBody, method);
            return iLGenerator;
        }

        private MetaType RecogniseCommonTypes(Type type)
        {
            return null;
        }

        internal void ReleaseLock(int opaqueToken)
        {
            if (opaqueToken != 0)
            {
                Monitor.Exit(this.types);
                if (opaqueToken != this.GetContention())
                {
                    LockContentedEventHandler lockContended = this.LockContended;
                    if (lockContended != null)
                    {
                        string stackTrace;
                        try
                        {
                            throw new ProtoException();
                        }
                        catch (Exception exception)
                        {
                            stackTrace = exception.StackTrace;
                        }
                        lockContended(this, new LockContentedEventArgs(stackTrace));
                    }
                }
            }
        }

        internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
        {
            if (((type != null) && (Helpers.GetTypeCode(type) == ProtoTypeCode.Unknown)) && !this[type].IgnoreListHandling)
            {
                if (type.IsArray)
                {
                    if (type.GetArrayRank() != 1)
                    {
                        throw new NotSupportedException("Multi-dimension arrays are supported");
                    }
                    itemType = type.GetElementType();
                    if (itemType == base.MapType(typeof(byte)))
                    {
                        defaultType = (Type) (itemType = null);
                    }
                    else
                    {
                        defaultType = type;
                    }
                }
                if (itemType == null)
                {
                    itemType = TypeModel.GetListItemType(this, type);
                }
                if (itemType != null)
                {
                    Type type2 = null;
                    Type type3 = null;
                    this.ResolveListTypes(itemType, ref type2, ref type3);
                    if (type2 != null)
                    {
                        throw TypeModel.CreateNestedListsNotSupported();
                    }
                }
                if ((itemType != null) && (defaultType == null))
                {
                    if ((type.IsClass && !type.IsAbstract) && (Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != null))
                    {
                        defaultType = type;
                    }
                    if ((defaultType == null) && type.IsInterface)
                    {
                        Type[] typeArray;
                        if ((type.IsGenericType && (type.GetGenericTypeDefinition() == base.MapType(typeof(IDictionary<,>)))) && (itemType == base.MapType(typeof(KeyValuePair<,>)).MakeGenericType(typeArray = type.GetGenericArguments())))
                        {
                            defaultType = base.MapType(typeof(Dictionary<,>)).MakeGenericType(typeArray);
                        }
                        else
                        {
                            defaultType = base.MapType(typeof(List<>)).MakeGenericType(new Type[] { itemType });
                        }
                    }
                    if ((defaultType != null) && !Helpers.IsAssignableFrom(type, defaultType))
                    {
                        defaultType = null;
                    }
                }
            }
        }

        protected internal override void Serialize(int key, object value, ProtoWriter dest)
        {
            ((MetaType) this.types[key]).Serializer.Write(value, dest);
        }

        public void SetDefaultFactory(MethodInfo methodInfo)
        {
            this.VerifyFactory(methodInfo, null);
            this.defaultFactory = methodInfo;
        }

        private void SetOption(byte option, bool value)
        {
            if (value)
            {
                this.options = (byte) (this.options | option);
            }
            else
            {
                this.options = (byte) (this.options & ~option);
            }
        }

        internal void TakeLock(ref int opaqueToken)
        {
            opaqueToken = 0;
            if (Monitor.TryEnter(this.types, this.metadataTimeoutMilliseconds))
            {
                opaqueToken = this.GetContention();
            }
            else
            {
                this.AddContention();
                throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
            }
        }

        private void ThrowIfFrozen()
        {
            if (this.GetOption(4))
            {
                throw new InvalidOperationException("The model cannot be changed once frozen");
            }
        }

        internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
        {
            BasicList.IPredicate predicate = new BasicTypeFinder(type);
            int index = this.basicTypes.IndexOf(predicate);
            if (index >= 0)
            {
                return ((BasicType) this.basicTypes[index]).Serializer;
            }
            lock (this.basicTypes)
            {
                WireType type2;
                index = this.basicTypes.IndexOf(predicate);
                if (index >= 0)
                {
                    return ((BasicType) this.basicTypes[index]).Serializer;
                }
                IProtoSerializer serializer = (MetaType.GetContractFamily(this, type, null) == MetaType.AttributeFamily.None) ? ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out type2, false, false, false, false) : null;
                if (serializer != null)
                {
                    this.basicTypes.Add(new BasicType(type, serializer));
                }
                return serializer;
            }
        }

        internal void VerifyFactory(MethodInfo factory, Type type)
        {
            if (factory != null)
            {
                if ((type != null) && Helpers.IsValueType(type))
                {
                    throw new InvalidOperationException();
                }
                if (!factory.IsStatic)
                {
                    throw new ArgumentException("A factory-method must be static", "factory");
                }
                if (((type != null) && (factory.ReturnType != type)) && (factory.ReturnType != base.MapType(typeof(object))))
                {
                    throw new ArgumentException("The factory-method must return object" + ((type == null) ? "" : (" or " + type.FullName)), "factory");
                }
                if (!CallbackSet.CheckCallbackParameters(this, factory))
                {
                    throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
                }
            }
        }

        private void WaitOnLock(MetaType type)
        {
            int opaqueToken = 0;
            try
            {
                this.TakeLock(ref opaqueToken);
            }
            finally
            {
                this.ReleaseLock(opaqueToken);
            }
        }

        private void WriteAssemblyAttributes(CompilerOptions options, string assemblyName, AssemblyBuilder asm)
        {
            if (!Helpers.IsNullOrEmpty(options.TargetFrameworkName))
            {
                Type type = null;
                try
                {
                    type = this.GetType("System.Runtime.Versioning.TargetFrameworkAttribute", base.MapType(typeof(string)).Assembly);
                }
                catch
                {
                }
                if (type != null)
                {
                    PropertyInfo[] infoArray;
                    object[] objArray;
                    if (Helpers.IsNullOrEmpty(options.TargetFrameworkDisplayName))
                    {
                        infoArray = new PropertyInfo[0];
                        objArray = new object[0];
                    }
                    else
                    {
                        infoArray = new PropertyInfo[] { type.GetProperty("FrameworkDisplayName") };
                        objArray = new object[] { options.TargetFrameworkDisplayName };
                    }
                    CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(type.GetConstructor(new Type[] { base.MapType(typeof(string)) }), new object[] { options.TargetFrameworkName }, infoArray, objArray);
                    asm.SetCustomAttribute(customBuilder);
                }
            }
            Type type2 = null;
            try
            {
                type2 = base.MapType(typeof(InternalsVisibleToAttribute));
            }
            catch
            {
            }
            if (type2 != null)
            {
                BasicList list = new BasicList();
                BasicList list2 = new BasicList();
                BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    MetaType current = (MetaType) enumerator.Current;
                    Assembly instance = current.Type.Assembly;
                    if (list2.IndexOfReference(instance) < 0)
                    {
                        list2.Add(instance);
                        AttributeMap[] mapArray = AttributeMap.Create(this, instance);
                        for (int i = 0; i < mapArray.Length; i++)
                        {
                            if (!(mapArray[i].AttributeType != type2))
                            {
                                object obj2;
                                mapArray[i].TryGet("AssemblyName", out obj2);
                                string str = obj2 as string;
                                if ((!(str == assemblyName) && !Helpers.IsNullOrEmpty(str)) && (list.IndexOf(new StringFinder(str)) < 0))
                                {
                                    list.Add(str);
                                    CustomAttributeBuilder builder2 = new CustomAttributeBuilder(type2.GetConstructor(new Type[] { base.MapType(typeof(string)) }), new object[] { str });
                                    asm.SetCustomAttribute(builder2);
                                }
                            }
                        }
                    }
                }
            }
        }

        private TypeBuilder WriteBasicTypeModel(CompilerOptions options, string typeName, ModuleBuilder module)
        {
            Type parent = base.MapType(typeof(TypeModel));
            TypeAttributes attr = (parent.Attributes & ~TypeAttributes.Abstract) | TypeAttributes.Sealed;
            if (options.Accessibility == Accessibility.Internal)
            {
                attr &= ~TypeAttributes.Public;
            }
            return module.DefineType(typeName, attr, parent);
        }

        private void WriteConstructors(TypeBuilder type, ref int index, SerializerPair[] methodPairs, ref ILGenerator il, int knownTypesCategory, FieldBuilder knownTypes, Type knownTypesLookupType, CompilerContext ctx)
        {
            int num;
            int num2;
            SerializerPair[] pairArray2;
            int num11;
            type.DefineDefaultConstructor(MethodAttributes.Public);
            il = type.DefineTypeInitializer().GetILGenerator();
            switch (knownTypesCategory)
            {
                case 1:
                    CompilerContext.LoadValue(il, this.types.Count);
                    il.Emit(OpCodes.Newarr, ctx.MapType(typeof(Type)));
                    index = 0;
                    foreach (SerializerPair pair in methodPairs)
                    {
                        il.Emit(OpCodes.Dup);
                        CompilerContext.LoadValue(il, index);
                        il.Emit(OpCodes.Ldtoken, pair.Type.Type);
                        il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
                        il.Emit(OpCodes.Stelem_Ref);
                        index++;
                    }
                    il.Emit(OpCodes.Stsfld, knownTypes);
                    il.Emit(OpCodes.Ret);
                    return;

                case 2:
                    CompilerContext.LoadValue(il, this.types.Count);
                    il.DeclareLocal(knownTypesLookupType);
                    il.Emit(OpCodes.Newobj, knownTypesLookupType.GetConstructor(new Type[] { base.MapType(typeof(int)) }));
                    il.Emit(OpCodes.Stsfld, knownTypes);
                    num = 0;
                    pairArray2 = methodPairs;
                    num11 = 0;
                    goto Label_0274;

                case 3:
                {
                    CompilerContext.LoadValue(il, this.types.Count);
                    il.Emit(OpCodes.Newobj, knownTypesLookupType.GetConstructor(new Type[] { base.MapType(typeof(int)) }));
                    il.Emit(OpCodes.Stsfld, knownTypes);
                    int num5 = 0;
                    foreach (SerializerPair pair3 in methodPairs)
                    {
                        il.Emit(OpCodes.Ldsfld, knownTypes);
                        il.Emit(OpCodes.Ldtoken, pair3.Type.Type);
                        il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
                        int num6 = num5++;
                        int baseKey = pair3.BaseKey;
                        if (baseKey != pair3.MetaKey)
                        {
                            num6 = -1;
                            for (int i = 0; i < methodPairs.Length; i++)
                            {
                                if ((methodPairs[i].BaseKey == baseKey) && (methodPairs[i].MetaKey == baseKey))
                                {
                                    num6 = i;
                                    break;
                                }
                            }
                        }
                        CompilerContext.LoadValue(il, num6);
                        il.Emit(OpCodes.Box, base.MapType(typeof(int)));
                        il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("Add", new Type[] { base.MapType(typeof(object)), base.MapType(typeof(object)) }), null);
                    }
                    il.Emit(OpCodes.Ret);
                    return;
                }
                default:
                    throw new InvalidOperationException();
            }
        Label_0219:
            CompilerContext.LoadValue(il, num2);
            il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("Add", new Type[] { base.MapType(typeof(Type)), base.MapType(typeof(int)) }), null);
            num11++;
        Label_0274:
            if (num11 < pairArray2.Length)
            {
                SerializerPair pair2 = pairArray2[num11];
                il.Emit(OpCodes.Ldsfld, knownTypes);
                il.Emit(OpCodes.Ldtoken, pair2.Type.Type);
                il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
                num2 = num++;
                int num3 = pair2.BaseKey;
                if (num3 != pair2.MetaKey)
                {
                    num2 = -1;
                    for (int j = 0; j < methodPairs.Length; j++)
                    {
                        if ((methodPairs[j].BaseKey == num3) && (methodPairs[j].MetaKey == num3))
                        {
                            num2 = j;
                            break;
                        }
                    }
                }
                goto Label_0219;
            }
            il.Emit(OpCodes.Ret);
        }

        private void WriteGetKeyImpl(TypeBuilder type, bool hasInheritance, SerializerPair[] methodPairs, CompilerContext.ILVersion ilVersion, out ILGenerator il, out int knownTypesCategory, out FieldBuilder knownTypes, out Type knownTypesLookupType)
        {
            BasicList list;
            int baseKey;
            il = Override(type, "GetKeyImpl");
            if (this.types.Count <= 20)
            {
                knownTypesCategory = 1;
                knownTypesLookupType = this.MapType(typeof(Type[]), true);
            }
            else
            {
                knownTypesLookupType = this.MapType(typeof(Dictionary<Type, int>), false);
                if (knownTypesLookupType == null)
                {
                    knownTypesLookupType = this.MapType(typeof(Hashtable), true);
                    knownTypesCategory = 3;
                }
                else
                {
                    knownTypesCategory = 2;
                }
            }
            knownTypes = type.DefineField("knownTypes", knownTypesLookupType, FieldAttributes.InitOnly | FieldAttributes.Static | FieldAttributes.Private);
            switch (knownTypesCategory)
            {
                case 1:
                    il.Emit(OpCodes.Ldsfld, knownTypes);
                    il.Emit(OpCodes.Ldarg_1);
                    il.EmitCall(OpCodes.Callvirt, base.MapType(typeof(IList)).GetMethod("IndexOf", new Type[] { base.MapType(typeof(object)) }), null);
                    if (!hasInheritance)
                    {
                        il.Emit(OpCodes.Ret);
                        return;
                    }
                    il.DeclareLocal(base.MapType(typeof(int)));
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Stloc_0);
                    list = new BasicList();
                    baseKey = -1;
                    for (int j = 0; j < methodPairs.Length; j++)
                    {
                        if (methodPairs[j].MetaKey == methodPairs[j].BaseKey)
                        {
                            break;
                        }
                        if (baseKey == methodPairs[j].BaseKey)
                        {
                            list.Add(list[list.Count - 1]);
                        }
                        else
                        {
                            list.Add(il.DefineLabel());
                            baseKey = methodPairs[j].BaseKey;
                        }
                    }
                    break;

                case 2:
                {
                    LocalBuilder local = il.DeclareLocal(base.MapType(typeof(int)));
                    Label label = il.DefineLabel();
                    il.Emit(OpCodes.Ldsfld, knownTypes);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Ldloca_S, local);
                    il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("TryGetValue", BindingFlags.Public | BindingFlags.Instance), null);
                    il.Emit(OpCodes.Brfalse_S, label);
                    il.Emit(OpCodes.Ldloc_S, local);
                    il.Emit(OpCodes.Ret);
                    il.MarkLabel(label);
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.Emit(OpCodes.Ret);
                    return;
                }
                case 3:
                {
                    Label label2 = il.DefineLabel();
                    il.Emit(OpCodes.Ldsfld, knownTypes);
                    il.Emit(OpCodes.Ldarg_1);
                    il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetProperty("Item").GetGetMethod(), null);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Brfalse_S, label2);
                    if (ilVersion != CompilerContext.ILVersion.Net1)
                    {
                        il.Emit(OpCodes.Unbox_Any, base.MapType(typeof(int)));
                    }
                    else
                    {
                        il.Emit(OpCodes.Unbox, base.MapType(typeof(int)));
                        il.Emit(OpCodes.Ldobj, base.MapType(typeof(int)));
                    }
                    il.Emit(OpCodes.Ret);
                    il.MarkLabel(label2);
                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.Emit(OpCodes.Ret);
                    return;
                }
                default:
                    throw new InvalidOperationException();
            }
            Label[] array = new Label[list.Count];
            list.CopyTo(array, 0);
            il.Emit(OpCodes.Switch, array);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
            baseKey = -1;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (baseKey == methodPairs[i].BaseKey)
                {
                    continue;
                }
                baseKey = methodPairs[i].BaseKey;
                int num4 = -1;
                for (int k = array.Length; k < methodPairs.Length; k++)
                {
                    if ((methodPairs[k].BaseKey == baseKey) && (methodPairs[k].MetaKey == baseKey))
                    {
                        num4 = k;
                        break;
                    }
                }
                il.MarkLabel(array[i]);
                CompilerContext.LoadValue(il, num4);
                il.Emit(OpCodes.Ret);
            }
        }

        private CompilerContext WriteSerializeDeserialize(string assemblyName, TypeBuilder type, SerializerPair[] methodPairs, CompilerContext.ILVersion ilVersion, ref ILGenerator il)
        {
            il = Override(type, "Serialize");
            CompilerContext context = new CompilerContext(il, false, true, methodPairs, this, ilVersion, assemblyName, base.MapType(typeof(object)));
            Label[] labels = new Label[this.types.Count];
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = il.DefineLabel();
            }
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Switch, labels);
            context.Return();
            for (int j = 0; j < labels.Length; j++)
            {
                SerializerPair pair = methodPairs[j];
                il.MarkLabel(labels[j]);
                il.Emit(OpCodes.Ldarg_2);
                context.CastFromObject(pair.Type.Type);
                il.Emit(OpCodes.Ldarg_3);
                il.EmitCall(OpCodes.Call, pair.Serialize, null);
                context.Return();
            }
            il = Override(type, "Deserialize");
            context = new CompilerContext(il, false, false, methodPairs, this, ilVersion, assemblyName, base.MapType(typeof(object)));
            for (int k = 0; k < labels.Length; k++)
            {
                labels[k] = il.DefineLabel();
            }
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Switch, labels);
            context.LoadNullRef();
            context.Return();
            for (int m = 0; m < labels.Length; m++)
            {
                SerializerPair pair2 = methodPairs[m];
                il.MarkLabel(labels[m]);
                Type valueType = pair2.Type.Type;
                if (valueType.IsValueType)
                {
                    il.Emit(OpCodes.Ldarg_2);
                    il.Emit(OpCodes.Ldarg_3);
                    il.EmitCall(OpCodes.Call, EmitBoxedSerializer(type, m, valueType, methodPairs, this, ilVersion, assemblyName), null);
                    context.Return();
                }
                else
                {
                    il.Emit(OpCodes.Ldarg_2);
                    context.CastFromObject(valueType);
                    il.Emit(OpCodes.Ldarg_3);
                    il.EmitCall(OpCodes.Call, pair2.Deserialize, null);
                    context.Return();
                }
            }
            return context;
        }

        private void WriteSerializers(CompilerOptions options, string assemblyName, TypeBuilder type, out int index, out bool hasInheritance, out SerializerPair[] methodPairs, out CompilerContext.ILVersion ilVersion)
        {
            index = 0;
            hasInheritance = false;
            methodPairs = new SerializerPair[this.types.Count];
            BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
            while (enumerator.MoveNext())
            {
                MetaType current = (MetaType) enumerator.Current;
                MethodBuilder serialize = type.DefineMethod("Write", MethodAttributes.Static | MethodAttributes.Private, CallingConventions.Standard, base.MapType(typeof(void)), new Type[] { current.Type, base.MapType(typeof(ProtoWriter)) });
                MethodBuilder deserialize = type.DefineMethod("Read", MethodAttributes.Static | MethodAttributes.Private, CallingConventions.Standard, current.Type, new Type[] { current.Type, base.MapType(typeof(ProtoReader)) });
                SerializerPair pair = new SerializerPair(this.GetKey(current.Type, true, false), this.GetKey(current.Type, true, true), current, serialize, deserialize, serialize.GetILGenerator(), deserialize.GetILGenerator());
                methodPairs[index++] = pair;
                if (pair.MetaKey != pair.BaseKey)
                {
                    hasInheritance = true;
                }
            }
            if (hasInheritance)
            {
                Array.Sort<SerializerPair>(methodPairs);
            }
            ilVersion = CompilerContext.ILVersion.Net2;
            if (options.MetaDataVersion == 0x10000)
            {
                ilVersion = CompilerContext.ILVersion.Net1;
            }
            index = 0;
            while (index < methodPairs.Length)
            {
                SerializerPair pair2 = methodPairs[index];
                CompilerContext ctx = new CompilerContext(pair2.SerializeBody, true, true, methodPairs, this, ilVersion, assemblyName, pair2.Type.Type);
                ctx.CheckAccessibility(pair2.Deserialize.ReturnType);
                pair2.Type.Serializer.EmitWrite(ctx, ctx.InputValue);
                ctx.Return();
                ctx = new CompilerContext(pair2.DeserializeBody, true, false, methodPairs, this, ilVersion, assemblyName, pair2.Type.Type);
                pair2.Type.Serializer.EmitRead(ctx, ctx.InputValue);
                if (!pair2.Type.Serializer.ReturnsValue)
                {
                    ctx.LoadValue(ctx.InputValue);
                }
                ctx.Return();
                index++;
            }
        }

        public bool AllowParseableTypes
        {
            get
            {
                return this.GetOption(0x40);
            }
            set
            {
                this.SetOption(0x40, value);
            }
        }

        public bool AutoAddMissingTypes
        {
            get
            {
                return this.GetOption(8);
            }
            set
            {
                if (!value && this.GetOption(2))
                {
                    throw new InvalidOperationException("The default model must allow missing types");
                }
                this.ThrowIfFrozen();
                this.SetOption(8, value);
            }
        }

        public bool AutoAddProtoContractTypesOnly
        {
            get
            {
                return this.GetOption(0x80);
            }
            set
            {
                this.SetOption(0x80, value);
            }
        }

        public bool AutoCompile
        {
            get
            {
                return this.GetOption(0x10);
            }
            set
            {
                this.SetOption(0x10, value);
            }
        }

        public static RuntimeTypeModel Default
        {
            get
            {
                return Singleton.Value;
            }
        }

        public bool InferTagFromNameDefault
        {
            get
            {
                return this.GetOption(1);
            }
            set
            {
                this.SetOption(1, value);
            }
        }

        public MetaType this[Type type]
        {
            get
            {
                return (MetaType) this.types[this.FindOrAddAuto(type, true, false, false)];
            }
        }

        public int MetadataTimeoutMilliseconds
        {
            get
            {
                return this.metadataTimeoutMilliseconds;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
                }
                this.metadataTimeoutMilliseconds = value;
            }
        }

        public bool UseImplicitZeroDefaults
        {
            get
            {
                return this.GetOption(0x20);
            }
            set
            {
                if (!value && this.GetOption(2))
                {
                    throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
                }
                this.SetOption(0x20, value);
            }
        }

        public enum Accessibility
        {
            Public,
            Internal
        }

        private sealed class BasicType
        {
            private readonly IProtoSerializer serializer;
            private readonly System.Type type;

            public BasicType(System.Type type, IProtoSerializer serializer)
            {
                this.type = type;
                this.serializer = serializer;
            }

            public IProtoSerializer Serializer
            {
                get
                {
                    return this.serializer;
                }
            }

            public System.Type Type
            {
                get
                {
                    return this.type;
                }
            }
        }

        private sealed class BasicTypeFinder : BasicList.IPredicate
        {
            private readonly Type type;

            public BasicTypeFinder(Type type)
            {
                this.type = type;
            }

            public bool IsMatch(object obj)
            {
                return (((RuntimeTypeModel.BasicType) obj).Type == this.type);
            }
        }

        public sealed class CompilerOptions
        {
            private RuntimeTypeModel.Accessibility accessibility;
            private string imageRuntimeVersion;
            private int metaDataVersion;
            private string outputPath;
            private string targetFrameworkDisplayName;
            private string targetFrameworkName;
            private string typeName;

            public void SetFrameworkOptions(MetaType from)
            {
                if (from == null)
                {
                    throw new ArgumentNullException("from");
                }
                foreach (AttributeMap map in AttributeMap.Create(from.Model, from.Type.Assembly))
                {
                    if (map.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
                    {
                        object obj2;
                        if (map.TryGet("FrameworkName", out obj2))
                        {
                            this.TargetFrameworkName = (string) obj2;
                        }
                        if (map.TryGet("FrameworkDisplayName", out obj2))
                        {
                            this.TargetFrameworkDisplayName = (string) obj2;
                            return;
                        }
                        break;
                    }
                }
            }

            public RuntimeTypeModel.Accessibility Accessibility
            {
                get
                {
                    return this.accessibility;
                }
                set
                {
                    this.accessibility = value;
                }
            }

            public string ImageRuntimeVersion
            {
                get
                {
                    return this.imageRuntimeVersion;
                }
                set
                {
                    this.imageRuntimeVersion = value;
                }
            }

            public int MetaDataVersion
            {
                get
                {
                    return this.metaDataVersion;
                }
                set
                {
                    this.metaDataVersion = value;
                }
            }

            public string OutputPath
            {
                get
                {
                    return this.outputPath;
                }
                set
                {
                    this.outputPath = value;
                }
            }

            public string TargetFrameworkDisplayName
            {
                get
                {
                    return this.targetFrameworkDisplayName;
                }
                set
                {
                    this.targetFrameworkDisplayName = value;
                }
            }

            public string TargetFrameworkName
            {
                get
                {
                    return this.targetFrameworkName;
                }
                set
                {
                    this.targetFrameworkName = value;
                }
            }

            public string TypeName
            {
                get
                {
                    return this.typeName;
                }
                set
                {
                    this.typeName = value;
                }
            }
        }

        private sealed class MetaTypeFinder : BasicList.IPredicate
        {
            private readonly Type type;

            public MetaTypeFinder(Type type)
            {
                this.type = type;
            }

            public bool IsMatch(object obj)
            {
                return (((MetaType) obj).Type == this.type);
            }
        }

        internal sealed class SerializerPair : IComparable
        {
            public readonly int BaseKey;
            public readonly MethodBuilder Deserialize;
            public readonly ILGenerator DeserializeBody;
            public readonly int MetaKey;
            public readonly MethodBuilder Serialize;
            public readonly ILGenerator SerializeBody;
            public readonly MetaType Type;

            public SerializerPair(int metaKey, int baseKey, MetaType type, MethodBuilder serialize, MethodBuilder deserialize, ILGenerator serializeBody, ILGenerator deserializeBody)
            {
                this.MetaKey = metaKey;
                this.BaseKey = baseKey;
                this.Serialize = serialize;
                this.Deserialize = deserialize;
                this.SerializeBody = serializeBody;
                this.DeserializeBody = deserializeBody;
                this.Type = type;
            }

            int IComparable.CompareTo(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentException("obj");
                }
                RuntimeTypeModel.SerializerPair pair = (RuntimeTypeModel.SerializerPair) obj;
                if (this.BaseKey == this.MetaKey)
                {
                    if (pair.BaseKey == pair.MetaKey)
                    {
                        return this.MetaKey.CompareTo(pair.MetaKey);
                    }
                    return 1;
                }
                if (pair.BaseKey == pair.MetaKey)
                {
                    return -1;
                }
                int num = this.BaseKey.CompareTo(pair.BaseKey);
                if (num == 0)
                {
                    num = this.MetaKey.CompareTo(pair.MetaKey);
                }
                return num;
            }
        }

        private sealed class Singleton
        {
            internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);

            private Singleton()
            {
            }
        }

        private sealed class StringFinder : BasicList.IPredicate
        {
            private readonly string value;

            public StringFinder(string value)
            {
                this.value = value;
            }

            bool BasicList.IPredicate.IsMatch(object obj)
            {
                return (this.value == ((string) obj));
            }
        }
    }
}

