using System;
using System.Runtime.Serialization;

namespace KingdomsAndCastles.Mods.Multiplayer.ErrorHandling.Exceptions
{
    [Serializable]
    public class PacketHandlerInvocationException : Exception
    {
        public PacketHandlerInvocationException()
        {
        }

        public PacketHandlerInvocationException(string message) : base(message)
        {
        }

        public PacketHandlerInvocationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PacketHandlerInvocationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}