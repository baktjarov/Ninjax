using GameStates.Interfaces;
using Zenject;

namespace GameStates
{
    public class GameStatesManager : IGameStatesManager
    {
        private IGameState _currentGameState;
        private DiContainer _diContainer;

        public GameStatesManager(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void ChangeState(IGameState newState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.Exit();
            }

            _currentGameState = newState;

            _diContainer?.Inject(_currentGameState);

            _currentGameState.Enter();
        }
    }
}