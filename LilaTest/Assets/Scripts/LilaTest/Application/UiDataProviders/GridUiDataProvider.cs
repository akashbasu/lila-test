using System;
using Core.IoC;
using Core.UI.Framework;

namespace LilaTest
{
    internal class GridUiDataProvider : IInitializable
    {
        [Dependency] private readonly IUiDataRegistry _uiDataRegistry;
        [Dependency] private readonly IGridDataManager _gridDataManager;

        public void Initialize(Action<IInitializable> onComplete = null)
        {
            Injector.ResolveDependencies(this);
            
            CreateGridModel();
            
            onComplete?.Invoke(this);
        }

        private void CreateGridModel()
        {
            _uiDataRegistry.UpdateModel(new GridModel("Grid", _gridDataManager.Data));
        }
    }
}