using System;

namespace Core.IoC
{
    internal interface IInitializable
    {
        void Initialize(Action<IInitializable> onComplete = null);
    }
}