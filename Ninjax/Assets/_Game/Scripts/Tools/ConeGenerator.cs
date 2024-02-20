using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class ConeGenerator : MonoBehaviour
    {
        [SerializeField][Range(5, 50)] private int _resolution = 25;
        [SerializeField][Range(0, 5)] private float _width = 90;
        [SerializeField][Min(0)] private float _length = 2;

        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;

        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;

        private void Awake()
        {
            Setup();
        }

        private void Update()
        {
            UpdateMeshData();
            Generate();
            ApplyMesh();
        }

        private void Setup()
        {
            _mesh = new Mesh();
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();

            _meshFilter.sharedMesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }

        private void UpdateMeshData()
        {
            _vertices = new Vector3[_resolution + 2];
            _uv = new Vector2[_vertices.Length];
            _triangles = new int[(_resolution * 2) * 3];
            _uv[0] = new Vector2(0.5f, 0f);
        }

        private void Generate()
        {
            float radius = _width * _length;

            _vertices[0] = Vector3.forward * _length;

            for (int i = 0, n = _resolution - 1; i < _resolution; i++)
            {
                float ratio = (float)i / n;
                float r = ratio * (Mathf.PI * 2f);
                float x = Mathf.Cos(r) * radius;
                float z = Mathf.Sin(r) * radius;

                _vertices[i + 1] = new Vector3(x, z, _length);
                _uv[i + 1] = new Vector2(ratio, 0f);
            }

            _vertices[_resolution + 1] = Vector3.zero;
            _uv[_resolution + 1] = new Vector2(0.5f, 1f);

            for (int i = 0, n = _resolution - 1; i < _resolution; i++)
            {
                float ratio = (float)i / n;
                float r = ratio * (Mathf.PI * 2f);
                float x = Mathf.Cos(r) * radius;
                float z = Mathf.Sin(r) * radius;

                float adjustedHeight = _length;

                _vertices[i + 1] = new Vector3(x, z, adjustedHeight);
                _uv[i + 1] = new Vector2(ratio, 0f);
            }

            int bottomOffset = _resolution * 3;

            for (int i = 0, n = _resolution - 1; i < n; i++)
            {
                int offset = i * 3 + bottomOffset;
                _triangles[offset] = i + 1;
                _triangles[offset + 1] = _resolution + 1;
                _triangles[offset + 2] = i + 2;
            }
        }

        private void ApplyMesh()
        {
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _mesh.triangles = _triangles;
            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();
        }

        [ContextMenu(nameof(Generate_Editor))]
        private void Generate_Editor()
        {
            Setup();
            UpdateMeshData();
            Generate();
            ApplyMesh();
        }
    }
}
