using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct BoardSpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new SpawnJob { deltaTime = SystemAPI.Time.DeltaTime, ecb = ecb.AsParallelWriter()}.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
        public partial struct SpawnJob : IJobEntity
        {
            public float deltaTime;
            public EntityCommandBuffer.ParallelWriter ecb;
            void Execute(RefRW<BoardSpawnerComponent> spawner, RefRW<LocalTransform> tf)
            {
                for (int i = 0; i < spawner.ValueRO.num; i++)
                {
                    for (int j = 0; j < spawner.ValueRO.num; j++)
                    {
                        var newEnemyE = ecb.Instantiate(0, spawner.ValueRO.prefab);
                        ecb.SetComponent(i, newEnemyE, new LocalTransform
                        {
                            Position = new float3(j, i, 0),
                            Rotation = quaternion.identity,
                            Scale = 1,
                        });
                        ecb.SetComponent(i, newEnemyE, new SquareComponent
                        {
                            rowID = i,
                            colID = j,
                        });
                    }
                }
            }
        }
    }
}