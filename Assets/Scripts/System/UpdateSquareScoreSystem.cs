using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(SquareScoreSystem))]
    public partial struct UpdateSquareScoreSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
            {
                if (squ.ValueRW.state == (int)color.Empty)
                {
                    foreach (var (tf1, squ1) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
                    {
                        int pm = 0;
                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x + 1 && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && squ1.ValueRW.state != (int)color.Empty)
                        {
                            pm++;
                        }
                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x - 1 && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && squ1.ValueRW.state != (int)color.Empty)
                        {
                            pm++;
                        }
                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x && tf1.ValueRW.Position.y == tf.ValueRW.Position.y + 1 && squ1.ValueRW.state != (int)color.Empty)
                        {
                            pm++;
                        }
                        if (tf1.ValueRW.Position.x == tf.ValueRW.Position.x && tf1.ValueRW.Position.y == tf.ValueRW.Position.y - 1 && squ1.ValueRW.state != (int)color.Empty)
                        {
                            pm++;
                        }
                        if(pm>=3)
                        {
                            squ.ValueRW.point = -1;
                        }
                    }
                }
            }

        }

    }
}