using System;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class ValueSignal<T>
    {
        public Action<T> onValueChanged;
        [field: SerializeField] public T value { get; private set; }

        public void ChangeValue(T newValue)
        {
            value = newValue;
            onValueChanged?.Invoke(newValue);
        }
    }
}