using Gameplay;
using Services;
using SO;
using System;
using TagComponents;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Model
    {
        public Action<MainPlayer_Tag> onMainPlayerDetected;

        public MainPlayer_Tag mainPlayer;

        [Inject] public Finish finish;
        [Inject] public FollowCamera followCamera;

        [Inject] public ListOfAllViews listOfAllViews;

        public Gameplay_GameState_Model(MainPlayer_Tag _player)
        {
            mainPlayer = _player;
        }

        public void Initialize()
        {
            InjectService.Inject(this);
        }
    }
}
