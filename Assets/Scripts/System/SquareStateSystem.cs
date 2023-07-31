using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;


public partial struct SquareStateSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
        state.RequireForUpdate<Player2Tag>();
        state.RequireForUpdate<SquareComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        var p1 = state.EntityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
        var tf1 = state.EntityManager.GetComponentData<LocalTransform>(p1).Position;
        var p2 = state.EntityManager.CreateEntityQuery(typeof(Player2Tag)).GetSingletonEntity();
        var tf2 = state.EntityManager.GetComponentData<LocalTransform>(p2).Position;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (tf, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
        {
            if (tf1.x == tf.ValueRW.Position.x && tf1.y == tf.ValueRW.Position.y)
            {
                squ.ValueRW.state = (int)color.Green;
            }
            if (tf2.x == tf.ValueRW.Position.x && tf2.y == tf.ValueRW.Position.y)
            {
                squ.ValueRW.state = (int)color.Red;
            }
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
