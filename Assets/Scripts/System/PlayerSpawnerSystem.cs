using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public partial struct PlayerSpawnerSystem : ISystem
{
    //public void OnCreate(ref SystemState state)
    //{
    //    state.RequireForUpdate<StartCommand>();
    //}
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var spawner in SystemAPI.Query<RefRW<PlayerSpawnerComponent>>())
        {
            if (spawner.ValueRW.canSpawn)
            {
                spawner.ValueRW.canSpawn = false;
                var newPlayerE = ecb.Instantiate( spawner.ValueRO.prefab);
                ecb.SetComponent(newPlayerE, new LocalTransform
                {
                    Position = new float3(0,0,-1),
                    Rotation = quaternion.identity,
                    Scale = 1,
                });
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
