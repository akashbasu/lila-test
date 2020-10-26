namespace Core.DataPropagator
{
    internal interface IDataPropagator<out TData>
    {
        TData Data { get; }
        event Updated<TData> OnUpdated;
    }
    
    internal delegate void Updated<in TData>(TData model);
}