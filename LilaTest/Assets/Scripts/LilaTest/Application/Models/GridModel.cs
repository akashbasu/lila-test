using Core.UI.Models;
using UnityEngine.UI;

namespace LilaTest
{
    internal class GridModel : ComplexModel
    {
        private readonly GridInfo _gridInfo;
        private GridItemModel[,] _gridItemModels;
        
        public GridLayoutGroup.Constraint Constraint => GridLayoutGroup.Constraint.FixedColumnCount;
        public int Column => _gridInfo.Dimensions.Column;
        public int Rows => _gridInfo.Dimensions.Row;
        public int GridCount => _gridInfo.GridItems.Length;
        
        public GridModel(string key, GridInfo gridInfo) : base(key)
        {
            _gridInfo = gridInfo;

            CreateSubModels();
        }

        private void CreateSubModels()
        {
            _gridItemModels = new GridItemModel[Rows,Column];
                
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    var itemInfo = _gridInfo.GridItems[i, j];
                    var subModel = new GridItemModel(itemInfo.Id.ToString(), itemInfo);
                    AddSubModel(subModel);
                    _gridItemModels[i, j] = subModel;
                }
            }
        }
    }
}