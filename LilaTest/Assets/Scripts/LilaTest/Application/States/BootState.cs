using Core.DataProviders;
using Core.EventSystems;
using Core.GameStateMachine;
using Core.IoC;
using Core.UI.StateControl;

namespace LilaTest
{
    internal class BootState : BaseGameState
    {
        protected override void InstallDependencies()
        {
            DependencyRegistry.RegisterInterface<IGameEventManager, GameEventManager>();
            DependencyRegistry.RegisterInterface<ISceneReferenceProvider, SceneReferenceProvider>();
            DependencyRegistry.RegisterInterface<IUiStateController, UiStateController>();
        }
    }
}