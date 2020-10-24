using System;

namespace Core.IoC
{
    internal interface IPostConstructable : IDisposable
    {
        void PostConstruct(params object[] args);
    }
}