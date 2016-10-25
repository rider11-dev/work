namespace MyNet.Components.Serialize.Protobuf.Serializers
{
    using MyNet.Components.Serialize.Protobuf.Compiler;
    using MyNet.Components.Serialize.Protobuf.Meta;
    using MyNet.Components.Serialize.Protobuf.Protobuf;
    using System;
    using System.Reflection;

    internal sealed class DefaultValueDecorator : ProtoDecoratorBase
    {
        private readonly object defaultValue;

        public DefaultValueDecorator(TypeModel model, object defaultValue, IProtoSerializer tail) : base(tail)
        {
            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }
            if (model.MapType(defaultValue.GetType()) != tail.ExpectedType)
            {
                throw new ArgumentException("Default value is of incorrect type", "defaultValue");
            }
            this.defaultValue = defaultValue;
        }

        private void EmitBeq(CompilerContext ctx, CodeLabel label, Type type)
        {
            switch (Helpers.GetTypeCode(type))
            {
                case ProtoTypeCode.Boolean:
                case ProtoTypeCode.Char:
                case ProtoTypeCode.SByte:
                case ProtoTypeCode.Byte:
                case ProtoTypeCode.Int16:
                case ProtoTypeCode.UInt16:
                case ProtoTypeCode.Int32:
                case ProtoTypeCode.UInt32:
                case ProtoTypeCode.Int64:
                case ProtoTypeCode.UInt64:
                case ProtoTypeCode.Single:
                case ProtoTypeCode.Double:
                    ctx.BranchIfEqual(label, false);
                    return;
            }
            MethodInfo method = type.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static, null, new Type[] { type, type }, null);
            if ((method == null) || (method.ReturnType != ctx.MapType(typeof(bool))))
            {
                throw new InvalidOperationException("No suitable equality operator found for default-values of type: " + type.FullName);
            }
            ctx.EmitCall(method);
            ctx.BranchIfTrue(label, false);
        }

        private void EmitBranchIfDefaultValue(CompilerContext ctx, CodeLabel label)
        {
            Type expectedType = this.ExpectedType;
            switch (Helpers.GetTypeCode(expectedType))
            {
                case ProtoTypeCode.Boolean:
                    if (!((bool) this.defaultValue))
                    {
                        ctx.BranchIfFalse(label, false);
                        return;
                    }
                    ctx.BranchIfTrue(label, false);
                    return;

                case ProtoTypeCode.Char:
                    if (((char) this.defaultValue) != '\0')
                    {
                        ctx.LoadValue((int) ((char) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.SByte:
                    if (((sbyte) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) ((sbyte) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.Byte:
                    if (((byte) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) ((byte) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.Int16:
                    if (((short) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) ((short) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.UInt16:
                    if (((ushort) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) ((ushort) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.Int32:
                    if (((int) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) this.defaultValue);
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.UInt32:
                    if (((uint) this.defaultValue) != 0)
                    {
                        ctx.LoadValue((int) ((uint) this.defaultValue));
                        this.EmitBeq(ctx, label, expectedType);
                        return;
                    }
                    ctx.BranchIfFalse(label, false);
                    return;

                case ProtoTypeCode.Int64:
                    ctx.LoadValue((long) this.defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.UInt64:
                    ctx.LoadValue((long) ((ulong) this.defaultValue));
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.Single:
                    ctx.LoadValue((float) this.defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.Double:
                    ctx.LoadValue((double) this.defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.Decimal:
                {
                    decimal defaultValue = (decimal) this.defaultValue;
                    ctx.LoadValue(defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;
                }
                case ProtoTypeCode.DateTime:
                    ctx.LoadValue(((DateTime) this.defaultValue).ToBinary());
                    ctx.EmitCall(ctx.MapType(typeof(DateTime)).GetMethod("FromBinary"));
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.String:
                    ctx.LoadValue((string) this.defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                case ProtoTypeCode.TimeSpan:
                {
                    TimeSpan span = (TimeSpan) this.defaultValue;
                    if (!(span == TimeSpan.Zero))
                    {
                        ctx.LoadValue(span.Ticks);
                        ctx.EmitCall(ctx.MapType(typeof(TimeSpan)).GetMethod("FromTicks"));
                        break;
                    }
                    ctx.LoadValue(typeof(TimeSpan).GetField("Zero"));
                    break;
                }
                case ProtoTypeCode.Guid:
                    ctx.LoadValue((Guid) this.defaultValue);
                    this.EmitBeq(ctx, label, expectedType);
                    return;

                default:
                    throw new NotSupportedException("Type cannot be represented as a default value: " + expectedType.FullName);
            }
            this.EmitBeq(ctx, label, expectedType);
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            base.Tail.EmitRead(ctx, valueFrom);
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            CodeLabel label = ctx.DefineLabel();
            if (valueFrom == null)
            {
                ctx.CopyValue();
                CodeLabel label2 = ctx.DefineLabel();
                this.EmitBranchIfDefaultValue(ctx, label2);
                base.Tail.EmitWrite(ctx, null);
                ctx.Branch(label, true);
                ctx.MarkLabel(label2);
                ctx.DiscardValue();
            }
            else
            {
                ctx.LoadValue(valueFrom);
                this.EmitBranchIfDefaultValue(ctx, label);
                base.Tail.EmitWrite(ctx, valueFrom);
            }
            ctx.MarkLabel(label);
        }

        public override object Read(object value, ProtoReader source)
        {
            return base.Tail.Read(value, source);
        }

        public override void Write(object value, ProtoWriter dest)
        {
            if (!object.Equals(value, this.defaultValue))
            {
                base.Tail.Write(value, dest);
            }
        }

        public override Type ExpectedType
        {
            get
            {
                return base.Tail.ExpectedType;
            }
        }

        public override bool RequiresOldValue
        {
            get
            {
                return base.Tail.RequiresOldValue;
            }
        }

        public override bool ReturnsValue
        {
            get
            {
                return base.Tail.ReturnsValue;
            }
        }
    }
}

