namespace Core.GameStateMachine
{
    internal static partial class CoreEvents
    {
        internal static class GameStateMachine
        {
            public static readonly string Start = $"{nameof(GameStateMachine)}.{nameof(Start)}";
            public static readonly string End = $"{nameof(GameStateMachine)}.{nameof(End)}";
        }
    }
}