using System;
using System.Collections.Generic;
using Core.IoC;

namespace Core.UI.Framework
{
    internal interface IUiDataRegistry : IInitializable
    {
        void UpdateModel(IBoundModel model);
        void RegisterComponent(string key, IBoundComponent boundComponent);
        void UnRegisterComponent(string key, IBoundComponent boundComponent);
    }
    
    internal class UiDataRegistry : IUiDataRegistry
    {
        private readonly Dictionary<string, BindingData> _directory = new Dictionary<string, BindingData>();

        public void Initialize(Action<IInitializable> onComplete = null)
        {
            _directory.Clear();
            
            onComplete?.Invoke(this);
        }
    
        public void UpdateModel(IBoundModel model)
        {
            var key = model.Key;
            if (_directory.TryGetValue(key, out var boundObjectData))
            {
                boundObjectData.UpdateData(model);
            }
            else
            {
                _directory[key] = new BindingData(model);
            }
        }
        
        public void RegisterComponent(string key, IBoundComponent boundComponent)
        {
            if (_directory.TryGetValue(key, out var boundObjectData))
            {
                boundObjectData.RemoveBoundObject(boundComponent);
            }
            else
            {
                _directory[key] = new BindingData(default);
            }
            
            _directory[key].AddBoundObject(boundComponent);
        }
        
        public void UnRegisterComponent(string key, IBoundComponent boundComponent)
        {
            if(!_directory.ContainsKey(key)) return;
            
            _directory[key].RemoveBoundObject(boundComponent);
        }

        
    }
}