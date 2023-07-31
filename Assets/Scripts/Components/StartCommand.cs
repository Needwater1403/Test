using Unity.Entities;
using CortexDeveloper.ECSMessages.Components;

public struct StartCommand : IComponentData, IMessageComponent
{
    public bool startGame;
}
