using Interfaces;
using System;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;

namespace Sensors
{
    public class VisionSensor_Simple : VisionBase
    {
        public class VisionConeRaycast
        {
            public Vector3 startPosition_Local = Vector3.zero;
            public Vector3 startPosition_World = Vector3.zero;

            public bool hasHitAnything = false;
            public bool hasHitTag = false;

            public Vector3 raycastDirection = Vector3.zero;
            public float raycastLength = 0;

            public Vector3 endPosition_Local = Vector3.zero;
            public Vector3 endPosition_World = Vector3.zero;

            public Color color = Color.green;
        }

        [SerializeField][Range(5, 360)] protected int _resolution = 50;
        [SerializeField][Range(5, 360f)] protected float _angle = 90f;
        [SerializeField] protected float _length = 10;
        [SerializeField] protected LayerMask _visionLayer;
        [SerializeField] protected bool _colorOnlyOnHitTagComponents = true;
        [SerializeField] protected bool _debugRaycast = true;

        [Header("Visuals")]
        [SerializeField] protected Color _normalColor = new Color(0, 1, 0, 0.05f);
        [SerializeField] protected Color _onHitColor = new Color(1, 0, 0, 0.05f);

        protected List<VisionConeRaycast> _visionConeRaycasts = new List<VisionConeRaycast>();

        protected virtual void Update()
        {
            Scan();

            for (int i = 0; i < _enteredObjects.Count; i++)
            {
                TagComponentBase enteredObject = _enteredObjects[i];

                if (_noticedObjects.Contains(enteredObject) == false)
                {
                    _noticedObjects.Add(enteredObject);
                    onEnter?.Invoke(enteredObject);
                }
            }

            for (int i = 0; i < _noticedObjects.Count; i++)
            {
                if (i >= _noticedObjects.Count) { break; }

                TagComponentBase noticedObject = _noticedObjects[i];

                if (_enteredObjects.Contains(noticedObject) == false && _noticedObjects.Contains(noticedObject) == true)
                {
                    _noticedObjects.Remove(noticedObject);
                    onExit?.Invoke(noticedObject);
                }
            }
        }

        protected virtual void Scan()
        {
            _enteredObjects.Clear();
            _visionConeRaycasts.Clear();

            float angleIncrement = _angle / (_resolution - 1);

            float currenTangle = -_angle / 2;
            float sine;
            float cosine;

            Vector3 startPosition = transform.position;

            for (int i = 0; i < _resolution; i++)
            {
                float radians = Mathf.Deg2Rad * currenTangle;
                sine = Mathf.Sin(radians);
                cosine = Mathf.Cos(radians);

                Vector3 raycastDirection_World = (transform.forward * cosine) + (transform.right * sine);
                Vector3 raycastDirection_Local = (Vector3.forward * cosine) + (Vector3.right * sine);

                TagComponentBase tag = null;

                bool isHit = Physics.Raycast(startPosition, raycastDirection_World, out RaycastHit hit, _length, _visionLayer);

                VisionConeRaycast visionConeRaycast = new VisionConeRaycast();
                visionConeRaycast.startPosition_Local = Vector3.zero;
                visionConeRaycast.startPosition_World = startPosition;

                visionConeRaycast.hasHitAnything = isHit;
                visionConeRaycast.hasHitTag = isHit && hit.collider.TryGetComponent<TagComponentBase>(out tag);

                visionConeRaycast.raycastDirection = raycastDirection_World;
                if (visionConeRaycast.hasHitAnything == true) { visionConeRaycast.raycastLength = hit.distance; }
                else { visionConeRaycast.raycastLength = _length; }

                visionConeRaycast.endPosition_Local = raycastDirection_Local * visionConeRaycast.raycastLength;
                visionConeRaycast.endPosition_World = visionConeRaycast.startPosition_World + visionConeRaycast.raycastDirection * visionConeRaycast.raycastLength;

                visionConeRaycast.color = _normalColor;

                if (visionConeRaycast.hasHitAnything == true)
                {
                    if (_colorOnlyOnHitTagComponents == true)
                    {
                        if (visionConeRaycast.hasHitTag == true)
                        {
                            visionConeRaycast.color = _onHitColor;
                        }
                    }
                    else
                    {
                        visionConeRaycast.color = _onHitColor;
                    }
                }

                _visionConeRaycasts.Add(visionConeRaycast);

                currenTangle += angleIncrement;

                OnTryNotice(tag);

                if (_debugRaycast == true) { Debug.DrawLine(visionConeRaycast.startPosition_World, visionConeRaycast.endPosition_World, visionConeRaycast.color); }
            }
        }

        protected virtual void OnTryNotice(TagComponentBase tag)
        {
            if (tag == null) { return; }

            if (_enteredObjects.Contains(tag) == false) { _enteredObjects.Add(tag); }
        }
    }
}