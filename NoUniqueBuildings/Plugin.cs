using BepInEx;
using HarmonyLib;
using System.Reflection;
using System.Linq;

#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace NoUniqueBuildings
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private Mod Mod;
        
        private void Awake()
        {
            Mod = new Mod();
            Mod.OnLoad();

            UnityEngine.Debug.Log("[NoUniqueBuildings]: Loading Harmony patches.");

            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID + "_Cities2Harmony");
            var patchedMethods = harmony.GetPatchedMethods().ToArray();

            UnityEngine.Debug.Log($"[NoUniqueBuildings]: Plugin {MyPluginInfo.PLUGIN_GUID} is loaded! Patched methods " + patchedMethods.Length);
            foreach (var patchedMethod in patchedMethods)
            {
                UnityEngine.Debug.Log($"[NoUniqueBuildings]: Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
            }
        }
    }
}