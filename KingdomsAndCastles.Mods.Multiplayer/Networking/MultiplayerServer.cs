using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking
{
    public class MultiplayerServer : MultiplayerSocket
    {
        public static MultiplayerServer Instance => Lazy.Value;
        private static readonly Lazy<MultiplayerServer> Lazy = new Lazy<MultiplayerServer>(() => new MultiplayerServer());
        
        public const int Port = 64469;
        
        public MultiplayerServer() : base(Port) {}
        
        public void Listen()
        {
            const int bufferSize = 1024;
            var receiveBuffer = new byte[bufferSize];
            
            Multiplayer.ModHelper.Log("Listening for network events...");

            while (true)
            {
                var eventType = NetworkTransport.Receive(out var recHostId, out var connectionId, out var channelId, receiveBuffer, bufferSize, out var dataSize, out var error);
            
                switch (eventType)
                {
                    case NetworkEventType.ConnectEvent:
                        Multiplayer.ModHelper.Log("Connect event received!");
                        break;
                    case NetworkEventType.Nothing:
                        break;
                    default:
                        Multiplayer.ModHelper.Log($"Multiplayer event received: {eventType}");
                        break;
                }
            }
        }
    }
}