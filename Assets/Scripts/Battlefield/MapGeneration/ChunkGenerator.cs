using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battlefield.Chunks;

namespace Battlefield.Generator
{
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class ChunkGenerator : MonoBehaviour
    {
        static Vector3 _meshOffset = Vector3.zero;
        public static Vector3 meshOffset => _meshOffset;
        static bool _meshOffsetDone = false;

        Vector3[] _corners;
        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;

        [SerializeField] MapConfig _mapConfig;
        [Header("Frame Config")]
        [Range(0, 20)]
        [SerializeField] int _framePadding;
        [Range(0, 20)]
        [SerializeField] int _frameWidth;


        //components
        MeshFilter _meshFilter;
        MeshRenderer _meshRenderer;
        MeshCollider _meshCollider;
        Chunk _chunkData;

        //getters
        public Vector3[] corners => _corners;
        public int framePadding => _framePadding;
        public int frameWidth => _frameWidth;


        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshCollider = GetComponent<MeshCollider>();
            _chunkData = GetComponent<Chunk>();
        }

        public void SetCorners(Vector3[] cornersHeight)
        {
            _corners = cornersHeight;

            if (!_meshOffsetDone)
            {
                float meshOffset = _mapConfig.chunkSize / -2f;
                _meshOffset = new Vector3(meshOffset, 0, meshOffset);
                _meshOffsetDone = true;
            }
        }

        public void CreateFrame()
        {
            _chunkData.GenerateFrame(this);
        }

        public void SetVertices(Vector3[] vertices)
        {
            _mesh = new Mesh();
            _vertices = vertices;

            CreateTriangles();
            FinalizeShape();
            _meshFilter.sharedMesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }

        void CreateTriangles()
        {
            int ti = 0;
            int paddingY = 0;
            _triangles = new int[6 * _mapConfig.chunkSize * _mapConfig.chunkSize];

            for (int x = 0; x < _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z < _mapConfig.chunkSize; z++)
                {
                    _triangles[ti++] = paddingY + _mapConfig.chunkSize + 1;
                    _triangles[ti++] = paddingY + 0;
                    _triangles[ti++] = paddingY + 1;
                    _triangles[ti++] = paddingY + _mapConfig.chunkSize + 1;
                    _triangles[ti++] = paddingY + 1;
                    _triangles[ti++] = paddingY + _mapConfig.chunkSize + 2;
                    paddingY++;
                }
                paddingY++;
            }
        }

        void FinalizeShape()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();

            SetTexturesMap();
        }

        private void SetTexturesMap()
        {
            Vector2[] uvs = new Vector2[_vertices.Length];

            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
            }
            _mesh.uv = uvs;
        }
    }
}