using System;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol;
using UnityEngine.Networking;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking
{
    public sealed class MultiplayerClient : MultiplayerSocket
    {
        public static MultiplayerClient Instance => Lazy.Value;
        private static readonly Lazy<MultiplayerClient> Lazy = new Lazy<MultiplayerClient>(() => new MultiplayerClient());

        public int ServerConnectionId { get; private set; } = ConnectionIds.Disconnected;

        public MultiplayerClient() : base(64468) {}
        
        public bool TryConnect(string connectToAddress)
        {
            Multiplayer.ModHelper.Log($"Attempting to connect to {connectToAddress}");
            
            var connectionId = NetworkTransport.Connect(HostId, connectToAddress, MultiplayerServer.Port, ConnectionIds.Disconnected, out var error);
            var networkError = (NetworkError) error;
            
            var isConnectionSuccessful = networkError == NetworkError.Ok;
            var connectionDebugLog = isConnectionSuccessful
                ? $"Successfully connected to {connectToAddress}."
                : $"Failed to connect to {connectToAddress}, received {networkError}";
            
            Multiplayer.ModHelper.Log(connectionDebugLog);

            if (isConnectionSuccessful)
            {
                ActiveConnections.Add(connectionId);
                ServerConnectionId = connectionId;
            }

            return isConnectionSuccessful;
        }

    }
}