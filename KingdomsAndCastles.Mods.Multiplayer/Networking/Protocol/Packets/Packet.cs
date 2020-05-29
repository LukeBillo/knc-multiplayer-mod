using System;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets
{
    public abstract class Packet : MessageBase
    {
        public abstract short PacketType { get; }
        public abstract QosType ChannelType { get; }
    }
}