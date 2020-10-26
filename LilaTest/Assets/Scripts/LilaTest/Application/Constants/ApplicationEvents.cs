namespace LilaTest
{
    internal class ApplicationEvents
    {
        internal class Application
        {
            public static readonly string Start = $"{nameof(Application)}.{nameof(Start)}";
            public static readonly string Stop = $"{nameof(Application)}.{nameof(Stop)}";
        }
        
        internal class Grid
        {
            public static readonly string OnSelect = $"{nameof(Grid)}.{nameof(OnSelect)}";
            public static readonly string Reset = $"{nameof(Grid)}.{nameof(Reset)}";
        }
    }
}