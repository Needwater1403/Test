using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System;


public partial struct PlayerSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StartCommand>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var spawner in SystemAPI.Query<RefRW<PlayerSpawnerComponent>>())
        {
            System.Random random = new System.Random();
            int x = random.Next(0, 4);
            int y = random.Next(0, 6);
            var newPlayerE = ecb.Instantiate(spawner.ValueRO.player1);
            ecb.SetComponent(newPlayerE, new LocalTransform
            {
                Position = new float3(x, y, -1),
                Rotation = quaternion.identity,
                Scale = 1,
            });
            var newPlayer2E = ecb.Instantiate(spawner.ValueRO.player2);
            ecb.SetComponent(newPlayer2E, new LocalTransform
            {
                Position = new float3(9 - x, 9 - y, -1),
                Rotation = quaternion.identity,
                Scale = 1,
            });
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
