using System;
using UnityEngine;

namespace Gameplay
{
    public class AnimationEvents : MonoBehaviour
    {
        public Action<string> onAnimationEvent;

        [field: SerializeField] public Animator animator { get; private set; }

        private void Awake()
        {
            if (animator == null) { animator = GetComponent<Animator>(); }
        }

        public void OnAnimationEvent(string key)
        {
            onAnimationEvent?.Invoke(key);
        }
    }
}