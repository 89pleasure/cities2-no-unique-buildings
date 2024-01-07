using Game.UI.InGame;
using HarmonyLib;

namespace NoUniqueBuildings.Patches
{
    [HarmonyPatch(typeof(UniqueAssetTrackingSystem), "OnUpdate")]
    internal class UniqueAssetTrackingSystemPatches
    {
        [HarmonyPrefix]
        public static bool Prefix(UniqueAssetTrackingSystem __instance)
        {
            return false;
        }
    }
}
