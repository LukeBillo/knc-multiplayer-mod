using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Harmony;
using KingdomsAndCastles.Mods.Multiplayer.ErrorHandling.Exceptions;
using KingdomsAndCastles.Mods.Multiplayer.Networking;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets.Client;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace KingdomsAndCastles.Mods.Multiplayer.UI
{
    [HarmonyPatch(typeof(MainMenuMode), "Init")]
    public static class MultiplayerButtonPatch
    {
        private static ConfiguredTaskAwaitable _listenerTask;
        
        [HarmonyPostfix]
        public static void Init(MainMenuMode __instance)
        {
            Multiplayer.ModHelper.Log("Initialising main menu...");

            var mainMenuInterface = __instance.mainMenuUI;

            Multiplayer.ModHelper.Log("Loaded UI buttons...");
            var menuContainer = mainMenuInterface.transform.Find("MainMenu/TopLevel/Body/ButtonContainer");
            var newButton = menuContainer.transform.Find("New");

            if (menuContainer is null || newButton is null)
            {
                Multiplayer.ModHelper.Log("Failed to find menu items");
                throw new NullReferenceException("Failed to instantiate menu, as menu items that were expected were missing.");
            }
            
            var createdHostButton = Object.Instantiate(newButton, menuContainer, false);
            var hostButtonTextComponent = createdHostButton.GetChild(0).GetComponent<TextMeshProUGUI>();
            hostButtonTextComponent.name = "HostMultiplayer";
            hostButtonTextComponent.text = "Host Multiplayer";
            
            var hostButton = createdHostButton.gameObject.GetComponent<Button>();
            hostButton.onClick.RemoveAllListeners();
            hostButton.onClick.AddListener(HostButtonClicked);
            
            var createdConnectButton = Object.Instantiate(newButton, menuContainer, false);
            var connectButtonTextComponent = createdConnectButton.GetChild(0).GetComponent<TextMeshProUGUI>();
            connectButtonTextComponent.text = "Connect to Multiplayer";

            var connectButton = createdHostButton.gameObject.GetComponent<Button>();
            connectButton.name = "ConnectToMultiplayer";
            connectButton.onClick.RemoveAllListeners();
            connectButton.onClick.AddListener(ConnectButtonClicked);

            Multiplayer.ModHelper.Log("Added multiplayer button to the menu!");
        }

        private static void HostButtonClicked()
        {
            // [Must] todo: UI for game selection, etc.
            
            var socket = MultiplayerServer.Instance;
            _listenerTask = Task.Run(() => socket.Listen())
                .ConfigureAwait(false);
        }

        private static void ConnectButtonClicked()
        {
            var socket = MultiplayerClient.Instance;
            var isSuccessfullyConnected = socket.TryConnect("127.0.0.1");
            
            if (!isSuccessfullyConnected)
            {
                // [Must] todo: Show window "Failed to connect"
                throw new NotImplementedException("Failed to connect to server- no connection failure behaviour implemented");
            }

            var serverConnectionId = socket.ServerConnectionId;
            if (serverConnectionId == ConnectionIds.Disconnected)
            {
                throw new InvalidMultiplayerStateException($"{nameof(MultiplayerClient)} reported that connection was successful, but {nameof(serverConnectionId)} is {nameof(ConnectionIds.Disconnected)}");
            }
            
            // [Should] todo: Loading or splash screen of some kind while initialising
            socket.SendPacket(new RequestInitialStatePacket(), socket.ServerConnectionId);
        }
    }
}