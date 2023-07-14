using Unity.Entities;
using UnityEngine;

public partial struct SquareTag : IComponentData
{

}

public class SquareTagAuthoring : MonoBehaviour
{
    public class SquareComponentBaker : Baker<SquareTagAuthoring>
    {
        public override void Bake(SquareTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new SquareTag
                {
                });
        }
    }
}



