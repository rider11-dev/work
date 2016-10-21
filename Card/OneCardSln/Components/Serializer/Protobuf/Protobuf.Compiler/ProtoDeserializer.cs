namespace MyNet.Components.Serialize.Protobuf.Compiler
{
    using MyNet.Components.Serialize.Protobuf.Protobuf;

    using System;
    using System.Runtime.CompilerServices;

    internal delegate object ProtoDeserializer(object value, ProtoReader source);
}

