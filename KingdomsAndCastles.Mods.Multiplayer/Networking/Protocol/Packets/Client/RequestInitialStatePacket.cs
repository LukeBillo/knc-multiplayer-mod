using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Client
{
    public class RequestInitialStatePacket : Packet
    {
        public const PacketIdentifier Identity = PacketIdentifier.RequestInitState;
        public const QosType SendOverChannel = QosType.Reliable;

        public override short PacketType => (short) Identity;
        public override QosType ChannelType => SendOverChannel;
        
        
    }
}