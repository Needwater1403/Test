using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

namespace Systems
{
    public partial struct ControlMovingSystem : ISystem
    {
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dirX = Input.GetAxisRaw("Horizontal");
            var dirY = Input.GetAxisRaw("Vertical");
            foreach (var (tf, m) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithAll<PlayerTag>())
            {
                if(m.ValueRW.canMove)
                {
                    if (Input.GetKeyDown("d") && m.ValueRW.canMoveRight)
                    {   
                        tf.ValueRW.Position.x += 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                    else if (Input.GetKeyDown("a") && m.ValueRW.canMoveLeft)
                    {
                        tf.ValueRW.Position.x -= 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                    else if (Input.GetKeyDown("w") && m.ValueRW.canMoveUp)
                    {
                        tf.ValueRW.Position.y += 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                    else if (Input.GetKeyDown("s") && m.ValueRW.canMoveDown)
                    {
                        tf.ValueRW.Position.y -= 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<Player2Tag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                        break;
                    }
                    
                }
                
            }
            foreach (var (tf, m) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithAll<Player2Tag>())
            {
                if(m.ValueRW.canMove)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        tf.ValueRW.Position.x += 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        tf.ValueRW.Position.x -= 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        tf.ValueRW.Position.y += 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        tf.ValueRW.Position.y -= 1;
                        foreach (var m1 in SystemAPI.Query<RefRW<MovableComponent>>().WithAll<PlayerTag>())
                        {
                            m1.ValueRW.canMove = true;
                        }
                        m.ValueRW.canMove = false;
                    }
                }
            }
        }

    }
}