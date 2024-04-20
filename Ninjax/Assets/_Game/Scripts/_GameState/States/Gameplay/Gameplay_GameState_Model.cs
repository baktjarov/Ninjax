using Characters;
using Gameplay;
using SO;
using System;
using TagComponents;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Model
    {
        public Action<MainPlayer_TagComponent> onMainPlayerDetected;

        public Player player;
        public Finish finish;

        [Inject] public ListOfAllViews listOfAllViews;

        public void Initialize()
        {

        }
    }
}
