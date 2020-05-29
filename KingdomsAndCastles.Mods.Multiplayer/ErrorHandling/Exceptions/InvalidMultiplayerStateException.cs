using System;
using System.Runtime.Serialization;

namespace KingdomsAndCastles.Mods.Multiplayer.ErrorHandling.Exceptions
{
    [Serializable]
    public class InvalidMultiplayerStateException : Exception
    {
        public InvalidMultiplayerStateException()
        {
        }

        public InvalidMultiplayerStateException(string message) : base(message)
        {
        }

        public InvalidMultiplayerStateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidMultiplayerStateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}