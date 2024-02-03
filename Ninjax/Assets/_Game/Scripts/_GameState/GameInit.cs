using GameStates.Interfaces;
using UnityEngine;

namespace GameStates
{
    public class GameInit : MonoBehaviour
    {
        private IGameStatesManager _gameStatesManager;

        public void Start()
        {
            _gameStatesManager = new GameStatesManager();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameStatesManager.ChangeState(new MainMenu_GameState_Controller());
        }
    }
}