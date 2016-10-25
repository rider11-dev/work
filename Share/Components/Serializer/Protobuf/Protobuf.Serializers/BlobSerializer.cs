namespace MyNet.Components.Serialize.Protobuf.Serializers
{
    using System;
    using MyNet.Components.Serialize.Protobuf.Meta;
    using MyNet.Components.Serialize.Protobuf.Protobuf;
    using MyNet.Components.Serialize.Protobuf.Compiler;
    internal sealed class BlobSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof(byte[]);
        private readonly bool overwriteList;

        public BlobSerializer(TypeModel model, bool overwriteList)
        {
            this.overwriteList = overwriteList;
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            if (this.overwriteList)
            {
                ctx.LoadNullRef();
            }
            else
            {
                ctx.LoadValue(valueFrom);
            }
            ctx.LoadReaderWriter();
            ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("AppendBytes"));
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteBytes", valueFrom);
        }

        public object Read(object value, ProtoReader source)
        {
            return ProtoReader.AppendBytes(this.overwriteList ? null : ((byte[]) value), source);
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteBytes((byte[]) value, dest);
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
                return !this.overwriteList;
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

