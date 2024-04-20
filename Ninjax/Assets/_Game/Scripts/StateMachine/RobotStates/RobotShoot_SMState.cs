using Characters;
using System.Linq;
using TagComponents;
using UnityEngine;

namespace StateMachine
{
    public class RobotShoot_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Robot _robot;

        [Header("States")]
        [SerializeField] private RobotFindPlayer_SMState _findPlayerState;

        public override void Tick()
        {
            base.Tick();

            MainPlayer_TagComponent mainPlayer = null;

            if (_robot.toAttack.Count > 0) { mainPlayer = _robot.toAttack.ElementAt(0); }

            if (mainPlayer != null)
            {
                Debug.Log("Shoot");
            }
            else
            {
                _nextState = _findPlayerState;
            }
        }
    }
}