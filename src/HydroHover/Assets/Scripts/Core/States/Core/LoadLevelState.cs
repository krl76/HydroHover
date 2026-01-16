using Core.States.Base;
using Core.States.Game;
using Infrastructure.Services.SceneManagement;

namespace Core.States.Core
{
    public class LoadLevelState : IPayloaded<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoaderService sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}