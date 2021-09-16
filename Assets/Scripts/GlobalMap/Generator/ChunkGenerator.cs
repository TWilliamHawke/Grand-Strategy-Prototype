using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{

    public class ChunkGenerator : MonoBehaviour
    {
        GeneratorConfig _config;

        //components
        Mesh _mesh;
        MeshFilter _filter;
        MeshRenderer _renderer;
        MeshCollider _collider;

        int _sizeX;
        int _sizeZ;

        Vector3[] _vertices;
        int[] _triangles;

        Texture2D _terrainTexture;
        Texture2D _regionsTexture;

        public void CreateMesh(GeneratorConfig config, int startX, int startZ)
        {
            _config = config;

            _mesh = new Mesh();
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<MeshCollider>();
            _filter.sharedMesh = _mesh;

            CreateShape(startX, startZ);
            UpdateMesh();
            CreateTexture(startX, startZ);
        }

        public void SetTerrainTexture()
        {
            _renderer.material.mainTexture = _terrainTexture;

        }

        public void SetRegionsTexture()
        {
            _renderer.material.mainTexture = _regionsTexture;

        }

        void CreateShape(int startX, int startZ)
        {
            int endX = Mathf.Min(startX + _config.chunkSize, _config.heightMap.width - 1);
            int endZ = Mathf.Min(startZ + _config.chunkSize, _config.heightMap.height - 1);

            _sizeX = endX - startX;
            _sizeZ = endZ - startZ;

            _vertices = new Vector3[(_sizeX + 1) * (_sizeZ + 1)];

            for (int i = 0, z = startZ; z <= endZ; z++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    var pixel = _config.heightMap.GetPixel(x, z);


                    float height = pixel.b > pixel.r ? -0.5f : (pixel.r - _config.seaLevel) * _config.heightMult;
                    _vertices[i] = new Vector3(x, height, z);
                    i++;
                }
            }

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

        void UpdateMesh()
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

            _collider.sharedMesh = _mesh;

            _mesh.uv = uvs;
        }

        void CreateTexture(int startX, int startZ)
        {
            _regionsTexture = new Texture2D(_sizeX, _sizeZ);
            _regionsTexture.SetPixels(_config.provinceMap.GetPixels(startX, startZ, _sizeX, _sizeZ));
            _regionsTexture.Apply();
            _terrainTexture = new Texture2D(_sizeX, _sizeZ);
            _terrainTexture.SetPixels(_config.colorMap.GetPixels(startX, startZ, _sizeX, _sizeZ));
            _terrainTexture.Apply();
            SetRegionsTexture();
        }

        }
}
