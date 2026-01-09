using Infrastructure.Services.Input;
using Infrastructure.Services.SceneManagement;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputService();
        BindSceneLoaderService();
    }

    private void BindInputService()
    {
        Container.BindInterfacesTo<InputService>().AsSingle();
    }
    
    private void BindSceneLoaderService()
    {
        Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
    }
}