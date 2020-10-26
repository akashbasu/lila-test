using Core.UI.Framework;
using UnityEngine;

namespace LilaTest
{
    internal class GridItemInfo : IBoundInfo
    {
        public int Id => _id;
        public Color Color => _color;

        private readonly int _id;
        private readonly Color _color;
        
        public GridItemInfo(int id, Color color)
        {
            _id = id;
            _color = color;
        }
    }
}