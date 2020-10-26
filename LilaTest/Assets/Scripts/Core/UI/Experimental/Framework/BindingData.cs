using System.Collections.Generic;

namespace Core.UI.Framework
{
    internal class BindingData
    {
        private IBoundModel _model;
        private readonly HashSet<IBoundComponent> _boundComponents;
    
        public BindingData(IBoundModel model)
        {
            _model = model;
            _boundComponents = new HashSet<IBoundComponent>();
        }

        public void RemoveBoundObject(IBoundComponent boundComponent)
        {
            if(_boundComponents.Remove(boundComponent)) boundComponent.ResetComponent();
        }
    
        public void AddBoundObject(IBoundComponent boundComponent)
        {
            if(_boundComponents.Add(boundComponent)) boundComponent.OnDataSet(_model);
        }

        public void UpdateData(IBoundModel model)
        {
            _model = model;
            foreach (var boundComponent in _boundComponents) boundComponent.OnDataUpdated(_model);
        }
    }
}