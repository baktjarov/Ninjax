using Interfaces;
using System;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;

namespace Gameplay
{
    public class VideoCameraSystem : MonoBehaviour, ISignalization<MainPlayer_TagComponent>
    {
        public Action<MainPlayer_TagComponent> onSignalizationTriggerred { get; set; }

        public IReadOnlyList<MainPlayer_TagComponent> noticedObjects => _noticedObjects;

        [SerializeField] private List<ISignalizer<MainPlayer_TagComponent>> _signalizations = new();
        [SerializeField] private List<MainPlayer_TagComponent> _noticedObjects = new();

        private void OnEnable()
        {
            foreach(var monoBeh in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if(monoBeh is ISignalizer<MainPlayer_TagComponent> signalizer)
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

        private void OnNoticedMainPlayer(MainPlayer_TagComponent mainPlayer)
        {
            if (_noticedObjects.Contains(mainPlayer) == false) { _noticedObjects.Add(mainPlayer); }
            onSignalizationTriggerred?.Invoke(mainPlayer);
        }
    }
}