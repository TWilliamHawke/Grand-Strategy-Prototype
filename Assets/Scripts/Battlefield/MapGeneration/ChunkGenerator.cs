using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Battlefield.Generator
{
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    //[RequireComponent(typeof(MeshFilter))]
    public class ChunkGenerator : MonoBehaviour
    {
        Vector3[] _corners;
        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;


        [SerializeField] MapConfig _mapConfig;

        //components
        MeshFilter _meshFilter;
        MeshRenderer _meshRenderer;
        MeshCollider _meshCollider;

        public Vector3[] corners => _corners;


        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetCorners(Vector3[] cornersHeight)
        {
            _corners = cornersHeight;
        }

        public void SetCorners()
        {
            float sz = _mapConfig.chunkSize;

            _corners = new Vector3[4] {
                        new Vector3(0, 1, 0),
                        new Vector3(sz, 2, 0),
                        new Vector3(sz, 2, sz),
                        new Vector3(0, 1, sz)
                        };

        }

        public void CreateMesh()
        {
            _mesh = new Mesh();
            _meshFilter.sharedMesh = _mesh;
            CreateShape();
            FinalizeShape();
        }

        public void SetVertices(Vector3[] vertices)
        {
            _vertices = vertices;
        }

        void CreateShape()
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

            Vector2[] uvs = new Vector2[_vertices.Length];

            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
            }


            _mesh.uv = uvs;

        }

    }
}