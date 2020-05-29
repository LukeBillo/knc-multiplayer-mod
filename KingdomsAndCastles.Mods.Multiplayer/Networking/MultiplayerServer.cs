using System;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.PacketHandlers;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking
{
    public class MultiplayerServer : MultiplayerSocket
    {
        public static MultiplayerServer Instance => Lazy.Value;

        private static readonly Lazy<MultiplayerServer> Lazy =
            new Lazy<MultiplayerServer>(() => new MultiplayerServer());

        public const int Port = 64469;

        public MultiplayerServer() : base(Port)
        {
            // Handler registrations for all server-side packets
            RegisterHandler<RequestInitialStatePacketHandler>();
        }
    }
}