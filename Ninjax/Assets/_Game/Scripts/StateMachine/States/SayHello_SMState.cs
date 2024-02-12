using TMPro;
using UnityEngine;

namespace StateMachine
{
    public class SayHello_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Transform _player;
        [SerializeField] private TextMeshProUGUI _messageText;

        [Header("Settings")]
        [SerializeField] private float _targetDistance = 7;

        [Header("States")]
        [SerializeField] private FindPlayer_SMState _findPlayerState;

        public override void Tick()
        {
            base.Tick();

            if (Vector3.Distance(transform.position, _player.position) <= _targetDistance)
            {
                _messageText.text = "Hello, " + _player.name + "!";
            }
            else
            {
                transform.LookAt(_player.position);

                _messageText.text = "Nooo, come here!";
                _nextState = _findPlayerState;
            }
        }
    }
}