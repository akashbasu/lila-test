using System;
using Core.UI.Models;
using UnityEngine.UI;

namespace LilaTest
{
    internal class GridModel : ComplexModel
    {
        private readonly GridInfo _gridInfo;
        private readonly GridItemModel[,] _gridItemModels;
        private readonly Quad _gridDimensions;
        
        private Quad _activatedQuad;

        public GridLayoutGroup.Constraint Constraint => GridLayoutGroup.Constraint.FixedColumnCount;
        public int Columns => _gridInfo.Dimensions.Column;
        public int Rows => _gridInfo.Dimensions.Row;
        public int GridCount => _gridInfo.GridItems.Length;
        
        public GridModel(string key, GridInfo gridInfo) : base(key)
        {
            _gridInfo = gridInfo;
            
            _gridDimensions = new Quad(new GridCoordinate(0, 0), 
                new GridCoordinate(0, Columns - 1), 
                new GridCoordinate(Rows - 1, 0), 
                new GridCoordinate(Rows - 1, Columns - 1));
            
            _gridItemModels = new GridItemModel[Rows,Columns];
            
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            _gridDimensions.ForEach((rows, columns) =>
            {
                var itemInfo = _gridInfo.GridItems[rows, columns];
                var subModel = new GridItemModel(itemInfo.Id.ToString(), itemInfo);
                AddSubModel(subModel);
                _gridItemModels[rows, columns] = subModel;
                return true;
            });
        }

        public GridCoordinate GetCoordinate(string dataKey)
        {
            GridCoordinate foundCoordinate = default;
            _gridDimensions.ForEach((rows, columns) =>
            {
                var gridItemModel = _gridItemModels[rows, columns];
                if (!string.Equals(gridItemModel.Key, dataKey)) return true;
                
                foundCoordinate = new GridCoordinate(rows, columns);
                return false;

            });

            return foundCoordinate;
        }

        public void DeactivateActiveElements()
        {
            _activatedQuad.ForEach((row, column) =>
            {
                _gridItemModels[row, column].UnSelect();
                UpdateSubModel(_gridItemModels[row, column]);
                return true;
            });
        }

        public void ActivateElements(Quad aoeQuad)
        {
            var validQuad = new Quad(new GridCoordinate(Math.Max(aoeQuad.TopLeft.Row, 0), Math.Max(aoeQuad.TopLeft.Column, 0)), 
                new GridCoordinate(Math.Max(aoeQuad.TopRight.Row, 0), Math.Min(aoeQuad.TopRight.Column, Columns - 1)), 
                new GridCoordinate(Math.Min(aoeQuad.BottomLeft.Row, Rows - 1), Math.Max(aoeQuad.BottomLeft.Column, 0)), 
                new GridCoordinate(Math.Min(aoeQuad.BottomRight.Row, Rows - 1), Math.Min(aoeQuad.BottomRight.Column, Columns - 1)));

            ActivateQuad(validQuad);
        }

        private void ActivateQuad(Quad validQuad)
        {
            _activatedQuad = validQuad;
            
            _activatedQuad.ForEach((row, column) =>
            {
                _gridItemModels[row, column].Select();
                UpdateSubModel(_gridItemModels[row, column]);
                return true;
            });
        }
    }
}