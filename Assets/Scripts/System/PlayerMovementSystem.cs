using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

namespace Systems
{
    public partial struct ControlMovingSystem : ISystem
    {
        //public void OnCreate(ref SystemState state)
        //{
        //    state.RequireForUpdate<StartCommand>();
        //}
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dirX = Input.GetAxisRaw("Horizontal");
            var dirY = Input.GetAxisRaw("Vertical");
            foreach (var (tf, ent) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>().WithEntityAccess())
            {
                if(Input.GetKeyDown("d"))
                { 
                    tf.ValueRW.Position.x += 1; 
                }
                else if (Input.GetKeyDown("a"))
                {
                    tf.ValueRW.Position.x -= 1;
                }
                else if (Input.GetKeyDown("w"))
                {
                    tf.ValueRW.Position.y += 1;
                }
                else if (Input.GetKeyDown("s"))
                {
                    tf.ValueRW.Position.y -= 1;
                }
            }
            foreach (var (tf, ent) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Player2Tag>().WithEntityAccess())
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tf.ValueRW.Position.x += 1;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    tf.ValueRW.Position.x -= 1;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tf.ValueRW.Position.y += 1;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tf.ValueRW.Position.y -= 1;
                }
            }
        }

    }
}