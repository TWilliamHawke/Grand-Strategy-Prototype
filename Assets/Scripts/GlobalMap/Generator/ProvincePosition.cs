using System;
using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using UnityEngine;

namespace GlobalMap
{
    public class ProvincePosition
    {
        List<Vector2Int> _points = new List<Vector2Int>();
        Vector3 _centerOfMass;

        HashSet<ProvincePosition> _neightbors = new HashSet<ProvincePosition>();
        delegate int SelectVectorPart(Vector2Int vector);

        Color _color;
        GeneratorConfig _config;

        public Color color => _color;
        public Vector3 centerPosition => _centerOfMass;

        public ProvincePosition(Color color, GeneratorConfig config)
        {
            _color = color;
            _config = config;
        }

        int _minX, _minZ, _maxX, _maxZ;

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

        public Vector3 GetCenterPosition()
        {
            int middleZ = FindMiddleLine(_minZ, _maxZ, point => point.y);
            int middleX = FindMiddleLine(_minX, _maxX, point => point.x);

            var height = _config.FindHeight(middleX, middleZ);

            _centerOfMass = new Vector3(middleX, height, middleZ);
            return _centerOfMass;
        }

        public void AddNeighbor(ProvincePosition province)
        {
            if (province == this) return;


            _neightbors.Add(province);
        }

        public void PrintProvinceData()
        {
            var provinceData = String.Format("minX is {0}, maxX is {1}, minZ is {2}, maxZ is {3}", _minX, _maxX, _minZ, _maxZ);
            var provinceSize = String.Format("Province size is {0}, neightBors count is {1}", _points.Count, _neightbors.Count);
            Debug.Log(provinceData);
            Debug.Log(provinceSize);
        }



        private int FindMiddleLine(int min, int max, SelectVectorPart callback)
        {
            int halfOfPoints = _points.Count / 2;

            int middleLine = min;
            int pointsBellowMiddle = 0;

            while (true)
            {
                foreach (var point in _points)
                {
                    if (callback(point) == middleLine) pointsBellowMiddle++;

                    if (pointsBellowMiddle >= halfOfPoints) break;
                }

                if (middleLine >= max) break;
                if (pointsBellowMiddle >= halfOfPoints) break;

                middleLine++;
            }

            return middleLine;
        }

        private int FindMiddleZ()
        {
            int halfpoints = _points.Count / 2;

            int middleLine = _minZ;
            int pointsBellowMiddle = 0;

            while (true)
            {
                foreach (var point in _points)
                {
                    if (point.y == middleLine) pointsBellowMiddle++;

                    if (pointsBellowMiddle > halfpoints) break;
                }

                middleLine++;

                if (middleLine >= _maxZ) break;
                if (pointsBellowMiddle > halfpoints) break;

            }

            return middleLine - 1;
        }
    }
}