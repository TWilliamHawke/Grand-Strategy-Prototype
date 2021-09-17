using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] MeshRenderer _bordersRenderer;
        [SerializeField] MeshFilter _bordersFilter;

        GeneratorConfig _config;

        //components
        [SerializeField] MeshFilter _chunkFilter;
        [SerializeField] MeshRenderer _chunkRenderer;
        [SerializeField] MeshCollider _chunkCollider;

        int _startX, _startZ, _endX, _endZ;

        int _sizeX => _endX - _startX;
        int _sizeZ => _endZ - _startZ;


        //meshData
        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;

        //chunkTextures
        Texture2D _terrainTexture;
        Texture2D _regionsTexture;

        public void CreateMesh(GeneratorConfig config, int startX, int startZ)
        {
            _config = config;

            _mesh = new Mesh();
            _chunkFilter = GetComponent<MeshFilter>();
            _chunkRenderer = GetComponent<MeshRenderer>();
            _chunkCollider = GetComponent<MeshCollider>();
            _chunkFilter.sharedMesh = _mesh;

            CreateShape(startX, startZ);
            FinalizeMesh();
            CreateTextures(startX, startZ);
            SetTerrainTexture();

        }

        public void SetTerrainTexture()
        {
            _chunkRenderer.material.mainTexture = _terrainTexture;

        }

        public void SetRegionsTexture()
        {
            _chunkRenderer.material.mainTexture = _regionsTexture;
        }

        void CreateShape(int startX, int startZ)
        {
            _startX = startX;
            _startZ = startZ;
            _endX = Mathf.Min(startX + _config.chunkSize, _config.heightMap.width - 1);
            _endZ = Mathf.Min(startZ + _config.chunkSize, _config.heightMap.height - 1);

            CreateVertices();
            CreateTriangles();
        }

        void CreateVertices()
        {
            _vertices = new Vector3[(_sizeX + 1) * (_sizeZ + 1)];

            for (int i = 0, z = _startZ; z <= _endZ; z++)
            {
                for (int x = _startX; x <= _endX; x++)
                {
                    float height = _config.FindHeight(x, z);
                    _vertices[i] = new Vector3(x, height, z);
                    i++;
                }
            }
        }

        private void CreateTriangles()
        {
            int ti = 0;
            int paddingZ = 0;
            _triangles = new int[6 * _sizeX * _sizeZ];

            for (int z = 0; z < _sizeZ; z++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    _triangles[ti++] = paddingZ + 0;
                    _triangles[ti++] = paddingZ + _sizeX + 1;
                    _triangles[ti++] = paddingZ + 1;
                    _triangles[ti++] = paddingZ + 1;
                    _triangles[ti++] = paddingZ + _sizeX + 1;
                    _triangles[ti++] = paddingZ + _sizeX + 2;
                    paddingZ++;
                }
                paddingZ++;
            }
        }

        void FinalizeMesh()
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

            _chunkCollider.sharedMesh = _mesh;

            _mesh.uv = uvs;
        }

        void CreateTextures(int startX, int startZ)
        {
            _regionsTexture = new Texture2D(_sizeX, _sizeZ);
            _regionsTexture.SetPixels(_config.provinceMap.GetPixels(startX, startZ, _sizeX, _sizeZ));
            _regionsTexture.Apply();
            _terrainTexture = new Texture2D(_sizeX, _sizeZ);
            _terrainTexture.SetPixels(_config.colorMap.GetPixels(startX, startZ, _sizeX, _sizeZ));
            _terrainTexture.Apply();
        }

    }
}
