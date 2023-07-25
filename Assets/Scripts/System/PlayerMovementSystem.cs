using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using JetBrains.Annotations;
using Unity.Entities.UniversalDelegates;
using Unity.Collections;

namespace Systems
{
    public partial struct ControlMovingSystem : ISystem
    {
        int up;
        int down;
        int left;
        int right;
        int max;
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {   
            var p1 = state.EntityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
            var m1 = state.EntityManager.GetComponentData<MovableComponent>(p1);

            var p2 = state.EntityManager.CreateEntityQuery(typeof(Player2Tag)).GetSingletonEntity();


            if (m1.canMove)
            {
                if (Input.GetKeyDown("d") && m1.canMoveRight)
                {
                    m1.state = (int)dir.Right;
                    state.EntityManager.SetComponentData(p1, m1);
                }
                else if (Input.GetKeyDown("a") && m1.canMoveLeft)
                {
                    m1.state = (int)dir.Left;
                    state.EntityManager.SetComponentData(p1, m1);
                }
                else if (Input.GetKeyDown("w") && m1.canMoveUp)
                {
                    m1.state = (int)dir.Up;
                    state.EntityManager.SetComponentData(p1, m1);
                }
                else if (Input.GetKeyDown("s") && m1.canMoveDown)
                {
                    m1.state = (int)dir.Down;
                    state.EntityManager.SetComponentData(p1, m1);
                }
            }
            foreach (var (tf, m) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithAll<Player2Tag>())
            {
                if(m.ValueRW.canMove)
                {
                    //System.Random random = new System.Random();
                    //int dirState = random.Next(0, 4);
                    up = 0;
                    down = 0;
                    left = 0;
                    right = 0;
                    NativeArray<int> arr = new NativeArray<int>(4,Allocator.Persistent);
                    foreach (var (tf1, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
                    {   
                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x && tf1.ValueRW.Position.y == tf.ValueRW.Position.y + 1 && !squ.ValueRW.isOccupied)
                        {
                            up = squ.ValueRW.point;
                            arr[0] = up;
                        }
                        //else if( tf1.ValueRW.Position.x == tf.ValueRW.Position.x && tf1.ValueRW.Position.y == tf.ValueRW.Position.y + 1 && squ.ValueRW.isOccupied)
                        //{
                        //    up = 0;
                        //    arr[0] = up;
                        //}

                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x && tf1.ValueRW.Position.y == tf.ValueRW.Position.y - 1 && !squ.ValueRW.isOccupied)
                        {
                            down = squ.ValueRW.point;
                            arr[1] = down;
                        }

                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x - 1 && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                        {
                            left = squ.ValueRW.point;
                            arr[2] = left;
                        }

                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x + 1 && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                        {
                            right = squ.ValueRW.point;
                            arr[3] = right;
                        }
                    }
                    Debug.Log(up + " " + down + " " + left + " " + right + " ");
                    max = -100;
                    for (int i = 0; i < arr.Length; i++)
                    { 
                        if(max < arr[i])
                        {
                            max = arr[i];
                        }
                    }
                     //   Debug.Log(arr[0] + " " + arr[1] + " " + arr[2] + " " + arr[3] + " ");
                    Debug.Log(max);

                    if (max == right && m.ValueRW.canMoveRight)
                    {
                        m.ValueRW.state = (int)dir.Right;
                    }
                    else if (max == left && m.ValueRW.canMoveLeft)
                    {
                        m.ValueRW.state = (int)dir.Left;
                    }
                    else if (max == up && m.ValueRW.canMoveUp)
                    {
                        m.ValueRW.state = (int)dir.Up;
                    }
                    else if (max == down && m.ValueRW.canMoveDown)
                    {
                        m.ValueRW.state = (int)dir.Down;
                    }
                }
            }
            
        }
    }
}