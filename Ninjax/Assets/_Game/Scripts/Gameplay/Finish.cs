using System;
using UnityEngine;

namespace Gameplay
{
    public class Finish : MonoBehaviour
    {
        private bool _isFinish = false;
        public Action isFinish { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isFinish)
            {
                isFinish?.Invoke();
            }
        }
    }
}