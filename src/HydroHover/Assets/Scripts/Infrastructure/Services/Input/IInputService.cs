using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 MoveInput { get; }
        float LiftInput { get; }
    
        bool HandbrakeInput { get; }

        bool PauseTriggered { get; }
        bool ResetTriggered { get; }
    }
}