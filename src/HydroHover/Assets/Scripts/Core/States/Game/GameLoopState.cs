using Core.States.Base;
using Infrastructure.Services.Window;
using UnityEngine;

namespace Core.States.Game
{
    public class GameLoopState : IState
    {
        private readonly IWindowService _windowService;

        public GameLoopState(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        public async void Enter()
        {
            Debug.Log("Entered GameLoopState");
            await _windowService.Open(WindowID.HUD);
        }

        public void Exit()
        {
        }
    }
}