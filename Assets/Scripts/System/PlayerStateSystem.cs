using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;


public partial struct PlayerStateSystem : ISystem
{
    //public void OnCreate(ref SystemState state)
    //{
    //    state.RequireForUpdate<StartCommand>();
    //}
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (tf, m, en) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithAll<PlayerTag>().WithEntityAccess())
        {
            switch (m.ValueRW.state)
            {
                case 0:
                    {
                        tf.ValueRW.Position.y += 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p1_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 1:
                    {
                        tf.ValueRW.Position.y -= 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p1_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 2:
                    {
                        tf.ValueRW.Position.x -= 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p1_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 3:
                    {

                        tf.ValueRW.Position.x += 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p1_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 4:
                    {   
                        break;
                    }
            }

        }
        foreach (var (tf, m, en) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithAll<Player2Tag>().WithEntityAccess())
        {
            switch (m.ValueRW.state)
            {
                case 0:
                    {
                        tf.ValueRW.Position.y += 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p2_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 1:
                    {
                        tf.ValueRW.Position.y -= 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p2_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 2:
                    {
                        tf.ValueRW.Position.x -= 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p2_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 3:
                    {
                        tf.ValueRW.Position.x += 1;
                        m.ValueRW.state = (int)dir.Stand;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        foreach (var s in SystemAPI.Query<RefRW<ScoreComponent>>())
                        {
                            s.ValueRW.p2_Score++;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }

        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
