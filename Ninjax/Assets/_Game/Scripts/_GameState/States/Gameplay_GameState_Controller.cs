using GameStates.Interfaces;
using Services;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Controller : IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private IGameStatesManager _gameStatesManager;

        private Gameplay_GameState_ViewsManager _viewsManager;

        public void Enter()
        {
            _sceneLoader?.LoadScene("Gameplay", () =>
            {
                _viewsManager = new Gameplay_GameState_ViewsManager();
                _viewsManager.Initialize();

                _viewsManager.onMainMenuClicked += EnterMainMenuState;
                _viewsManager.onNextClicked += ReloadGameplayState;
            });
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }

        private void EnterMainMenuState()
        {
            _gameStatesManager.ChangeState(new MainMenu_GameState_Controller());
        }

        private void ReloadGameplayState()
        {
            _gameStatesManager.ChangeState(new Gameplay_GameState_Controller());
        }
    }
}