using System;
using Core.EventSystems;
using Core.IoC;
using Core.UI.Framework;
using Core.UI.Models;

namespace LilaTest
{
    internal class GridUiDataProvider : IInitializable, IDisposable
    {
        [Dependency] private readonly IGameEventManager _gameEventManager;
        [Dependency] private readonly IUiDataRegistry _uiDataRegistry;
        [Dependency] private readonly IGridDataManager _gridDataManager;
        [Dependency] private readonly IConfigResourceProvider _configResourceProvider;

        private GridModel _gridModel;
        private GridCoordinate _currentSelection = new GridCoordinate(-1, -1);

        public void Initialize(Action<IInitializable> onComplete = null)
        {
            Injector.ResolveDependencies(this);
            
            CreateGridModel();
            SubscribeToEvents();
            
            onComplete?.Invoke(this);
        }

        public void Dispose()
        {
            _gameEventManager?.Unsubscribe(ApplicationEvents.Grid.OnSelect, OnSelect);
            _gameEventManager?.Unsubscribe(ApplicationEvents.Grid.Reset, OnReset);
        }

        private void CreateGridModel()
        {
            _uiDataRegistry.UpdateModel(new StringModel(UiKeys.Application.AoE, _configResourceProvider.Config.AreaOfInterest.ToString()));
            _gridModel = new GridModel(UiKeys.Grid.GridModel, _gridDataManager.Data);
            _uiDataRegistry.UpdateModel(_gridModel);
        }

        private void SubscribeToEvents()
        {
            _gameEventManager.Subscribe(ApplicationEvents.Grid.OnSelect, OnSelect);
            _gameEventManager.Subscribe(ApplicationEvents.Grid.Reset, OnReset);
        }

        private void OnSelect(object[] args)
        {
            if (args?.Length < 1) return;
            var dataKey = (string) args[0];
            var coordinate = _gridModel.GetCoordinate(dataKey);
            
            if(Equals(_currentSelection, coordinate)) return;
            
            ActivateForSelectedQuad(coordinate);
        }
        
        private void OnReset(object[] args)
        {
            _gridModel.DeactivateActiveElements();
        }

        private void ActivateForSelectedQuad(GridCoordinate coordinate)
        {
            _gridModel.DeactivateActiveElements();
            _gridModel.ActivateElements(GetAreaOfEffect(coordinate));
            _currentSelection = coordinate;
        }

        private Quad GetAreaOfEffect(GridCoordinate center)
        {
            var aoe = (int) _configResourceProvider.Config.AreaOfInterest;
            var centerRow = center.Row;
            var centerColumn = center.Column;
            return new Quad(new GridCoordinate(centerRow - aoe, centerColumn - aoe), 
                new GridCoordinate(centerRow - aoe, centerColumn + aoe),
                new GridCoordinate(centerRow + aoe, centerColumn - aoe),
                new GridCoordinate(centerRow + aoe, centerColumn + aoe));
        }
    }
}