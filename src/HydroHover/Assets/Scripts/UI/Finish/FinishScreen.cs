using Core.States.Base;
using Core.States.Core;
using Core.States.MainMenu;
using Data;
using Infrastructure.Services.RaceManager;
using Infrastructure.Services.Window;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Finish
{
    public class FinishScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _bestTimeText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        private IRaceManagerService _raceService;
        private GameStateMachine _stateMachine;
        private IWindowService _windowService;

        [Inject]
        public void Construct(IRaceManagerService raceService, GameStateMachine stateMachine, IWindowService windowService)
        {
            _raceService = raceService;
            _stateMachine = stateMachine;
            _windowService = windowService;
        }

        private void Start()
        {
            float time = _raceService.CurrentTime;
            _timeText.text = $"Time: {FormatTime(time)}";
            
            _restartButton.onClick.AddListener(OnRestartClicked);
            _menuButton.onClick.AddListener(OnMenuClicked);
        }

        private void OnRestartClicked()
        {
            _windowService.Close(WindowID.Finish);
            
            _stateMachine.Enter<LoadLevelState, string>(ScenesPaths.GAMEPLAY);
        }

        private void OnMenuClicked()
        {
            _windowService.Close(WindowID.Finish);
            _stateMachine.Enter<MainMenuState>();
        }

        private string FormatTime(float t)
        {
            int minutes = (int)(t / 60);
            int seconds = (int)(t % 60);
            int milliseconds = (int)((t * 100) % 100);
            return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
        }
    }
}