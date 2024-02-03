using GameStates.Interfaces;

namespace GameStates
{
    public class GameStatesManager : IGameStatesManager
    {
        private IGameState _currentGameState;

        public void ChangeState(IGameState newState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.Exit();
            }

            _currentGameState = newState;

            _currentGameState.Enter();
        }
    }
}