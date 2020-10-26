using Core.UI.Framework;
using UnityEngine;

namespace LilaTest
{
    internal class GridItemModel : IBoundModel
    {
        public string Key { get; private set; }
        public void OverrideKey(string key) => Key = key;

        public bool IsSelected => _isSelected;
        public int Id => _itemInfo.Id;
        public Color Color => _itemInfo.Color;

        private readonly GridItemInfo _itemInfo;
        private bool _isSelected;
        
        public GridItemModel(string key, GridItemInfo itemInfo)
        {
            Key = key;
            _itemInfo = itemInfo;
        }

        public void Select()
        {
            _isSelected = true;
        }
        
        public void UnSelect()
        {
            _isSelected = false;
        }
    }
}