using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Topology;
using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking
{
    public class MultiplayerSocket
    {
        protected ConnectionConfig ConnectionConfig { get; }
        protected List<IChannel> Channels { get; }
        protected List<int> ActiveConnections { get; }
        protected int HostId { get; }

        protected MultiplayerSocket(int port)
        {
            NetworkTransport.Init();
            
            ConnectionConfig = new ConnectionConfig();
            int reliableSequenceChannelId = ConnectionConfig.AddChannel(QosType.ReliableSequenced);

            Channels = new List<IChannel>();
            ActiveConnections = new List<int>();
            
            Channels.Add(new ReliableInSequenceChannel(reliableSequenceChannelId));
            
            var topology = new HostTopology(ConnectionConfig, 4);
            HostId = NetworkTransport.AddHost(topology, port);
        }
    }
}