using GameStates.Interfaces;
using Services;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Controller : IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private IGameStatesManager _gameStatesManager;

        private Gameplay_GameState_View _view;
        private Gameplay_GameState_Model _model;

        public void Enter()
        {
            _sceneLoader?.LoadScene("Gameplay", () =>
            {
                _model = new Gameplay_GameState_Model();

                _view = new Gameplay_GameState_View(_model);
                _view.Initialize();

                _view.onMainMenuClicked += EnterMainMenuState;
                _view.onNextClicked += ReloadGameplayState;
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