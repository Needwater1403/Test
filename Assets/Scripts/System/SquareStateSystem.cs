using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;


public partial struct SquareStateSystem : ISystem
{
    //public void OnCreate(ref SystemState state)
    //{
    //    state.RequireForUpdate<StartCommand>();
    //}
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (tf, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
        {
            foreach (var pl in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>())
            {
                if(pl.ValueRW.Position.x == tf.ValueRW.Position.x && pl.ValueRW.Position.y == tf.ValueRW.Position.y)
                {
                    squ.ValueRW.state = (int)color.Green;
                }
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
