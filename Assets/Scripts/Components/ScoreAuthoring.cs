using Unity.Entities;
using UnityEngine;

public partial struct ScoreComponent : IComponentData
{
    public int p1_Score;
    public int p2_Score;
}

public class ScoreAuthoring : MonoBehaviour
{
    public class ScoreComponentBaker : Baker<ScoreAuthoring>
    {
        public override void Bake(ScoreAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ScoreComponent
            {   
                p1_Score = 0,
                p2_Score = 0,
            });
        }
    }
}



