using CortexDeveloper.ECSMessages.Service;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
namespace Systems
{
    [UpdateAfter(typeof(BoardSpawnerSystem))]
    public partial struct WallSpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }

        public void OnUpdate(ref SystemState state)
        {
            for (int i = 0; i < 3; i++)
            {
                System.Random random = new System.Random();
                int x = random.Next(0, 4);
                int y = random.Next(0, 9);
                foreach (var (tf, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
                {
                    if (x == tf.ValueRW.Position.x && y == tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                    {
                        squ.ValueRW.state = (int)color.Wall;
                    }
                    if (x == 9 - tf.ValueRW.Position.x && y == 9 - tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                    {
                        squ.ValueRW.state = (int)color.Wall;
                    }
                }
            }
        }
    }
}
