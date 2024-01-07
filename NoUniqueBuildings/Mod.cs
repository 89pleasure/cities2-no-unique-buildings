using Colossal.Logging;
using Game;
using Game.Modding;
using NoUniqueBuildings.Systems;

namespace NoUniqueBuildings
{
    public sealed class Mod : IMod
    {
        public const string Name = MyPluginInfo.PLUGIN_NAME;
        public static Mod Instance { get; set; }
        internal ILog Log { get; private set; }
        public void OnLoad()
        {
            Instance = this;
            Log = LogManager.GetLogger(Name);
#if VERBOSE
            Log.effectivenessLevel = Level.Verbose;
#elif DEBUG
            Log.effectivenessLevel = Level.Debug;
#endif

            Log.Info("Loading.");
        }

        public void OnCreateWorld(UpdateSystem updateSystem)
        {
            UnityEngine.Debug.Log("[NoUniqueBuildings]: Add system to world.");
            updateSystem.UpdateAt<NoUniqueBuildingsSystem>(SystemUpdatePhase.ModificationEnd);
        }

        public void OnDispose()
        {
            UnityEngine.Debug.Log("[NoUniqueBuildings]: Mod disposed.");
            Instance = null;
        }
    }
}
