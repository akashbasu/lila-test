namespace Core.UI.Framework
{
    internal interface IBoundComponent
    {
        string FullKey { get; }
        void OnDataSet(IBoundModel model);
        void OnDataUpdated(IBoundModel model);
        void ResetComponent();
    }
}