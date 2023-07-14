using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine.Rendering;
using UnityEngine;

public struct MeshAndMaterialComponent : IComponentData
{
    public BatchMaterialID WhiteMaterialID;
    public BatchMaterialID GreyMaterialID;
    public BatchMaterialID RedMaterialID;
    public BatchMaterialID GreenMaterialID;
    public BatchMeshID meshID;
}
public class MeshAndMaterialAuthoring : MonoBehaviour
{
    public Material WhiteMaterial;
    public Material GreyMaterial;
    public Material RedMaterial;
    public Material GreenMaterial;
    public Mesh mesh;
    public class MeshAndMaterialComponentBaker : Baker<MeshAndMaterialAuthoring>
    {
        public override void Bake(MeshAndMaterialAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var hybridRender = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<EntitiesGraphicsSystem>();
            AddComponent(entity, new MeshAndMaterialComponent
            {
                WhiteMaterialID = hybridRender.RegisterMaterial(authoring.WhiteMaterial),
                GreyMaterialID = hybridRender.RegisterMaterial(authoring.GreyMaterial),
                RedMaterialID = hybridRender.RegisterMaterial(authoring.RedMaterial),
                GreenMaterialID = hybridRender.RegisterMaterial(authoring.GreenMaterial),
                meshID = hybridRender.RegisterMesh(authoring.mesh),
            });
        }
    }
}