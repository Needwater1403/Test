using Unity.Entities;
using UnityEngine;

public partial struct Player2Tag : IComponentData
{

}

public class Player2TagAuthoring : MonoBehaviour
{
    public class Player2ComponentBaker : Baker<Player2TagAuthoring>
    {
        public override void Bake(Player2TagAuthoring authoring) 
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player2Tag{
                });
        }
    }
}



