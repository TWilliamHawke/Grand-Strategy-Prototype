using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Regions
{
    public class RegionPosition
    {
        List<Vector2Int> _points = new List<Vector2Int>();
        int _minX, _minZ, _maxX, _maxZ;
        delegate int SelectVectorPart(Vector2Int vector);

        public List<Vector2Int> points => _points;


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

        public Vector3 FindCenterPosition()
        {
            int middleZ = FindMiddleLine(_minZ, _maxZ, point => point.y);
            int middleX = FindMiddleLine(_minX, _maxX, point => point.x);

            return new Vector3(middleX, 0, middleZ);
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