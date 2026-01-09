using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneManagement
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}