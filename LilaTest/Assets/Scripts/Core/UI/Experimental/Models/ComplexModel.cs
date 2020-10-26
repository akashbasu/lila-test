using Core.IoC;
using Core.UI.Framework;

namespace Core.UI.Models
{
    internal abstract class ComplexModel : IBoundModel
    {
        [Dependency] protected readonly IUiDataRegistry _dataRegistry;

        public string Key { get; private set; }
        public void OverrideKey(string key) => Key = key;

        protected ComplexModel(string key)
        {
            Key = key;
            
            Injector.ResolveDependencies(this);
        }

        protected void AddSubModel(IBoundModel model)
        {
            model.OverrideKey($"{Key}.{model.Key}");
            _dataRegistry.UpdateModel(model);
        }
    }
}