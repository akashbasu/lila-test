using System;
using Core.IoC;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.AssetManagement
{
    internal abstract class BaseResourcesDataManager<TData> : IPostConstructable where TData : Object
    {
        protected TData[] _data;
        protected abstract string DataPath { get; }

        public virtual void PostConstruct(params object[] args)
        {
            var resourceLoader = new ResourcesLoader<TData>(DataPath);
            if (!resourceLoader.TryLoadData(out _data))
            {
                Debug.LogException(new Exception($"[{GetType()}] failed to load data."));
            }
        }

        public virtual void Dispose()
        {
            _data = null;
        }
    }
}