using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Client;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Server;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.PacketHandlers
{
    public class RequestInitialStatePacketHandler : PacketHandler<RequestInitialStatePacket, InitialStateResponsePacket>
    {
        public const PacketIdentifier HandlesPacketIdentity = PacketIdentifier.RequestInitState;
        public override short HandlesPacketType => (short) HandlesPacketIdentity;
        
        public override InitialStateResponsePacket Handle(RequestInitialStatePacket _)
        {
            return new InitialStateResponsePacket();
        }
    }
}