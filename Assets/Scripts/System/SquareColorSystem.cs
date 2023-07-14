using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;


public partial struct SquareColorSystem : ISystem
{
    //public void OnCreate(ref SystemState state)
    //{
    //    state.RequireForUpdate<StartCommand>();
    //}
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (m, squ, en) in SystemAPI.Query<RefRW<MeshAndMaterialComponent>, RefRW<SquareComponent>>().WithEntityAccess())
        {
            switch (squ.ValueRW.state)
            {
                case 0:
                   {
                        //ecb.SetComponent(en, new MaterialMeshInfo
                        //{
                        //    MaterialID = m.ValueRO.whiteMaterialID,
                        //    MeshID = m.ValueRO.meshID,
                        //});
                        break;
                    }
                case 1:
                    {
                        //ecb.SetComponent(en, new MaterialMeshInfo
                        //{
                        //    MaterialID = m.ValueRO.greyMaterialID,
                        //    MeshID = m.ValueRO.meshID,
                        //});
                        break;
                    }
                case 2:
                    {
                        //ecb.SetComponent(en, new MaterialMeshInfo
                        //{
                        //    MaterialID = m.ValueRO.redMaterialID,
                        //    MeshID = m.ValueRO.meshID,
                        //});
                        break;
                    }
                case 3:
                    {
                        
                        ecb.SetComponent(en, new MaterialMeshInfo
                        {
                            MaterialID = m.ValueRO.materialID,
                            MeshID = m.ValueRO.meshID,
                        });
                        break;
                    }
            }
             
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
