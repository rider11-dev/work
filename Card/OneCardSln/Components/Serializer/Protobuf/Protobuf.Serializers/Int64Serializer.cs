namespace MyNet.Components.Serialize.Protobuf.Serializers
{
    using System;
    using MyNet.Components.Serialize.Protobuf.Meta;
    using MyNet.Components.Serialize.Protobuf.Protobuf;
    using MyNet.Components.Serialize.Protobuf.Compiler;
    internal sealed class Int64Serializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof(long);

        public Int64Serializer(TypeModel model)
        {
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadInt64", this.ExpectedType);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteInt64", valueFrom);
        }

        public object Read(object value, ProtoReader source)
        {
            return source.ReadInt64();
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteInt64((long) value, dest);
        }

        public Type ExpectedType
        {
            get
            {
                return expectedType;
            }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get
            {
                return false;
            }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get
            {
                return true;
            }
        }
    }
}

