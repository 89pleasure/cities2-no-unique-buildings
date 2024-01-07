using Game.UI.InGame;
using HarmonyLib;

namespace NoUniqueBuildings.Patches
{
    
    internal class UniqueAssetTrackingSystemPatches
    {
        [HarmonyPatch(typeof(UniqueAssetTrackingSystem), "OnUpdate")]
        [HarmonyPrefix]
        public static bool Prefix(UniqueAssetTrackingSystem __instance)
        {
            return false;
        }
    }
}
