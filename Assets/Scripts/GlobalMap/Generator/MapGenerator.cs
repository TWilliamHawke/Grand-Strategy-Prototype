using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{

    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] GeneratorConfig _config;
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
            _offset = new Vector3(_startPixelX, -0.5f, _startPixelZ);

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

            FindProvincesCenter();

        }

        void FindProvincesCenter()
        {
            var counter = 0;

            foreach (var pair in _provinceList)
            {
                counter++;
                var province = pair.Value;
                pair.Value.SetCenterPosition();
                
                if(province.isSeaRegion || province.isUnwalkable) continue;

                var centerPos = province.centerPosition - _offset;
                var castle = Instantiate(_castlePrefab, centerPos, Quaternion.identity);
                castle.transform.SetParent(transform);
            }

        }

        void CheckNeightBors()
        {
            for (int z = _startPixelZ; z < _endPixelZ; z++)
            {
                Color thisPixelColor = _config.provinceMap.GetPixel(_startPixelX, z);
                for (int x = _startPixelX; x < _endPixelX; x++)
                {
                    Color nextPixelHorizontal = _config.provinceMap.GetPixel(x + 1, z);
                    Color nextPixelVertical = _config.provinceMap.GetPixel(x, z + 1);

                    if (thisPixelColor == nextPixelHorizontal) continue;
                    
                    AddNeighbors(thisPixelColor, nextPixelHorizontal);
                }
            }
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
