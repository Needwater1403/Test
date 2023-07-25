using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


namespace Systems
{
    [UpdateAfter(typeof(WallSpawnerSystem))]
    public partial struct SquareScoreSystem : ISystem
    {
        int a;
        int b;
        public void OnCreate(ref SystemState state)
        {
            a = 0;
            b = 9;
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (a < b) { 
                for (int i = a; i <= b; i++)
                {
                    for (int j = a; j <= b; j++)
                    {
                        foreach (var (tf, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
                        {
                            if (tf.ValueRW.Position.x == j && tf.ValueRW.Position.y == i && !squ.ValueRW.isOccupied)
                            { 
                                squ.ValueRW.point++;
                                squ.ValueRW.basePoint++;
                            }
                        }
                    }
                }
                a++;
                b--;
                Debug.Log(a + " " + b);
            }
            else
            {             
                state.Enabled = false;
            }
            

        }

    }
}