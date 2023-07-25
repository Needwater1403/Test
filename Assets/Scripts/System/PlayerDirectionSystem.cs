using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerDirectionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MovableComponent>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {   
        foreach (var (tf, m) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>())
        {   
            //RANGE
            if(tf.ValueRW.Position.x == m.ValueRW.minX)
            {
                m.ValueRW.canMoveLeft = false;
            }
            if (tf.ValueRW.Position.x == m.ValueRW.maxX)
            {
                m.ValueRW.canMoveRight = false;
            }
            if (tf.ValueRW.Position.y == m.ValueRW.minY)
            {
                m.ValueRW.canMoveDown = false;
            }
            if (tf.ValueRW.Position.y == m.ValueRW.maxY)
            {
                m.ValueRW.canMoveUp = false;
            }

            //DIRECTION
            foreach (var (tf1, squ) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SquareComponent>>())
            {   
                //RIGHT
                if (tf1.ValueRW.Position.x == (tf.ValueRW.Position.x + 1) && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveRight = true;
                }
                else if (tf1.ValueRW.Position.x == (tf.ValueRW.Position.x + 1) && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveRight = false;
                }

                //LEFT
                if (tf1.ValueRW.Position.x == (tf.ValueRW.Position.x - 1) && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && !squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveLeft = true;
                }
                else if (tf1.ValueRW.Position.x == (tf.ValueRW.Position.x - 1) && tf1.ValueRW.Position.y == tf.ValueRW.Position.y && squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveLeft = false;
                }

                //UP
                if (tf1.ValueRW.Position.y == (tf.ValueRW.Position.y + 1) && tf1.ValueRW.Position.x == tf.ValueRW.Position.x && !squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveUp = true;
                }
                else if (tf1.ValueRW.Position.y == (tf.ValueRW.Position.y + 1) && tf1.ValueRW.Position.x == tf.ValueRW.Position.x && squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveUp = false;
                }

                //DOWN
                if (tf1.ValueRW.Position.y == (tf.ValueRW.Position.y - 1) && tf1.ValueRW.Position.x == tf.ValueRW.Position.x && !squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveDown = true;
                }
                else if (tf1.ValueRW.Position.y == (tf.ValueRW.Position.y - 1) && tf1.ValueRW.Position.x == tf.ValueRW.Position.x && squ.ValueRW.isOccupied)
                {
                    m.ValueRW.canMoveDown = false;
                }
            }
        }
    }
}
