using System.Collections.Generic;
using System;
using TagComponents;
using UnityEngine;

namespace Sensors
{
    public class VisionBase : MonoBehaviour
    {
        public Action<TagComponentBase> onEnter;
        public Action<TagComponentBase> onExit;

        public IReadOnlyCollection<TagComponentBase> noticedObjects => _noticedObjects;

        [SerializeField] protected List<TagComponentBase> _enteredObjects = new List<TagComponentBase>();
        [SerializeField] protected List<TagComponentBase> _noticedObjects = new List<TagComponentBase>();
    }
}