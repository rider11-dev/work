namespace OneCardSln.Components.Serialize.Protobuf.Compiler
{
    using OneCardSln.Components.Serialize.Protobuf.Protobuf;
    using System;
    using System.Runtime.CompilerServices;

    internal delegate void ProtoSerializer(object value, ProtoWriter dest);
}

