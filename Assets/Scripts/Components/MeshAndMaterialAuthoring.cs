using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine.Rendering;
using UnityEngine;

public struct MeshAndMaterialComponent : IComponentData
{
    public BatchMaterialID materialID;
    public BatchMeshID meshID;
}
public class MeshAndMaterialAuthoring : MonoBehaviour
{
    public Material material;
    public Mesh mesh;
    public class MeshAndMaterialComponentBaker : Baker<MeshAndMaterialAuthoring>
    {
        public override void Bake(MeshAndMaterialAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var hybridRender = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<EntitiesGraphicsSystem>();
            AddComponent(entity, new MeshAndMaterialComponent
            {
                materialID = hybridRender.RegisterMaterial(authoring.material),
                meshID = hybridRender.RegisterMesh(authoring.mesh),
            });
        }
    }
}