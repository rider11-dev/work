namespace OneCardSln.Components.Serialize.Protobuf.Meta
{
    using System;

    public sealed class LockContentedEventArgs : EventArgs
    {
        private readonly string ownerStackTrace;

        internal LockContentedEventArgs(string ownerStackTrace)
        {
            this.ownerStackTrace = ownerStackTrace;
        }

        public string OwnerStackTrace
        {
            get
            {
                return this.ownerStackTrace;
            }
        }
    }
}

