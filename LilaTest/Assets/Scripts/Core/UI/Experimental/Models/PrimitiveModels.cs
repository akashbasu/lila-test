namespace Core.UI.Models
{
    internal class StringModel : GenericModel<string>
    {
        public StringModel(string key, string value) : base(key, value) { }
    }
    
    internal class IntModel : GenericModel<int>
    {
        public IntModel(string key, int value) : base(key, value) { }
    }

    internal class BoolModel : GenericModel<bool>
    {
        public BoolModel(string key, bool value) : base(key, value) { }
    }
}