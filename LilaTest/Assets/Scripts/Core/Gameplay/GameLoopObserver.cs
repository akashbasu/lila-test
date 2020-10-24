namespace Core.Gameplay
{
    internal interface IGameLoopObserver
    {
        void Reset();
        void SetLevelData<TData>(TData data);
        void OnGameStart();
        void Update(float deltaTime);
        void OnGameEnd();
    }
}