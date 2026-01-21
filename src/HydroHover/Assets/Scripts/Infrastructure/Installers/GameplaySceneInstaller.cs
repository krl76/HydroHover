using Infrastructure.Factories;
using Infrastructure.Services.RaceManager;
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

        public override void InstallBindings()
        {
            BindWaterSystem();
            BindWindSystem();
            BindRaceSystem();
        }

        private void BindRaceSystem()
        {
            Container.Bind<IRaceManagerService>().To<RaceManagerService>().AsSingle();
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
    }
}