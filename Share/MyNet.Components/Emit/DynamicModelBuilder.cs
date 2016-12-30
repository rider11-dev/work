using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Emit
{
    public class DynamicModelBuilder
    {
        const string AsmName = "DynamicModels";
        private static Dictionary<Type, Type> _vmTypes = new Dictionary<Type, Type>();
        private static AssemblyBuilder _asmBuilder = null;
        public static AssemblyBuilder AsmBuilder
        {
            get
            {
                if (_asmBuilder == null)
                {
                    //_asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(AsmName), AssemblyBuilderAccess.RunAndSave);
                    _asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(AsmName), AssemblyBuilderAccess.Run);
                }
                return _asmBuilder;
            }
        }
        private static ModuleBuilder _moduleBuilder;
        public static ModuleBuilder ModuleBuilder
        {
            get
            {
                if (_moduleBuilder == null)
                {
                    //_moduleBuilder = AsmBuilder.DefineDynamicModule(AsmName, AsmName + ".dll");
                    _moduleBuilder = AsmBuilder.DefineDynamicModule(AsmName);
                }
                return _moduleBuilder;
            }
        }
        public static TInterface GetInstance<TInterface>(Type parent = null, string nmspace = "", Func<IEnumerable<PropertyCustomAttributeUnit>> propAttrProvider = null, params object[] ctorArgs)
        {
            IEnumerable<PropertyCustomAttributeUnit> propAttrs = null;
            if (propAttrProvider != null)
            {
                propAttrs = propAttrProvider();
            }
            var newType = GetTypeInternal<TInterface>(parent: parent, nmspace: nmspace, propAttrs: propAttrs);
            var obj = Activator.CreateInstance(newType, ctorArgs);
            return (TInterface)obj;
        }

        private static Type GetTypeInternal<TInterface>(Type parent = null, string nmspace = "", IEnumerable<PropertyCustomAttributeUnit> propAttrs = null)
        {
            var iType = typeof(TInterface);
            if (!iType.IsInterface)
            {
                throw new ArgumentException("只能是接口", "TInterface");
            }

            if (!_vmTypes.ContainsKey(iType))
            {
                TypeBuilder typeBuilder = ModuleBuilder.DefineType(string.Format("{0}.{1}_Impl", AsmName + (nmspace.IsEmpty() ? "" : "." + nmspace), iType.Name),
                TypeAttributes.Public, parent, new Type[] { iType });
                //1、构造函数
                if (parent != null)
                {
                    var parentCtors = parent.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                    if (parentCtors.IsNotEmpty())
                    {
                        foreach (var ctor in parentCtors)
                        {
                            DefineCtorInvokingParent(typeBuilder, ctor);
                        }
                    }
                }
                //2、属性
                var props = typeof(TInterface).GetProperties();
                foreach (var prop in props)
                {
                    DefineProperty(typeBuilder, prop, propAttrs == null ? null : propAttrs.Where(u => u.prop_name == prop.Name).FirstOrDefault());
                }
                //3、ICopyable接口
                DefineCopyToMethod<TInterface>(typeBuilder, props);

                var type = typeBuilder.CreateType();
                _vmTypes.Add(iType, type);
                //AsmBuilder.Save(AsmName + "." + type.Name + ".dll");
            }
            return _vmTypes[iType];
        }
        private static void DefineProperty(TypeBuilder typeBuilder, PropertyInfo prop, PropertyCustomAttributeUnit attrUnit = null)
        {
            //定义私有字段
            FieldBuilder fldBuilder = typeBuilder.DefineField("_" + prop.Name, prop.PropertyType, FieldAttributes.Private);

            //定义属性
            PropertyBuilder propBuilder = typeBuilder.DefineProperty(prop.Name, PropertyAttributes.None, prop.PropertyType, null);
            //1、属性基本设置
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig |
                MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.Final;
            //getter
            MethodBuilder getterBuilder = typeBuilder.DefineMethod("get_" + prop.Name, getSetAttr, prop.PropertyType, null);
            ILGenerator getterIL = getterBuilder.GetILGenerator();
            getterIL.Emit(OpCodes.Ldarg_0);
            getterIL.Emit(OpCodes.Ldfld, fldBuilder);//加载字段的值
            getterIL.Emit(OpCodes.Ret);
            //setter
            MethodBuilder setterBuilder = typeBuilder.DefineMethod("set_" + prop.Name, getSetAttr, null, new Type[] { prop.PropertyType });
            ILGenerator setterIL = setterBuilder.GetILGenerator();
            #region TODO，如果字段值与value相等，则结束
            //TODO，这里应添加if value!=_prop _prop=value控制，暂未实现
            //setterIL.Emit(OpCodes.Ldarg_0);
            //setterIL.Emit(OpCodes.Ldfld, fldBuilder);
            //setterIL.Emit(OpCodes.Ldarg_1);
            //if (prop.PropertyType == typeof(Int32) || prop.PropertyType.BaseType == typeof(Enum))
            //{
            //    setterIL.Emit(OpCodes.Ceq);
            //    setterIL.Emit(OpCodes.Ldc_I4_0);
            //    setterIL.Emit(OpCodes.Ceq);
            //}
            //else
            //{
            //    setterIL.Emit(OpCodes.Call, prop.PropertyType.GetMethod("op_Inequality"));
            //}
            //setterIL.Emit(OpCodes.Stloc_0);
            //setterIL.Emit(OpCodes.Ldloc_0);
            //setterIL.Emit(OpCodes.Brfalse_S);
            #endregion

            setterIL.Emit(OpCodes.Ldarg_0);
            setterIL.Emit(OpCodes.Ldarg_1);
            setterIL.Emit(OpCodes.Stfld, fldBuilder);//替换字段的值
            //调用父类方法，通知更改
            if (typeBuilder.BaseType == typeof(BaseModel))
            {
                setterIL.Emit(OpCodes.Ldarg_0);
                setterIL.Emit(OpCodes.Ldstr, prop.Name);
                setterIL.Emit(OpCodes.Call, typeof(BaseModel).GetMethod("RaisePropertyChanged"));
            }
            setterIL.Emit(OpCodes.Ret);
            propBuilder.SetGetMethod(getterBuilder);
            propBuilder.SetSetMethod(setterBuilder);

            //2、属性特性
            if (attrUnit != null && attrUnit.HasAttr)
            {
                var error = string.Empty;
                if (!attrUnit.check(out error))
                {
                    throw new Exception(string.Format("创建自定义类型——添加属性特性错误：{0}", error));
                }
                CustomAttributeBuilder attrBuilder = null;
                ConstructorInfo ctorInfo = null;
                List<Type> ctorArgTypes = new List<Type>();
                List<object> ctorArgs = new List<object>();
                List<PropertyInfo> namedProperties = new List<PropertyInfo>();
                List<object> propertyValues = new List<object>();
                foreach (var attr in attrUnit.attrs)
                {
                    attrBuilder = null;
                    ctorInfo = null;
                    ctorArgTypes.Clear();
                    ctorArgs.Clear();
                    namedProperties.Clear();
                    propertyValues.Clear();

                    var attrType = Type.GetType(attr.attr_type);
                    if (!string.IsNullOrWhiteSpace(attr.error_msg))
                    {
                        namedProperties.Add(attrType.GetProperty("ErrorMessage"));
                        propertyValues.Add(attr.error_msg);
                    }
                    if (!string.IsNullOrWhiteSpace(attr.error_msg_res_name))
                    {
                        namedProperties.Add(attrType.GetProperty("ErrorMessageResourceName"));
                        propertyValues.Add(attr.error_msg_res_name);
                    }
                    if (!string.IsNullOrWhiteSpace(attr.error_msg_res_type))
                    {
                        namedProperties.Add(attrType.GetProperty("ErrorMessageResourceType"));
                        propertyValues.Add(Type.GetType(attr.error_msg_res_type));
                    }
                    if (!string.IsNullOrWhiteSpace(attr.ctor_arg_types))
                    {
                        ctorArgTypes.AddRange(attr.ctor_arg_types.Split(',').Select(s => Type.GetType(s)));
                        for (int idx = 0; idx < ctorArgTypes.Count; idx++)
                        {
                            ctorArgs.Add(Convert.ChangeType(attr.ctor_arg_values[idx], ctorArgTypes[idx]));
                        }
                    }
                    ctorInfo = attrType.GetConstructor(ctorArgTypes.ToArray());
                    attrBuilder = new CustomAttributeBuilder(ctorInfo, ctorArgs.ToArray(), namedProperties.ToArray(), propertyValues.ToArray());
                    propBuilder.SetCustomAttribute(attrBuilder);
                }
            }

        }
        private static void DefineCtorInvokingParent(TypeBuilder typeBuilder, ConstructorInfo parentCtor)
        {
            var pCtorParams = parentCtor.GetParameters();
            var paramTypes = pCtorParams.Select(p => p.ParameterType).ToArray();
            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(parentCtor.Attributes, parentCtor.CallingConvention, paramTypes);
            ILGenerator ctorIL = ctorBuilder.GetILGenerator();

            ctorIL.Emit(OpCodes.Ldarg_0);
            if (pCtorParams.Length >= 1)//加载第一个参数
            {
                ctorIL.Emit(OpCodes.Ldarg_1);
            }
            if (pCtorParams.Length >= 2)
            {
                ctorIL.Emit(OpCodes.Ldarg_2);
            }
            if (pCtorParams.Length >= 3)
            {
                ctorIL.Emit(OpCodes.Ldarg_3);
            }
            //The ldarg.s instruction is an efficient encoding for loading arguments indexed from 4 through 255.
            //https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.ldarg_s.aspx
            if (pCtorParams.Length >= 4)//第4个以及以后的参数
            {
                for (int idx = 3; idx < pCtorParams.Length; idx++)
                {
                    ctorIL.Emit(OpCodes.Ldarg_S, idx + 1);
                }
            }
            ctorIL.Emit(OpCodes.Call, parentCtor);
            ctorIL.Emit(OpCodes.Ret);
        }
        private static void DefineCopyToMethod<TInterface>(TypeBuilder typeBuilder, PropertyInfo[] props)
        {
            Type iType = typeof(TInterface);
            if (iType.GetInterface(typeof(ICopyable).Name) == null)
            {
                return;
            }
            var method = typeof(ICopyable).GetMethod("CopyTo");
            var copyParams = method.GetParameters();
            MethodBuilder copyMBuilder = typeBuilder.DefineMethod(method.Name,
                //实现接口方法时，MethodAttributes.Virtual不能少
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                CallingConventions.HasThis | CallingConventions.Standard,
                method.ReturnType,
                copyParams.IsEmpty() ? null : copyParams.Select(p => p.ParameterType).ToArray());
            ILGenerator copyIL = copyMBuilder.GetILGenerator();
            //声明连个临时变量
            LocalBuilder localIType = copyIL.DeclareLocal(iType);
            LocalBuilder isnull = copyIL.DeclareLocal(typeof(bool));
            //声明两个控制标签
            Label lblExit = copyIL.DefineLabel();
            Label lblCopy = copyIL.DefineLabel();

            //是否为空
            copyIL.Emit(OpCodes.Ldarg_1);
            copyIL.Emit(OpCodes.Ldnull);
            copyIL.Emit(OpCodes.Ceq);
            copyIL.Emit(OpCodes.Stloc_1);//给isnull变量赋值
            copyIL.Emit(OpCodes.Ldloc_1);//加载isnull变量的值
            copyIL.Emit(OpCodes.Brfalse_S, lblCopy);//不为空，跳转到copy主体
            copyIL.Emit(OpCodes.Br_S, lblExit);//为空，跳转到结束标记

            copyIL.MarkLabel(lblCopy);//标记开始复制操作
            copyIL.Emit(OpCodes.Ldarg_1);
            copyIL.Emit(OpCodes.Isinst, iType);
            copyIL.Emit(OpCodes.Stloc_0);
            if (props.IsNotEmpty())
            {
                foreach (var prop in props)
                {
                    copyIL.Emit(OpCodes.Ldloc_0);
                    copyIL.Emit(OpCodes.Ldarg_0);
                    copyIL.Emit(OpCodes.Call, iType.GetMethod("get_" + prop.Name));
                    copyIL.Emit(OpCodes.Callvirt, iType.GetMethod("set_" + prop.Name, new Type[] { prop.PropertyType }));
                }
            }

            copyIL.MarkLabel(lblExit);//标记结束
            copyIL.Emit(OpCodes.Ret);
        }
    }
}
