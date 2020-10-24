using System;
using Core.EventSystems;
using Core.IoC;
using UnityEngine;

namespace Core
{
    internal abstract class WaitForEventState : IInitializable
    {
        [Dependency] protected readonly IGameEventManager _gameEventManager;
        
        private Action<IInitializable> _onComplete;

        public void Initialize(Action<IInitializable> onComplete = null)
        {
            Injector.ResolveDependencies(this);
            
            StartWait(onComplete);
        }
        
        protected void StartWait(Action<IInitializable> onComplete)
        {
            _onComplete = onComplete;
            _gameEventManager.Subscribe(EndEvent, OnWaitComplete);
        }
        
        protected abstract string EndEvent { get; }

        private void OnWaitComplete(object[] obj)
        {
            _gameEventManager.Unsubscribe(EndEvent, OnWaitComplete);
            
            Debug.Log($"[{GetType().Name}] {nameof(OnWaitComplete)}");
            
            _onComplete?.Invoke(this);
            _onComplete = null;
        }
    }
}