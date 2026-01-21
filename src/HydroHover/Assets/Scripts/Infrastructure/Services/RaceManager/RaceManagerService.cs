using System;
using System.Collections.Generic;
using Features.Trigger;
using UnityEngine;

namespace Infrastructure.Services.RaceManager
{
    public class RaceManagerService : IRaceManagerService
    {
        public event Action OnRaceStarted;
        public event Action OnRaceFinished;
        public event Action<int> OnCheckpointPassed;
        public event Action OnWrongCheckpoint;

        private List<CheckpointTrigger> _checkpoints = new();
        private int _currentIndex = 0;
        private float _startTime;
        private bool _isActive;

        public bool IsRaceActive => _isActive;
        public float CurrentTime => _isActive ? Time.time - _startTime : 0f;
        public int CurrentCheckpointIndex => _currentIndex;
        public int TotalCheckpoints => _checkpoints.Count;

        public Vector3? NextCheckpointPosition
        {
            get
            {
                if (_checkpoints == null || _checkpoints.Count == 0) return null;
                if (_currentIndex >= _checkpoints.Count) return null;
                return _checkpoints[_currentIndex].transform.position;
            }
        }

        public void RegisterTrack(List<CheckpointTrigger> checkpoints)
        {
            _checkpoints = checkpoints;
            
            for (int i = 0; i < _checkpoints.Count; i++)
            {
                var cp = _checkpoints[i];
                cp.Index = i;
                cp.OnPlayerEntered -= HandleCheckpointEnter;
                cp.OnPlayerEntered += HandleCheckpointEnter;
                cp.ResetState();
            }
        
            Debug.Log($"[RaceService] Track registered: {_checkpoints.Count} checkpoints.");
        }

        public void StartRace()
        {
            if (_checkpoints.Count == 0)
            {
                Debug.LogError("[RaceService] Cannot start race: No checkpoints!");
                return;
            }

            _currentIndex = 0;
            _startTime = Time.time;
            _isActive = true;
            
            foreach (var cp in _checkpoints) cp.ResetState();

            OnRaceStarted?.Invoke();
            Debug.Log("[RaceService] Race Started!");
        }

        public void FinishRace()
        {
            _isActive = false;
            OnRaceFinished?.Invoke();
            Debug.Log($"[RaceService] Finished! Time: {CurrentTime:F2}");
        }

        private void HandleCheckpointEnter(int index)
        {
            if (!_isActive) return;

            if (index == _currentIndex)
            {
                _currentIndex++;
                OnCheckpointPassed?.Invoke(index);

                if (_currentIndex >= _checkpoints.Count)
                {
                    FinishRace();
                }
            }
            else if (index > _currentIndex)
            {
                OnWrongCheckpoint?.Invoke();
                Debug.LogWarning($"Wrong Checkpoint! Need {_currentIndex}, got {index}");
            }
        }
    }
}