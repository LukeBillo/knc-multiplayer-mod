using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Server
{
    public class InitialStateResponsePacket : Packet
    {
        public override short PacketType { get; }
        public override QosType ChannelType { get; }
    }
}