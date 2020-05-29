using System.Collections.Generic;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.PacketHandlers;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking
{
    public abstract class MultiplayerSocket
    {
        protected ConnectionConfig ConnectionConfig { get; }
        protected List<int> ActiveConnections { get; }
        protected int HostId { get; }
        
        private readonly Dictionary<short, IPacketHandler> _packetHandlers;
        private readonly Dictionary<QosType, byte> _channels;
        
        private bool _activelyListening = false;
        private const int PacketBufferSize = 1024;

        protected MultiplayerSocket(int port)
        {
            ActiveConnections = new List<int>();
            _packetHandlers = new Dictionary<short, IPacketHandler>();
            _channels = new Dictionary<QosType, byte>();
            
            NetworkTransport.Init();
            
            ConnectionConfig = new ConnectionConfig();
            RegisterChannel(QosType.Reliable);

            var topology = new HostTopology(ConnectionConfig, 4);
            HostId = NetworkTransport.AddHost(topology, port);
        }

        public void Listen()
        {
            var receiveBuffer = new byte[PacketBufferSize];

            Multiplayer.ModHelper.Log("Listening for network events...");

            while (_activelyListening)
            {
                var eventType = NetworkTransport.Receive(out var recHostId, out var connectionId, out var channelId,
                    receiveBuffer, PacketBufferSize, out var dataSize, out var error);

                var state = (NetworkError) error;
                if (state != NetworkError.Ok)
                {
                    // todo: handle network errors
                }

                switch (eventType)
                {
                    case NetworkEventType.ConnectEvent:
                        Multiplayer.ModHelper.Log("Connect event received!");
                        break;
                    case NetworkEventType.DataEvent:
                        var serialisedPacket = System.Text.Encoding.UTF8.GetString(receiveBuffer);
                        var packet = JsonConvert.DeserializeObject<Packet>(serialisedPacket);
                        var response = HandlePacket(packet);
                        SendPacket(response, connectionId);
                        break;
                    case NetworkEventType.DisconnectEvent:
                        Multiplayer.ModHelper.Log($"A disconnection occurred.");
                        break;
                    case NetworkEventType.Nothing:
                        break;
                    case NetworkEventType.BroadcastEvent:
                        break;
                    default:
                        Multiplayer.ModHelper.Log($"Multiplayer event received: {eventType}");
                        break;
                }
            }
        }

        public Packet HandlePacket(Packet data)
        {
            var handler = _packetHandlers[data.PacketType];
            return handler.Invoke(data);
        }

        public void SendPacket(Packet packet, int connectionId)
        {
            if (packet is EmptyPacket)
                return;
            
            var sendToChannelId = _channels[packet.ChannelType];
            var packetBuffer = new byte[PacketBufferSize];
            
            NetworkTransport.Send(HostId, connectionId, sendToChannelId, packetBuffer, PacketBufferSize, out var error);
            
            var state = (NetworkError) error;
            if (state != NetworkError.Ok)
            {
                // todo: handle network errors
            }
        }

        private void RegisterChannel(QosType channelType)
        {
            var channelId = ConnectionConfig.AddChannel(channelType);
            _channels.Add(channelType, channelId);
        }

        protected void RegisterHandler<THandler>() where THandler : IPacketHandler, new()
        {
            var handler = new THandler();
            _packetHandlers.Add(handler.HandlesPacketType, handler);
        }
    }
}
