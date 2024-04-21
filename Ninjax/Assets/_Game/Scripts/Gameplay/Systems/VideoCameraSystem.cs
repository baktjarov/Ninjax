using Interfaces;
using System;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;

namespace Gameplay
{
    public class VideoCameraSystem : MonoBehaviour, ISignalization<MainPlayer_Tag>
    {
        public Action<MainPlayer_Tag> onSignalizationTriggerred { get; set; }

        public IReadOnlyList<MainPlayer_Tag> noticedObjects => _noticedObjects;

        [SerializeField] private List<ISignalizer<MainPlayer_Tag>> _signalizations = new();
        [SerializeField] private List<MainPlayer_Tag> _noticedObjects = new();

        private void OnEnable()
        {
            foreach(var monoBeh in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if(monoBeh is ISignalizer<MainPlayer_Tag> signalizer)
                {
                    _signalizations.Add(signalizer);
                }
            }

            foreach (var signalizations in _signalizations)
            {
                signalizations.onSignalize += OnNoticedMainPlayer;
            }
        }

        private void OnDisable()
        {
            foreach (var signalizations in _signalizations)
            {
                signalizations.onSignalize -= OnNoticedMainPlayer;
            }
        }

        private void OnNoticedMainPlayer(MainPlayer_Tag mainPlayer)
        {
            if (_noticedObjects.Contains(mainPlayer) == false) { _noticedObjects.Add(mainPlayer); }
            onSignalizationTriggerred?.Invoke(mainPlayer);
        }
    }
}