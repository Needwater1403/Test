using Unity.Entities;
using UnityEngine;

public partial struct PlayerTag : IComponentData
{

}

public class PlayerTagAuthoring : MonoBehaviour
{
    public class PlayerComponentBaker : Baker<PlayerTagAuthoring>
    {
        public override void Bake(PlayerTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,new PlayerTag{
                });
        }
    }
}



