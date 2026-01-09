using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService, IInitializable, System.IDisposable
    {
        private HoverControls _controls;
        
        public Vector2 MoveInput => _controls.Player.Move.ReadValue<Vector2>();
        public float LiftInput => _controls.Player.Lift.ReadValue<float>();
        public bool HandbrakeInput => _controls.Player.Handbrake.IsPressed();

        public bool PauseTriggered => _controls.Player.Pause.WasPressedThisFrame();
        public bool ResetTriggered => _controls.Player.Reset.WasPressedThisFrame();

        public void Initialize()
        {
            _controls = new HoverControls();
            _controls.Enable();
            Debug.Log("[InputService] Initialized and Actions Enabled");
        }

        public void Dispose()
        {
            _controls.Disable();
            _controls.Dispose();
            Debug.Log("[InputService] Disposed");
        }
    }
}