using System.Collections.Generic;
using Core.Command;
using Core.IoC;
using UnityEngine;

namespace Core.GameStateMachine
{ 
    internal abstract class GameStateController : MonoBehaviour
    {
        private readonly Queue<IInitializable> _steps = new Queue<IInitializable>();

        protected abstract void AddOneTimeStates();
        
        protected abstract void AddGameStates();
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _steps.Clear();
            AddOneTimeStates();
            AddGameStates();
            
            NextState();
        }

        private void OnApplicationQuit()
        {
            DependencyRegistry.Reset();
        }

        private void NextState()
        {
            if(_steps.Count == 0) AddGameStates();
            ProcessState(_steps.Peek());
       }

        private void ProcessState(IInitializable step)
        {
            Debug.Log($"[{nameof(GameStateController)}] {nameof(ProcessState)} {step.GetType().Name}");
            new EventCommand(CoreEvents.GameStateMachine.Start, step.GetType().Name).Execute();
            step.Initialize(OnStepComplete);
        }

        private void OnStepComplete(IInitializable initializable)
        {
            if(_steps.Peek() != initializable) return;
            
            _steps.Dequeue();
            Debug.Log($"[{nameof(GameStateController)}] {nameof(OnStepComplete)} Completed {initializable.GetType().Name}");
            new EventCommand(CoreEvents.GameStateMachine.End, initializable.GetType().Name).Execute();
            NextState();
        }
    }
}