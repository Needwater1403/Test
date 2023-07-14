using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.IO.LowLevel.Unsafe;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct TriggerEventsSystem : ISystem
    {
        struct ComponentDataHandles
        {
            public ComponentLookup<SquareComponent> squareState;

            public ComponentDataHandles(ref SystemState systemState)
            {
                squareState = systemState.GetComponentLookup<SquareComponent>(false);
            }
            public void Update(ref SystemState systemState)
            {
                squareState.Update(ref systemState);
            }
        }
        ComponentDataHandles data;
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            data = new ComponentDataHandles(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            data.Update(ref state);
            //state.Dependency = new BulletTriggerEvents
            //{   
            //    squareState = data.squareState,
            //    squareList = SystemAPI.GetComponentLookup<SquareTag>(),
            //    player = SystemAPI.GetComponentLookup<PlayerTag>(),
            //}.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            //state.Dependency.Complete();
        }
        public struct BulletTriggerEvents : ITriggerEventsJob
        {
            public ComponentLookup<SquareTag> squareList;
            public ComponentLookup<PlayerTag> player;
            public ComponentLookup<SquareComponent> squareState;
            public void Execute(Unity.Physics.TriggerEvent collisionEvent)
            {
                Entity entB = collisionEvent.EntityB;
                Entity entA = collisionEvent.EntityA;
                var isSquareA = squareList.HasComponent(collisionEvent.EntityA);               
                var isSquareB = squareList.HasComponent(collisionEvent.EntityB);
                var isPLayerA = player.HasComponent(collisionEvent.EntityA);
                var isPLayerB = player.HasComponent(collisionEvent.EntityB);

                if (isPLayerA && isSquareB)
                {
                    var state = squareState[entB];
                    state.state = (int)color.Green;
                    squareState[entB] = state;
                }
                else if(isPLayerB && isSquareA)
                {
                    var state = squareState[entA];
                    state.state = (int)color.Green;
                    squareState[entA] = state;
                }
            }
        }
    }
}