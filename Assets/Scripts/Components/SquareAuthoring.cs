using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct SquareComponent : IComponentData
{
    public int state;
    public int rowID;
    public int colID;
    public bool isOccupied;
    public int point;
    public int basePoint;
}

public class SquareAuthoring : MonoBehaviour
{

}

public class SquareComponentBaker : Baker<SquareAuthoring>
    {
        public override void Bake(SquareAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity,
            new SquareComponent
            {
                state = (int)color.Empty,
                isOccupied = false,
                point = 0,
            });
        }
    }

