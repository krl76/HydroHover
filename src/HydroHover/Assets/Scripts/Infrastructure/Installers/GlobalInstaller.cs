using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.Window;
using Zenject;

namespace Infrastructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoreSystems();
            BindFactories();
        }
    
        private void BindFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
    
        private void BindCoreSystems()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
        }
    }
}