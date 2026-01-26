using System.Collections.Generic;
using Infrastructure.Services.Settings;
using Infrastructure.Services.Input;
using Infrastructure.Services.Window;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace UI.Settings
{
    public class SettingsWindow : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _sensitivitySlider;

        [Header("Rebinding Generation")]
        [SerializeField] private Transform _controlsContainer;
        [SerializeField] private RebindButton _rebindPrefab;

        [Header("Main")]
        [SerializeField] private Button _closeButton;

        private ISettingsService _settingsService;
        private IWindowService _windowService;
        private IInputService _inputService;
        
        private List<GameObject> _spawnedButtons = new List<GameObject>();

        [Inject]
        public void Construct(ISettingsService settingsService, IWindowService windowService, IInputService inputService)
        {
            _settingsService = settingsService;
            _windowService = windowService;
            _inputService = inputService;
        }

        private void Start()
        {
            InitializeValues();
            GenerateRebindUI();
            SubscribeEvents();
        }

        private void GenerateRebindUI()
        {
            foreach (var btn in _spawnedButtons) Destroy(btn);
            _spawnedButtons.Clear();

            InputActionAsset asset = _inputService.GetActionAsset();
            InputActionMap map = asset.FindActionMap("Player");

            if (map == null) return;

            foreach (var action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    InputBinding binding = action.bindings[i];
                    
                    if (binding.isComposite) continue;
                    
                    string displayName = action.name;
                    
                    if (binding.isPartOfComposite)
                    {
                        displayName += $" {binding.name.ToUpper()}";
                    }
                    
                    RebindButton item = Instantiate(_rebindPrefab, _controlsContainer);
                    item.Setup(action, i, displayName);
                    
                    _spawnedButtons.Add(item.gameObject);
                }
            }
        }
        
        private void InitializeValues()
        {
            _muteToggle.isOn = _settingsService.IsMuted;
            _masterSlider.value = _settingsService.MasterVolume;
            _musicSlider.value = _settingsService.MusicVolume;
            _sfxSlider.value = _settingsService.SFXVolume;
            _sensitivitySlider.value = _settingsService.Sensitivity;
        }

        private void SubscribeEvents()
        {
            _muteToggle.onValueChanged.AddListener(val => _settingsService.IsMuted = val);
            _masterSlider.onValueChanged.AddListener(val => _settingsService.MasterVolume = val);
            _musicSlider.onValueChanged.AddListener(val => _settingsService.MusicVolume = val);
            _sfxSlider.onValueChanged.AddListener(val => _settingsService.SFXVolume = val);
            _sensitivitySlider.onValueChanged.AddListener(val => _settingsService.Sensitivity = val);
            _closeButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            _settingsService.Save();
            _windowService.Open(WindowID.MainMenu);
            _windowService.Close(WindowID.Settings);
        }
    }
}