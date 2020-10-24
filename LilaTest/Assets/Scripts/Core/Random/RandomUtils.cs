using Core.DataTypes;

namespace Core.Random
{
    internal interface IRandomUtils
    {
        int Next();
        int Next(int min, int max);
        double NextNormalized();
        int Next(IntRangedValue val);
        bool NextBool(float normalizedProbability);
    }
    
    internal class RandomUtils : IRandomUtils
    {
        private readonly System.Random _random;
        
        public int Next() => _random.Next();
        public int Next(int min, int max) => _random.Next(min, max);
        public double NextNormalized() => _random.NextDouble();
        public int Next(IntRangedValue val) => _random.Next(val.Min, val.Max);
        public bool NextBool(float normalizedProbability) => NextNormalized() <= normalizedProbability;

        public RandomUtils(int seed)
        {
            _random = new System.Random(seed);
        }
    }
}