namespace MyNet.Components.Serialize.Protobuf.Serializers
{
    internal interface ISerializerProxy
    {
        IProtoSerializer Serializer { get; }
    }
}

