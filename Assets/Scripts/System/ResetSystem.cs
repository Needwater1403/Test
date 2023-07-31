using Unity.Collections;
using Unity.Entities;


namespace Systems
{
    public partial struct ResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ResetCommand>();
        }
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (squ, en) in SystemAPI.Query<RefRW<SquareComponent>>().WithEntityAccess())
            {
                ecb.DestroyEntity(en);
            }
            foreach (var (squ, en) in SystemAPI.Query<RefRW<MovableComponent>>().WithEntityAccess())
            {   
                ecb.DestroyEntity(en);
            }
            foreach (var w in SystemAPI.Query<RefRW<WinStatus>>())
            {
                w.ValueRW.status = -2;
            }
            foreach (var (sc, en) in SystemAPI.Query<RefRW<ScoreComponent>>().WithEntityAccess())
            {
                sc.ValueRW.p1_Score = 0;
                sc.ValueRW.p2_Score = 0;
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}