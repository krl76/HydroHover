using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService, IInitializable, System.IDisposable
    {
        private HoverControls _controls;
        
        public Vector2 MoveInput => _controls.Player.Move.ReadValue<Vector2>() * SensitivityMultiplier;
        public float LiftInput => _controls.Player.Lift.ReadValue<float>();
        public bool HandbrakeInput => _controls.Player.Handbrake.IsPressed();

        public bool PauseTriggered => _controls.Player.Pause.WasPressedThisFrame();

        public float SensitivityMultiplier { get; set; } = 1.0f;

        public void Enable() => _controls.Enable();

        public void Disable()
        {
            _controls.Disable();
        }

        public void Initialize()
        {
            _controls = new HoverControls();
            Enable();
        }

        public void Dispose()
        {
            _controls?.Dispose();
        }
        
        public InputActionAsset GetActionAsset()
        {
            return _controls.asset;
        }

        public void LoadBindingOverrides(string jsonOverrides)
        {
            if (string.IsNullOrEmpty(jsonOverrides)) return;
            _controls.asset.LoadBindingOverridesFromJson(jsonOverrides);
        }

        public string SaveBindingOverrides()
        {
            return _controls.asset.SaveBindingOverridesAsJson();
        }
    }
}