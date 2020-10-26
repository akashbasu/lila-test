using System;

namespace Core.IoC
{
    internal interface ILifecycleManaged : IPostConstructable, IDisposable { }
}