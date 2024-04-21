using GameStates.Interfaces;
using Services;
using UnityEngine;

namespace GameStates
{
    public class MainMenu_GameState_Controller : IGameState
    {
        private SceneLoader _sceneLoader;
        private IGameStatesManager _gameStatesManager;

        private MainMenu_GameState_ViewsManager _viewsManager;

        public void Enter()
        {
            _sceneLoader = InjectService.diContainer.Resolve<SceneLoader>();
            _gameStatesManager = InjectService.diContainer.Resolve<IGameStatesManager>();

            _sceneLoader?.LoadScene("MainMenu", () =>
            {
                _viewsManager = new MainMenu_GameState_ViewsManager();
                _viewsManager.Initialize();

                _viewsManager.onPlayClicked += EnterGameplayState;
                _viewsManager.onQuitClicked += QuitGame;
            });
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }

        private void EnterGameplayState()
        {
            _gameStatesManager.ChangeState(new Gameplay_GameState_Controller());
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}