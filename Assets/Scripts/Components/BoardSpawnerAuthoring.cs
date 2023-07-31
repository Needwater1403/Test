using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct BoardSpawnerComponent : IComponentData
{
    public Entity prefab;
    public bool canSpawn;
    public int num;
}

public class BoardSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public bool canSpawn;
    public int num;
    //public List<float3> pos;
    public class BoardSpawnerComponentBaker : Baker<BoardSpawnerAuthoring>
    {
        public override void Bake(BoardSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new BoardSpawnerComponent
                {
                    prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    num = authoring.num,
                    canSpawn = authoring.canSpawn,
                });

            //BlobAssetReference<SquarePosition> bar;
            //using (var bb = new BlobBuilder(Allocator.Temp))
            //{
            //    ref SquarePosition enp = ref bb.ConstructRoot<SquarePosition>();
            //    BlobBuilderArray<float3> positions = bb.Allocate(ref enp.value, authoring.num);
            //    for (int i = 0; i < authoring.num; i++)
            //    {
            //        positions[i] = authoring.pos.ElementAt(i);
            //    }
            //    bar = bb.CreateBlobAssetReference<SquarePosition>(Allocator.Persistent);
            //}
            //AddBlobAsset(ref bar, out var hash);
            //AddComponent(new SquarePositionAsset() { asset = bar });
        }
    }
}
