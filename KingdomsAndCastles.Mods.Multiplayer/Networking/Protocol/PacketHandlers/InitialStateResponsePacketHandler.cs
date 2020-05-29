using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Server;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.PacketHandlers
{
    public class InitialStateResponsePacketHandler : PacketHandler<InitialStateResponsePacket, EmptyPacket>
    {
        public override short HandlesPacketType { get; }
        public override EmptyPacket Handle(InitialStateResponsePacket request)
        {
            Multiplayer.ModHelper.Log($"Received {nameof(InitialStateResponsePacket)}: {request.Test}");
            return Packet.Empty;
        }
    }
}