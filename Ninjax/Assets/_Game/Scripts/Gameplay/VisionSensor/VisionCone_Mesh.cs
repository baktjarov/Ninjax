using Sensors;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class VisionCone_Mesh : VisionSensor_Simple
    {
        [Header("Settings")]
        [SerializeField] private string _materialColorParameter = "_BaseColor";

        [Header("Components")]
        [SerializeField] private Material _meshMaterial;

        private Mesh _mesh;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private MaterialPropertyBlock _materialPropertyBlock;

        [ContextMenu(nameof(Awake))]
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _mesh = new Mesh();
            _meshFilter.sharedMesh = _mesh;
            _meshRenderer.material = _meshMaterial;

            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        [ContextMenu(nameof(Update))]
        protected override void Update()
        {
            base.Update();

            int[] triangles = new int[(_resolution - 1) * 3];
            Vector3[] vertices = new Vector3[_resolution + 1];
            vertices[0] = Vector3.zero;

            for (int i = 0; i < _resolution; i++)
            {
                vertices[i + 1] = _visionConeRaycasts[i].endPosition_Local;
            }

            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }

            _mesh.Clear();
            _mesh.vertices = vertices;
            _mesh.triangles = triangles;

            _materialPropertyBlock.SetColor(_materialColorParameter, noticedObjects.Count > 0 ? _onHitColor : _normalColor);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}