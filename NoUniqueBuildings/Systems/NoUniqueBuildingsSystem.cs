using Game;
using Unity.Entities;
using UnityEngine.Scripting;
using Game.SceneFlow;
using NoUniqueBuildings.Jobs;
using Unity.Jobs;
using Colossal.Serialization.Entities;

namespace NoUniqueBuildings.Systems
{
    public partial class NoUniqueBuildingsSystem : GameSystemBase
    {
        private EndFrameBarrier m_Barrier;
        private UpdateSignatureBuildingsTypeHandle m_UpdateSignatureBuildingsJobTypeHandle;
        private EntityQuery m_UpdateSignatureBuildingsJobQuery;

        [Preserve]
        protected override void OnCreate()
        {
            base.OnCreate();

            // Create a barrier system using the default world
            m_Barrier = World.GetOrCreateSystemManaged<EndFrameBarrier>();

            // Job Queries
            UpdateSignatureBuildingsQuery updateSignatureBuildingsQuery = new();
            m_UpdateSignatureBuildingsJobQuery = GetEntityQuery(updateSignatureBuildingsQuery.Query);

            RequireForUpdate(m_UpdateSignatureBuildingsJobQuery);
            Mod.Instance.Log.Info("System created.");
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
        }

        [Preserve]
        protected override void OnUpdate()
        {
            if (GameManager.instance == null || !GameManager.instance.gameMode.IsGameOrEditor())
            {
                return;
            }

            if (!m_UpdateSignatureBuildingsJobQuery.IsEmptyIgnoreFilter)
            {
                Mod.Instance.Log.Info("Run UpdateSignatureBuildingsJob");
                m_UpdateSignatureBuildingsJobTypeHandle.AssignHandles(ref CheckedStateRef);
                UpdateSignatureBuildingsJob updateSignatureBuildingsJob = new()
                {
                    Ecb = m_Barrier.CreateCommandBuffer().AsParallelWriter(),
                    EntityHandle = m_UpdateSignatureBuildingsJobTypeHandle.EntityTypeHandle,
                    PlaceableObjectDataLookup = m_UpdateSignatureBuildingsJobTypeHandle.PlaceableObjectDataLookup,
                };
                Dependency = updateSignatureBuildingsJob.Schedule(m_UpdateSignatureBuildingsJobQuery, Dependency);
                m_Barrier.AddJobHandleForProducer(Dependency);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Mod.Instance.Log.Info("System destroyed.");

        }
    }
}
