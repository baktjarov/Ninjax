using GameStates.Interfaces;
using Zenject;

namespace GameStates
{
    public class GameInit : MonoInstaller
    {
        [Inject] private IGameStatesManager _gameStatesManager;

        public override void InstallBindings()
        {

        }

        public override void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameStatesManager.ChangeState(new MainMenu_GameState_Controller());
        }
    }
}
