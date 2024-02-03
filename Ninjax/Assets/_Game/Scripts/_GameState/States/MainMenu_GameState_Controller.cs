using GameStates.Interfaces;
using Services;

namespace GameStates
{
    public class MainMenu_GameState_Controller : IGameState
    {
        private SceneLoader _sceneLoader;
        private IGameStatesManager _gameStatesManager;

        public void Enter()
        {
            _sceneLoader?.LoadScene("MainMenu", () =>
            {

            });
        }

        public void Exit()
        {

        }
        public void Update()
        {

        }
    }
}