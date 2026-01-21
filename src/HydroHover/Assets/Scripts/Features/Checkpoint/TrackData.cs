using System.Linq;
using Features.Trigger;
using Infrastructure.Services.RaceManager;
using UnityEngine;
using Zenject;

namespace Features.Checkpoint
{
    public class TrackData : MonoBehaviour
    {
        private IRaceManagerService _raceManagerService;

        [Inject]
        public void Construct(IRaceManagerService raceManagerService)
        {
            _raceManagerService = raceManagerService;
        }

        private void Start()
        {
            if (_raceManagerService == null)
            {
                var sceneContext = FindObjectOfType<SceneContext>();
                if (sceneContext != null)
                {
                    sceneContext.Container.Inject(this);
                }
            }
            
            var checkpoints = GetComponentsInChildren<CheckpointTrigger>().ToList();
            
            _raceManagerService.RegisterTrack(checkpoints);
            
            _raceManagerService.StartRace(); 
        }
    }
}