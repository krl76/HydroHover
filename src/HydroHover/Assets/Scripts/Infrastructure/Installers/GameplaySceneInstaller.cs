using Physics.Enviroment;
using Physics.Hover;
using Physics.Water;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private WaveSettings _waveSettings;
        [SerializeField] private WaterPhysicsSystem _waterSystem;
        [SerializeField] private WindSystem _windSystem;
        [SerializeField] private HoverController _hoverController;

        public override void InstallBindings()
        {
            BindWaterSystem();
            BindWindSystem();
            BindHoverSystem();
        }

        private void BindWaterSystem()
        {
            Container.BindInstance(_waveSettings);
            Container.BindInstance(_waterSystem).AsSingle();
        }
        
        private void BindWindSystem()
        {
            Container.BindInstance(_windSystem).AsSingle();
        }
        
        private void BindHoverSystem()
        {
            Container.BindInstance(_hoverController).AsSingle();
        }
    }
}