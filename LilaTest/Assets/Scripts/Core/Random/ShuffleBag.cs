using System;
using System.Collections.Generic;

namespace Core.Random
{
    internal class ShuffleBag
    {
        private readonly IRandomUtils _randomUtils;
        
        private readonly int _size;
        private List<int> _bag;

        internal ShuffleBag(int size, IRandomUtils currentRandomizer = null)
        {
            _randomUtils = currentRandomizer ?? new RandomUtils((int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
            
            _size = size;
            FillBag();
        }

        public int Next()
        {
            if (_size <= 0) return 0;
            
            if (_bag.Count == 0) FillBag();

            var index = _randomUtils.Next(0, _bag.Count);
            var val = _bag[index];
            _bag.RemoveAt(index);
            return val;
        }

        private void FillBag()
        {
            _bag = new List<int>(_size);
            for (var i = 0; i < _size; i++) _bag.Add(i);
        }
    }
}