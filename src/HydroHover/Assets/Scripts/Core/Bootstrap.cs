using Data;
using Infrastructure.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoader;
        
        [Inject]
        public void Construct(ISceneLoaderService sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Debug.Log("Bootstrap: Services Initialized. Loading Gameplay...");
            
            _sceneLoader.LoadScene(ScenesPaths.GAMEPLAY);
        }
    }
}