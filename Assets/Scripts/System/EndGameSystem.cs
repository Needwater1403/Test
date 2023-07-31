using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System;


public partial struct EndGameSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        //state.RequireForUpdate<StartCommand>();
        state.RequireForUpdate<MovableComponent>();
    }
    public void OnUpdate(ref SystemState state)
    {
        var winStatus = state.EntityManager.CreateEntityQuery(typeof(WinStatus)).GetSingletonEntity();
        var gt = state.EntityManager.GetComponentData<WinStatus>(winStatus);

        var p1 = state.EntityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
        var m1 = state.EntityManager.GetComponentData<MovableComponent>(p1);

        var p2 = state.EntityManager.CreateEntityQuery(typeof(Player2Tag)).GetSingletonEntity();
        var m2 = state.EntityManager.GetComponentData<MovableComponent>(p2);

        var scoreBoard = state.EntityManager.CreateEntityQuery(typeof(ScoreComponent)).GetSingletonEntity();
        var score = state.EntityManager.GetComponentData<ScoreComponent>(scoreBoard);

        if (!m1.canMoveDown && !m1.canMoveRight && !m1.canMoveLeft && !m1.canMoveUp)
        {
            m1.isStuck = true;
            state.EntityManager.SetComponentData(p1, m1);
        }
        if (!m2.canMoveDown && !m2.canMoveRight && !m2.canMoveLeft && !m2.canMoveUp)
        {
            m2.isStuck = true;
            state.EntityManager.SetComponentData(p2, m2);
        }
        
        if (score.p1_Score > score.p2_Score && m2.isStuck)
        {
            gt.status = (int)gameState.MaxWin;
            state.EntityManager.SetComponentData(winStatus, gt);
        }
        else if (!m2.isStuck && m1.isStuck)
        {
            gt.status = (int)gameState.MinWin;
            state.EntityManager.SetComponentData(winStatus, gt);
        }
        else if(score.p1_Score == score.p2_Score && m1.isStuck && m2.isStuck)
        {
            gt.status = (int)gameState.Tie;
            state.EntityManager.SetComponentData(winStatus, gt);
        }
    }
}
