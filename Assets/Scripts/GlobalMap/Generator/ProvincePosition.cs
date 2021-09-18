using System;
using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using UnityEngine;

namespace GlobalMap
{
    public class ProvincePosition
    {

        GeneratorConfig _config;

        List<Vector2Int> _points = new List<Vector2Int>();
        Vector3 _centerOfMass;
        HashSet<ProvincePosition> _landNeightbors = new HashSet<ProvincePosition>();
        HashSet<ProvincePosition> _seaNeightbors = new HashSet<ProvincePosition>();
        Color _color;
        bool _isSeaRegion = false;
        bool _isUnWalkable = false;

        public Color color => _color;
        public Vector3 centerPosition => _centerOfMass;
        public bool isSeaRegion => _isSeaRegion;
        public bool isUnwalkable => _isUnWalkable;

        delegate int SelectVectorPart(Vector2Int vector);
        int _minX, _minZ, _maxX, _maxZ;

        public ProvincePosition(Color color, GeneratorConfig config)
        {
            _color = color;
            _config = config;

            _isUnWalkable = color == Color.black || color == Color.white;
            _isSeaRegion = color == Color.white;

            if(color == Color.black || color == Color.white)
            {
                _isUnWalkable = true;
            }
        }

        public void AddPoint(int x, int z)
        {
            if (_points.Count == 0)
            {
                _minX = _maxX = x;
                _maxZ = _minZ = z;
            }
            else
            {
                _minX = _minX > x ? x : _minX;
                _maxX = _maxX < x ? x : _maxX;
                _minZ = _minZ > z ? z : _minZ;
                _maxZ = _maxZ < z ? z : _maxZ;

            }

            _points.Add(new Vector2Int(x, z));
        }

        public void SetCenterPosition()
        {
            if(_isUnWalkable)
            {
                _centerOfMass = Vector3.zero;
                return;
            }
            
            int middleZ = FindMiddleLine(_minZ, _maxZ, point => point.y);
            int middleX = FindMiddleLine(_minX, _maxX, point => point.x);

            var height = _config.FindHeight(middleX, middleZ);

            if(height < 0)
            {
                _isSeaRegion = true;
            }
            else
            {
            }

            _centerOfMass = new Vector3(middleX, height, middleZ);
        }

        public void AddNeighbor(ProvincePosition province)
        {
            if (province == this || province.isUnwalkable) return;

            if(province.isSeaRegion)
            {
                _seaNeightbors.Add(province);
            }
            else
            {
                _landNeightbors.Add(province);
            }

        }

        public void PrintProvinceData()
        {
            var provinceData = String.Format("minX is {0}, maxX is {1}, minZ is {2}, maxZ is {3}", _minX, _maxX, _minZ, _maxZ);
            var provinceSize = String.Format("Province size is {0}, neightBors count is {1}", _points.Count, _landNeightbors.Count);
            Debug.Log(provinceData);
            Debug.Log(provinceSize);
        }



        private int FindMiddleLine(int min, int max, SelectVectorPart selector)
        {
            int halfOfPoints = _points.Count / 2;

            int middleLine = min;
            int pointsBellowMiddle = 0;

            while (true)
            {
                foreach (var point in _points)
                {
                    if (selector(point) == middleLine) pointsBellowMiddle++;

                    if (pointsBellowMiddle >= halfOfPoints) break;
                }

                if (middleLine >= max) break;
                if (pointsBellowMiddle >= halfOfPoints) break;

                middleLine++;
            }

            return middleLine;
        }

    }
}