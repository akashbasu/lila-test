using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataProviders;
using Core.EventSystems;
using Core.GameStateMachine;
using Core.IoC;
using UnityEngine;

namespace Core.UI.StateControl
{
    internal interface IUiStateController : IPostConstructable {}
    
    internal class UiStateController : IUiStateController
    {
        [Dependency] private readonly IGameEventManager _gameEventManager;
        [Dependency] private readonly ISceneReferenceProvider _sceneReferenceProvider;
        
        private GameObject _root;

        private Dictionary<string, List<GameObject>> _gameStateToUiMap;
        
        public void PostConstruct(params object[] args)
        {
            if (GetReferences())
            {
                _gameEventManager.Subscribe(CoreEvents.GameStateMachine.Start, OnGameStateStart);
                _gameEventManager.Subscribe(CoreEvents.GameStateMachine.End, OnGameStateEnd);
                return;
            }
            
            Debug.LogError($"[{nameof(UiStateController)}] {nameof(PostConstruct)} Failed to find references!");
        }

        public void Dispose()
        {
            _gameEventManager.Unsubscribe(CoreEvents.GameStateMachine.Start, OnGameStateStart);
            _gameEventManager.Unsubscribe(CoreEvents.GameStateMachine.End, OnGameStateEnd);
        }

        private bool GetReferences()
        {
            if (!_sceneReferenceProvider.TryGetEntry(Tags.UiRoot, out var uiRoot)) return false;

            _root = uiRoot;
            _gameStateToUiMap = new Dictionary<string, List<GameObject>>(_root.transform.childCount);

            foreach (Transform child in _root.transform)
            {
                var objectsForState = new List<GameObject>(child.childCount);
                objectsForState.AddRange(from Transform stateObjects in child select stateObjects.gameObject);
                _gameStateToUiMap.Add(child.name, objectsForState);
            }

            return _gameStateToUiMap.Count > 0;
        }
        
        private void OnGameStateEnd(object[] obj)
        {
            DisableAllObjects();
        }

        private void OnGameStateStart(object[] obj)
        {
            if (obj?.Length < 1) return;
            
            var currentState = (string) obj[0]; 
            if (_gameStateToUiMap.ContainsKey(currentState))
            {
                DisableAllObjects();
                EnableCurrent(currentState);
            }
            else
            {
                DisableAllObjects();
                Debug.LogError($"[{nameof(UiStateController)}] {nameof(OnGameStateStart)} No UI state found for {currentState}");
            }
        }
        
        private void EnableCurrent(string currentState)
        {
            
            foreach (var stateObject in _gameStateToUiMap[currentState])
            {
                stateObject.SetActive(true);
            }
        }
        
        private void DisableAllObjects()
        {
            foreach (var gameObject in _gameStateToUiMap.Values.SelectMany(gameObjectsForState => gameObjectsForState))
            {
                gameObject.SetActive(false);
            }
        }
    }
}