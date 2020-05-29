using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets
{
    public class EmptyPacket : Packet
    {
        public const PacketIdentifier Identifier = PacketIdentifier.Empty;

        public override short PacketType => (short) Identifier;
        public override QosType ChannelType => QosType.Reliable;
    }
}