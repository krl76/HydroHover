using System;
using System.Collections;
using Infrastructure.Services.Player;
using Infrastructure.Services.RaceManager;
using Physics.Hover;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.HUD
{
    public class HUDWindow : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _checkpointText;
        [SerializeField] private TextMeshProUGUI _fpsText;
        
        [Header("Speedometer")]
        [SerializeField] private RectTransform _speedNeedle;
        [SerializeField] private float _minSpeed = 0f;
        [SerializeField] private float _maxSpeed = 200f;
        [SerializeField] private float _minAngle = 135f;
        [SerializeField] private float _maxAngle = -135f;
        
        [Header("Bars")]
        [SerializeField] private Image _liftBar;
        [SerializeField] private Image _thrustBar;

        private IPlayerService _playerService;
        private IRaceManagerService _raceManagerService;

        private HoverController _hoverController;

        private float _fpsCount;

        [Inject]
        public void Construct(IPlayerService playerService, IRaceManagerService raceManagerService)
        {
            _playerService = playerService;
            _raceManagerService = raceManagerService;
        }

        private void Start()
        {
            UpdateGameMetrics();
        }

        private void Update()
        {
            UpdatePhysics();
            UpdateRaceInfo();
        }

        private void UpdatePhysics()
        {
            if (!_playerService.IsPlayerCreated) return;
            if (_hoverController == null)
            {
                _hoverController = _playerService.Transform.gameObject.GetComponent<HoverController>();
                return;
            }
            
            var rb = _hoverController.Rb;
            float speedKmh = rb.linearVelocity.magnitude * 3.6f;
            _speedText.text = $"{speedKmh:F0} km/h";
            
            float t = Mathf.InverseLerp(_minSpeed, _maxSpeed, speedKmh);
            
            float angle = Mathf.Lerp(_minAngle, _maxAngle, t);
            
            _speedNeedle.localRotation = Quaternion.Euler(0, 0, angle);
            
            var lift = _hoverController.LiftEngine;
            var thrust = _hoverController.ThrustEngine;

            if (_liftBar) _liftBar.fillAmount = lift.CurrentRPM / lift.MaxRPM;
            if (_thrustBar) _thrustBar.fillAmount = thrust.CurrentRPM / thrust.MaxRPM;
        }

        private void UpdateRaceInfo()
        {
            float t = _raceManagerService.CurrentTime;
            int minutes = (int)(t / 60);
            int seconds = (int)(t % 60);
            int milliseconds = (int)((t * 100) % 100);
            _timerText.text = $"{minutes:00}:{seconds:00}.{milliseconds:00}";
            
            _checkpointText.text = $"{_raceManagerService.CurrentCheckpointIndex} / {_raceManagerService.TotalCheckpoints}";

            _fpsText.text = $"FPS: {Mathf.Round(_fpsCount)}";
        }

        private IEnumerator UpdateGameMetrics()
        {
            while (true)
            {
                _fpsCount = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}