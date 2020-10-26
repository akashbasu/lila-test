using System;
using Core.IoC;
using Core.Random;
using JetBrains.Annotations;

namespace LilaTest
{
    internal interface IDeterministicRandomProvider : IInitializable
    {
        [CanBeNull] IRandomUtils RandomUtils { get; }
    }
    
    internal class DeterministicRandomProvider : IDeterministicRandomProvider
    {
        [Dependency] private readonly IConfigResourceProvider _configResourceProvider;

        private IRandomUtils _randomUtils;

        public IRandomUtils RandomUtils => _randomUtils;
        
        public void Initialize(Action<IInitializable> onComplete = null)
        {
            _randomUtils = new RandomUtils(_configResourceProvider.Config.Seed);
            
            onComplete?.Invoke(this);
        }
    }
}