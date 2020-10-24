namespace Core.Ui
{
    internal interface IBindable<in TData>
    {
        void UpdateData(TData data);
    }
}