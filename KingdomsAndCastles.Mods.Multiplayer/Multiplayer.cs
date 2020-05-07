using System;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace KingdomsAndCastles.Mods.Multiplayer
{
    public class Multiplayer : MonoBehaviour
    {
        public static KCModHelper ModHelper { get; private set; }
        
        public void Preload(KCModHelper modHelper)
        {
            ModHelper = modHelper;
            ModHelper.Log("Initialising multiplayer mod!");
            
            var harmony = HarmonyInstance.Create("KingdomsAndCastles.Mod.Multiplayer");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            ModHelper.Log("Multiplayer successfully initialised!");
        }
    }
}