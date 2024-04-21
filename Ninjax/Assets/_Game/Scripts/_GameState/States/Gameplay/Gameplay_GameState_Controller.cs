using GameStates.Interfaces;
using Services;
using TagComponents;
using UnityEngine;

namespace GameStates
{
    public class Gameplay_GameState_Controller : IGameState
    {
        private SceneLoader _sceneLoader;
        private IGameStatesManager _gameStatesManager;

        private Gameplay_GameState_View _view;
        private Gameplay_GameState_Model _model;

        public void Enter()
        {
            _sceneLoader = InjectService.diContainer.Resolve<SceneLoader>();
            _gameStatesManager = InjectService.diContainer.Resolve<IGameStatesManager>();

            _sceneLoader?.LoadScene("Gameplay", () =>
            {
                MainPlayer_Tag mainPlayer = InjectService.diContainer.Resolve<MainPlayer_Tag>();
                PlayerStart_Tag playerStart = InjectService.diContainer.Resolve<PlayerStart_Tag>();
                mainPlayer = Object.Instantiate(mainPlayer, playerStart.transform.position, playerStart.transform.rotation);

                _model = new Gameplay_GameState_Model(mainPlayer);
                _model.Initialize();

                _view = new Gameplay_GameState_View(_model);
                _view.Initialize();

                _view.onMainMenuClicked += EnterMainMenuState;
                _view.onNextClicked += ReloadGameplayState;

                _model.followCamera.SetFollowObject(mainPlayer.transform);
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