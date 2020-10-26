using System;
using Core.AssetManagement;
using Core.IoC;
using UnityEngine;

namespace LilaTest
{
    internal interface IConfigResourceProvider : IInitializable
    {
        ApplicationConfig Config { get; }
    }
    
    internal class ConfigResourceProvider : BaseResourcesDataManager<ApplicationConfig>, IConfigResourceProvider
    {
        private const int Index = 0;
        
        public ApplicationConfig Config => _data[Index];
        
        public override void PostConstruct(params object[] args)
        {
            LoadData();
        }
        
        public void Initialize(Action<IInitializable> onComplete = null)
        {
            LoadData();
            
            onComplete?.Invoke(this);
        }

        private void LoadData()
        {
            base.PostConstruct();

            if (_data.Length > 1)
            {
                Debug.LogWarning($"[{nameof(ConfigResourceProvider)}] Multiple application configurations loaded. Selecting first.");
            }
        }

        protected override string DataPath => ApplicationConstants.DataPaths.ConfigPath;
    }
    
    internal static partial class ApplicationConstants
    {
        internal static partial class DataPaths
        {
            public const string ConfigPath = "Config";
        }
    }
}