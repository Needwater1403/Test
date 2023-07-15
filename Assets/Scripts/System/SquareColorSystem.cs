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
                        ecb.SetComponent(en, new MaterialMeshInfo
                        {
                            MaterialID = m.ValueRO.WhiteMaterialID,
                            MeshID = m.ValueRO.meshID,
                        });
                        break;
                    }
                case 1:
                    {
                        ecb.SetComponent(en, new MaterialMeshInfo
                        {
                            MaterialID = m.ValueRO.GreyMaterialID,
                            MeshID = m.ValueRO.meshID,
                        });
                        squ.ValueRW.isOccupied = true;
                        break;
                    }
                case 2:
                    {
                        ecb.SetComponent(en, new MaterialMeshInfo
                        {
                            MaterialID = m.ValueRO.RedMaterialID,
                            MeshID = m.ValueRO.meshID,
                        });
                        squ.ValueRW.isOccupied = true;
                        break;
                    }
                case 3:
                    {
                        
                        ecb.SetComponent(en, new MaterialMeshInfo
                        {
                            MaterialID = m.ValueRO.GreenMaterialID,
                            MeshID = m.ValueRO.meshID,
                        });
                        squ.ValueRW.isOccupied = true;
                        break;
                    }
            }
             
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
