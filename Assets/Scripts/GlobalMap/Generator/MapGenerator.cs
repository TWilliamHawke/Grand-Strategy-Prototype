using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{

    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] GeneratorConfig _config;
        [SerializeField] MeshFilter _borders;
        [Header("Map Parts")]
        [SerializeField] ChunkGenerator _chunkPrefab;
        [SerializeField] MeshFilter _sea;
        [SerializeField] Castle _castlePrefab;


        int _startPixelX;
        int _startPixelZ;
        int _endPixelX;
        int _endPixelZ;

        List<int> _triangles = new List<int>();

        Mesh _bordersMesh;
        List<Vector3> _verticies = new List<Vector3>();
        List<int> _indicies = new List<int>();
        Vector3 _offset;

        Dictionary<Color, ProvincePosition> _provinceList = new Dictionary<Color, ProvincePosition>();
        public Dictionary<Color, ProvincePosition> provinceList => _provinceList;

        List<ChunkGenerator> _chunkList = new List<ChunkGenerator>();


        private void Start()
        {
            _startPixelX = _config.startPixelX;
            _startPixelZ = _config.startPixelZ;
            
            _bordersMesh = new Mesh();
            _offset = new Vector3(_startPixelX, -0.5f, _startPixelZ);
            _borders.transform.position = -_offset - Vector3.up * 0.5f;


            CreateLandscape();
            CreateProvinces();
            CheckNeightBors();
        }

        public void ShowRegionsMap()
        {
            foreach (var chunk in _chunkList)
            {
                chunk.SetRegionsTexture();
            }
        }

        public void ShowTerrainMap()
        {
            foreach (var chunk in _chunkList)
            {
                chunk.SetTerrainTexture();
            }
        }

        void CreateLandscape()
        {
            _endPixelX = _startPixelX + _config.mapWidth;
            _endPixelX = _endPixelX > _config.heightMap.width ? _config.heightMap.width : _endPixelX;

            _endPixelZ = _startPixelZ + _config.mapHeight;
            _endPixelZ = _endPixelZ > _config.heightMap.height ? _config.heightMap.height : _endPixelZ;

            var chunkPos = new Vector3(-_startPixelX, 0, -_startPixelZ);

            for (int x = _startPixelX; x < _endPixelX; x += _config.chunkSize)
            {
                for (int z = _startPixelZ; z < _endPixelZ; z += _config.chunkSize)
                {

                    var chunk = Instantiate(_chunkPrefab, chunkPos, Quaternion.identity);
                    _chunkList.Add(chunk);
                    chunk.CreateMesh(_config, x, z);
                    chunk.transform.SetParent(transform);
                }
            }

            TransformSea();
        }

        void TransformSea()
        {
            int seaWidth = _endPixelX - _startPixelX - 1;
            int seaHeight = _endPixelZ - _startPixelZ - 1;

            _sea.transform.position = new Vector3(seaWidth / 2f, 0, seaHeight / 2f);
            _sea.transform.localScale = new Vector3(seaWidth / 10f, 1, seaHeight / 10f);
        }

        void CreateProvinces()
        {

            for (int x = _startPixelX; x < _endPixelX; x++)
            {
                for (int z = _startPixelZ; z < _endPixelZ; z++)
                {
                    Color color = _config.provinceMap.GetPixel(x, z);

                    if (!_provinceList.ContainsKey(color))
                    {
                        _provinceList.Add(color, new ProvincePosition(color, _config));
                    }
                    _provinceList[color].AddPoint(x, z);

                }
            }


            StartCoroutine(FindProvincesCenter());

        }

        private IEnumerator FindProvincesCenter()
        {
            yield return null;

            foreach (var pair in _provinceList)
            {
                var centerPos = pair.Value.GetCenterPosition() - _offset;
                if (pair.Value.centerPosition.y >= 0)
                {
                    var castle = Instantiate(_castlePrefab, centerPos, Quaternion.identity);
                    castle.transform.SetParent(transform);
                }
            }
        }

        void CheckNeightBors()
        {
            var thicknessOffsetHorizontal = new Vector3(-_config.bordersThickness, 0, 0);
            var thicknessOffsetVertical = new Vector3(0, 0, _config.bordersThickness);

            for (int z = _startPixelZ; z < _endPixelZ; z++)
            {
                Color thisPixelColor = _config.provinceMap.GetPixel(_startPixelX, z);
                for (int x = _startPixelX; x < _endPixelX; x++)
                {
                    Color nextPixelHorizontal = _config.provinceMap.GetPixel(x + 1, z);
                    Color nextPixelVertical = _config.provinceMap.GetPixel(x, z + 1);

                    if (thisPixelColor != nextPixelHorizontal)
                    {
                        AddNeighbors(thisPixelColor, nextPixelHorizontal);
                        AddBorders(x, z, Vector3.back, thicknessOffsetHorizontal);
                    }

                    if (thisPixelColor != nextPixelVertical)
                    {
                        AddBorders(x, z, Vector3.left, thicknessOffsetVertical);
                    }

                    thisPixelColor = nextPixelHorizontal;
                }

            }

            // Debug.Log(_indicies.Last());
            // Debug.Log(_verticies.Count);

            _bordersMesh.vertices = _verticies.ToArray();
            // _bordersMesh.SetIndices(_indicies, MeshTopology.Lines, 0);
            // _bordersMesh.RecalculateBounds();
            _bordersMesh.triangles = _triangles.ToArray();
            _bordersMesh.RecalculateNormals();
            _borders.sharedMesh = _bordersMesh;

        }

        private void AddBorders(int x, int z, Vector3 borderDirection, Vector3 thicknessOffset)
        {
            int verticeIndex = _verticies.Count;

            var borderStart = new Vector3(x + 1, 10, z + 1);
            var borderEnd = borderStart + borderDirection;

            borderStart = VectorHeightToMap(borderStart);
            borderEnd = VectorHeightToMap(borderEnd);

            _verticies.Add(borderStart + thicknessOffset);
            _verticies.Add(borderStart - thicknessOffset);
            _verticies.Add(borderEnd - thicknessOffset);
            _verticies.Add(borderEnd + thicknessOffset);

            _triangles.Add(verticeIndex);
            _triangles.Add(verticeIndex + 1);
            _triangles.Add(verticeIndex + 2);
            _triangles.Add(verticeIndex);
            _triangles.Add(verticeIndex + 2);
            _triangles.Add(verticeIndex + 3);


            // _indicies.Add(_verticies.Count);
            // _verticies.Add(borderStart);
            // _indicies.Add(_verticies.Count);
            // _verticies.Add(borderEnd);

        }

        public Vector3 VectorHeightToMap(Vector3 vector)
        {
            var height = _config.FindHeight(vector.x, vector.z);

            vector.y = Mathf.Max(height, 0) + .05f;
            return vector;
        }



        void AddNeighbors(Color c1, Color c2)
        {
            bool success1 = _provinceList.TryGetValue(c1, out var province1);
            bool success2 = _provinceList.TryGetValue(c2, out var province2);

            if (success1 && success2)
            {
                province1.AddNeighbor(province2);
                province2.AddNeighbor(province1);
            }
        }
    }
}
