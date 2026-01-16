using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks; // UniTask
using Data;
using Infrastructure.Services;
using Infrastructure.Services.Window;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        Task CreateScreen(string path, WindowID id);
        void DestroyScreen(WindowID id);
        T GetScreenComponent<T>(WindowID id) where T : Component;
        bool Exists(WindowID id);
    }

    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        
        private readonly Dictionary<WindowID, GameObject> _createdWindows = new();

        public UIFactory(DiContainer container)
        {
            _container = container;
        }

        public async Task CreateScreen(string path, WindowID id)
        {
            if (_createdWindows.ContainsKey(id)) return;
            
            var handle = Addressables.InstantiateAsync(path);
            GameObject windowInstance = await handle.ToUniTask();
            
            _container.InjectGameObject(windowInstance);

            _createdWindows[id] = windowInstance;
        }

        public void DestroyScreen(WindowID id)
        {
            if (_createdWindows.TryGetValue(id, out var window))
            {
                Addressables.ReleaseInstance(window);
                _createdWindows.Remove(id);
            }
        }

        public T GetScreenComponent<T>(WindowID id) where T : Component
        {
            if (_createdWindows.TryGetValue(id, out var window))
            {
                return window.GetComponent<T>();
            }
            return null;
        }

        public bool Exists(WindowID id) => _createdWindows.ContainsKey(id);
    }
}