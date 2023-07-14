using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct PlayerSpawnerComponent : IComponentData
{
    public Entity player1;
    public Entity player2;
    public bool canSpawn;
}
public class PlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public bool canSpawn;
    public class PlayerSpawnerComponentBaker : Baker<PlayerSpawnerAuthoring>
    {
        public override void Bake(PlayerSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new PlayerSpawnerComponent
                {
                    player1 = GetEntity(authoring.player1, TransformUsageFlags.Dynamic),
                    player2 = GetEntity(authoring.player2, TransformUsageFlags.Dynamic),
                    canSpawn = authoring.canSpawn,
                });
        }
    }
}
