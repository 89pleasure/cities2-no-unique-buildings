using Game;
using Game.Common;
using HarmonyLib;

namespace NoUniqueBuildings.Patches
{
    [HarmonyPatch(typeof(SystemOrder))]
    internal class SystemOrderPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(SystemOrder), nameof(SystemOrder.Initialize))]
        public static void GetSystemOrder(UpdateSystem updateSystem)
        {
            Mod.Instance.OnCreateWorld(updateSystem);
        }
    }
}
