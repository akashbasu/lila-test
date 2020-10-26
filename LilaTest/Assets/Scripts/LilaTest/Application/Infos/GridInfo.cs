using Core.UI.Framework;

namespace LilaTest
{
    internal class GridInfo : IBoundInfo
    {
        private readonly GridItemInfo[,] _grid;
        private readonly GridCoordinate _dimensions;

        public GridCoordinate Dimensions => _dimensions;
        public GridItemInfo[,] GridItems => _grid;
        
        public GridInfo(GridItemInfo[,] grid)
        {
            _grid = grid;
            _dimensions = new GridCoordinate(grid.GetLength(0), grid.GetLength(1));
        }
    }
}