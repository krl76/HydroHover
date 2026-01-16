using Core.States.Base;
using Data;
using Infrastructure.Services.SceneManagement;

namespace Core.States.MainMenu
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoaderService _sceneLoader;

        public MainMenuState(ISceneLoaderService sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(ScenesPaths.MAIN_MENU);
        }

        public void Exit()
        {
        }
    }
}