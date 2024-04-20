using TagComponents;
using UnityEngine;

namespace Sensors
{
    public class Trigger_Vision : VisionBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TagComponentBase>(out var tagComponent) == true)
            {
                _enteredObjects.Add(tagComponent);
                _noticedObjects.Add(tagComponent);

                onEnter?.Invoke(tagComponent);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TagComponentBase>(out var tagComponent) == true)
            {
                _enteredObjects.Remove(tagComponent);
                _noticedObjects.Remove(tagComponent);

                onExit?.Invoke(tagComponent);
            }
        }
    }
}