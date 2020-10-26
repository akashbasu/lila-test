using System;
using Core.DataPropagator;
using Core.IoC;

namespace LilaTest
{
    internal interface IGridDataManager : IInitializable, IDataPropagator<GridInfo> { }
    
    internal class GridDataManager : IGridDataManager
    {
        [Dependency] private readonly IConfigResourceProvider _configResource;
        [Dependency] private readonly IDeterministicRandomProvider _deterministicRandomProvider;
        
        private GridInfo _info;
        
        public event Updated<GridInfo> OnUpdated;
        
        public GridInfo Data
        {
            get => _info;
            private set
            {
                _info = value;
                OnUpdated?.Invoke(_info);
            }
        }
        
        public void Initialize(Action<IInitializable> onComplete = null)
        {
            GenerateGridInfo();
            
            onComplete?.Invoke(this);
        }

        private void GenerateGridInfo()
        {
            Data = new GridInfo(GenerateGrid());
        }
        
        private GridItemInfo[,] GenerateGrid()
        {
            var config = _configResource.Config;
            var rows = config.GridDimensions.Row;
            var columns = config.GridDimensions.Column;
            
            var grid = new GridItemInfo[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    grid[i, j] = new GridItemInfo(i * columns + j + 1, _deterministicRandomProvider.RandomUtils.NextColor());
                }
            }

            return grid;
        }
    }
}