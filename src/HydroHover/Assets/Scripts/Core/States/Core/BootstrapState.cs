using Core.States.Base;
using Data;

namespace Core.States.Core
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadLevelState, string>(ScenesPaths.GAMEPLAY);
        }

        public void Exit()
        {
        }
    }
}