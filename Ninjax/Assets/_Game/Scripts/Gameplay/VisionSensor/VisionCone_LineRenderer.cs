using Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class VisionCone_LineRenderer : VisionSensor_Simple
    {
        [Header("Settings")]
        [SerializeField] private float _lineRendererWidthMultiplier = 1;

        [Header("Components")]
        [SerializeField] private LineRenderer _lineRendererPrefab;

        private List<LineRenderer> _lineRenderersPool = new List<LineRenderer>();

        [ContextMenu(nameof(Update))]
        protected override void Update()
        {
            if (_lineRendererPrefab == null) { return; }

            if (_lineRenderersPool.Count != _resolution)
            {
                ClearLineRenderers();
                for (int i = 0; i < _resolution; i++)
                {
                    LineRenderer lineRenderer = Instantiate(_lineRendererPrefab, transform);
                    _lineRenderersPool.Add(lineRenderer);
                    lineRenderer.widthCurve.keys = new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) };
                }
            }

            base.Update();

            for (int i = 0; i < _visionConeRaycasts.Count; i++)
            {
                VisionConeRaycast visionConeRaycast = _visionConeRaycasts[i];
                LineRenderer lineRenderer = _lineRenderersPool[i];
                lineRenderer.SetPositions(new Vector3[] { visionConeRaycast.startPosition_World, visionConeRaycast.endPosition_World });
                lineRenderer.startColor = visionConeRaycast.color;
                lineRenderer.endColor = visionConeRaycast.color;
                lineRenderer.widthMultiplier = _lineRendererWidthMultiplier;
            }
        }

        private void ClearLineRenderers()
        {
            foreach (var renderer in _lineRenderersPool)
            {
                Destroy(renderer.gameObject);
            }
            _lineRenderersPool.Clear();
        }
    }
}