using Unity.Entities;
using UnityEngine;

public partial struct WinStatus : IComponentData
{
    public int status;
}
public class WinStatusAuthoring : MonoBehaviour
{

    public class WinStatusBaker : Baker<WinStatusAuthoring>
    {
        public override void Bake(WinStatusAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new WinStatus
            {
                status = 10,
            });
        }
    }
}
