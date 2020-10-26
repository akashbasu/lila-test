using System;

namespace LilaTest
{
    internal readonly struct Quad
    {
        public GridCoordinate TopLeft { get; }
        public GridCoordinate TopRight { get; }
        public GridCoordinate BottomLeft { get; }
        public GridCoordinate BottomRight { get; }

        public Quad(GridCoordinate topLeft, GridCoordinate topRight, GridCoordinate bottomLeft, GridCoordinate bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
        
        public void ForEach(Func<int, int, bool> callback)
        {
            if (callback == null) return;
            
            for (var i = TopLeft.Row ; i <= BottomLeft.Row; i++)
            {
                for (var j = TopLeft.Column; j <= TopRight.Column; j++)
                {
                    if(!callback(i, j)) return;
                }
            }
        }
    }
}