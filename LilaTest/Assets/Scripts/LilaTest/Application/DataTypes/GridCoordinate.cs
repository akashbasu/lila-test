using System;
using UnityEngine;

namespace LilaTest
{
    [Serializable]
    internal struct GridCoordinate
    {
        public int Row => _row;
        public int Column => _column;

        [SerializeField] private int _row;
        [SerializeField] private int _column;

        public GridCoordinate(int row, int column)
        {
            _row = row;
            _column = column;
        }
    }
}