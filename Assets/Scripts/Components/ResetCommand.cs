using Unity.Entities;
using CortexDeveloper.ECSMessages.Components;

public struct ResetCommand : IComponentData, IMessageComponent
{
    public bool resetGame;
}
