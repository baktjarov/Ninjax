using System;
using TagComponents;
using UnityEngine;

namespace Sensors
{
    public class VisionSensor : MonoBehaviour
    {
        public Action<TagComponentBase> onEnter;
        public Action<TagComponentBase> onExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TagComponentBase>(out TagComponentBase tag))
            {
                onEnter?.Invoke(tag);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TagComponentBase>(out TagComponentBase tag))
            {
                onExit?.Invoke(tag);
            }
        }
    }
}