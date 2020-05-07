using System;
using System.Threading.Tasks;
using Harmony;
using KingdomsAndCastles.Mods.Multiplayer.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KingdomsAndCastles.Mods.Multiplayer.UI
{
    [HarmonyPatch(typeof(MainMenuMode), "Init")]
    public static class MultiplayerButtonPatch
    {
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
                throw new NullReferenceException(
                    "Failed to instantiate menu, as menu items that were expected were missing.");
            }
            
            var createdHostButton = GameObject.Instantiate(newButton, menuContainer, false);
            var hostButtonTextComponent = createdHostButton.GetChild(0).GetComponent<TextMeshProUGUI>();
            hostButtonTextComponent.text = "Host Multiplayer";
            
            var hostButton = createdHostButton.gameObject.GetComponent<Button>();
            hostButton.onClick.AddListener(HostButtonClicked);
            
            var createdConnectButton = GameObject.Instantiate(newButton, menuContainer, false);
            var connectButtonTextComponent = createdConnectButton.GetChild(0).GetComponent<TextMeshProUGUI>();
            connectButtonTextComponent.text = "Connect to Multiplayer";

            var connectButton = createdHostButton.gameObject.GetComponent<Button>();
            connectButton.onClick.AddListener(ConnectButtonClicked);

            Multiplayer.ModHelper.Log("Added multiplayer button to the menu!");
        }

        private static void HostButtonClicked()
        {
            var socket = MultiplayerServer.Instance;
            Task
                .Run(() => socket.Listen())
                .ConfigureAwait(false);
        }

        private static void ConnectButtonClicked()
        {
            var socket = MultiplayerClient.Instance;
            socket.TryConnect("127.0.0.1");
        }
    }
}