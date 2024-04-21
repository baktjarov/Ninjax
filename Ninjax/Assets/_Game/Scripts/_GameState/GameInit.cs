using GameStates.Interfaces;
using Services;
using Zenject;

namespace GameStates
{
    public class GameInit : MonoInstaller
    {
        [Inject] private IGameStatesManager _gameStatesManager;

        public override void InstallBindings()
        {
            InjectService.SetDIContainer(Container);

            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameStatesManager.ChangeState(new MainMenu_GameState_Controller());
        }
    }
}
