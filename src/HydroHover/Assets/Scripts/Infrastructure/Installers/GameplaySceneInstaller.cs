using Physics.Water;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private WaveSettings _waveSettings;
        [SerializeField] private WaterPhysicsSystem _waterSystem;

        public override void InstallBindings()
        {
            BindWaterSystem();
        }

        private void BindWaterSystem()
        {
            Container.BindInstance(_waveSettings);
            Container.BindInstance(_waterSystem).AsSingle();
        }
    }
}