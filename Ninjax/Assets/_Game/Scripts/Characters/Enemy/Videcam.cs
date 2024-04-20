using Interfaces;
using StateMachine;
using System;
using TagComponents;

namespace Characters
{
    public class Videcam : EnemyStateMachineBase, ISignalizer<MainPlayer_TagComponent>
    {
        public Action<MainPlayer_TagComponent> onSignalize { get; set; }

        protected override void OnNotice(MainPlayer_TagComponent mainPlayer)
        {
            base.OnNotice(mainPlayer);

            Signalize(mainPlayer);
        }

        public override void OnSignalization(MainPlayer_TagComponent noticedObject)
        {

        }

        public  void Signalize(MainPlayer_TagComponent noticedObject)
        {
            onSignalize?.Invoke(noticedObject);
        }
    }
}