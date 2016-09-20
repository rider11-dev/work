namespace OneCardSln.Components.Serialize.Protobuf.Compiler
{
    using OneCardSln.Components.Serialize.Protobuf.Protobuf;

    using System;
    using System.Runtime.CompilerServices;

    internal delegate object ProtoDeserializer(object value, ProtoReader source);
}

