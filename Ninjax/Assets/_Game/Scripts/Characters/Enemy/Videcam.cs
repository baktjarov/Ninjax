using System.Collections.Generic;
using StateMachine;
using TagComponents;
using UnityEngine;

namespace Characters
{
    public class Videcam : StateMachineBase
    {
        public IReadOnlyList<MainPlayer_TagComponent> toAttack => _toAttack;
        [SerializeField] private List<MainPlayer_TagComponent> _toAttack = new List<MainPlayer_TagComponent>();
    }
}