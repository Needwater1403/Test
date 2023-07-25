using Unity.Entities;
using UnityEngine;

public partial struct MovableComponent : IComponentData
{
    public bool canMove;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool canMoveUp;
    public bool canMoveDown;
    public int minX;
    public int minY;
    public int maxX;
    public int maxY;
    public int state;
}

public class MovableAuthoring : MonoBehaviour
{
    public bool canMove;
    public int minX;
    public int minY;
    public int maxX;
    public int maxY;
    public class PlayerComponentBaker : Baker<MovableAuthoring>
    {
        public override void Bake(MovableAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MovableComponent
            {
                canMove = authoring.canMove,
                minX = authoring.minX,
                minY = authoring.minY,
                maxX = authoring.maxX,
                maxY = authoring.maxY,
                state = (int)dir.Stand,
            });
        }
    }
}