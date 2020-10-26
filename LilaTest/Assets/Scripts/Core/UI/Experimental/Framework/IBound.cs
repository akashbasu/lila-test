namespace Core.UI.Framework
{
    //Manager layer
    internal interface IBoundInfo {}
    
    //Ui layer
    internal interface IBoundModel
    {
        string Key { get; }
        void OverrideKey(string key);
    }
}