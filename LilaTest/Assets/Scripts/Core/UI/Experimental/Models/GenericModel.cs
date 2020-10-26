using Core.UI.Framework;

namespace Core.UI.Models
{
    internal abstract class GenericModel<TModel> : IBoundModel
    {
        public string Key { get; private set; }
        public TModel Value { get; }
        
        public void OverrideKey(string key) => Key = key;
        
        protected GenericModel(string key, TModel value)
        {
            Key = key;
            Value = value;
        }
    }
}