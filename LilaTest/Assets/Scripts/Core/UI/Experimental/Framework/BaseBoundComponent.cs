using System;
using Core.IoC;
using UnityEngine;

namespace Core.UI.Framework
{
    internal abstract class BaseBoundComponent<TModel> : MonoBehaviour, IBoundComponent where TModel : IBoundModel
    {
        [SerializeField] protected string _uiKey = string.Empty;
        [SerializeField] private bool _isRelative;

        [Dependency] protected readonly IUiDataRegistry _uiDataRegistry;

        private IUiDataRegistry UiDataRegistry
        {
            get
            {
                if (_uiDataRegistry == null)
                {
                    Injector.ResolveDependencies(this);
                }

                return _uiDataRegistry;
            }
        }
        

        private TModel _boundModel;
        private string _prefix = string.Empty;
        private string RelativeKey
        {
            get
            {
                if (string.IsNullOrEmpty(_prefix))
                {
                    _prefix = GetPrefix();
                }
                
                return $"{_prefix}.{_uiKey}";
            }
        }

        private string GetPrefix() => GetComponentInParent<IBoundComponent>().FullKey;
        
        public string FullKey => _isRelative ? RelativeKey : _uiKey;
        
        public virtual void ResetComponent()
        {
            BoundModel = default;
        }
        
        protected TModel BoundModel
        {
            get => _boundModel;
            private set
            {
                _boundModel = value;
                OnModelUpdated();
            }
        }
        
        protected virtual void Awake()
        {
            // Injector.ResolveDependencies(this);
        }
        
        protected virtual void OnEnable()
        {
            if(string.IsNullOrWhiteSpace(_uiKey)) return;

            UiDataRegistry.RegisterComponent(FullKey, this);
        }

        protected virtual void OnDisable()
        {
            if(string.IsNullOrWhiteSpace(_uiKey)) return;
            
            UiDataRegistry.UnRegisterComponent(FullKey, this);
        }

        protected void Reorient(string uiKey)
        {
            UiDataRegistry.UnRegisterComponent(FullKey, this);
            _prefix = string.Empty;
            _uiKey = uiKey;
            UiDataRegistry.RegisterComponent(FullKey, this);
        }

        protected abstract void OnModelUpdated();
        
        void IBoundComponent.OnDataSet(IBoundModel model)
        {
            UpdateData(model);
        }

        void IBoundComponent.OnDataUpdated(IBoundModel model)
        {
            UpdateData(model);
        }
        
        private void UpdateData(object data)
        {
            if(data == null) ResetComponent();
            else if (!(data is TModel boundModel)) throw new Exception($"{GetType()} on {gameObject.name} with key {_uiKey} has a model type mismatch");
            else
            {
                BoundModel = boundModel;
            }
        }
    }
}