namespace OneCardSln.Components.Serialize.Protobuf.Serializers
{
    internal interface ISerializerProxy
    {
        IProtoSerializer Serializer { get; }
    }
}

