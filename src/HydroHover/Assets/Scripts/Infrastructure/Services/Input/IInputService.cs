using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 MoveInput { get; }
        float LiftInput { get; }
        bool HandbrakeInput { get; }
        bool PauseTriggered { get; }

        float SensitivityMultiplier { get; set; }

        void Enable();
        void Disable();
        
        InputActionAsset GetActionAsset();
        void LoadBindingOverrides(string jsonOverrides);
        string SaveBindingOverrides();
    }
}