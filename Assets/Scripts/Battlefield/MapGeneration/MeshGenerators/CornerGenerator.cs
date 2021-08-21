using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public abstract class CornerGenerator : IMeshGenerator
    {
        protected MapConfig _mapConfig;
        protected Vector3[] _corners;
        IMeshGenerator _nextGenerator;

        abstract protected float GetHeight(float distance);
        abstract protected bool SelectCorner(out Vector3 corner);

        public CornerGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void SetNext(IMeshGenerator nextGenerator)
        {
            _nextGenerator = nextGenerator;
        }

        public void CreateVertices(ChunkGenerator chunkGenerator)
        {
            _corners = chunkGenerator.corners;
            
            if(SelectCorner(out var selectedCorner))
            {
                var vertices = CreateVertices(selectedCorner);
                chunkGenerator.SetVertices(vertices);
            }
            else
            {
                _nextGenerator.CreateVertices(chunkGenerator);
            }
        }


        Vector3[] CreateVertices(Vector3 selectedCorner)
        {
            var vertices = new Vector3[_mapConfig.chunkLines * _mapConfig.chunkLines];
            selectedCorner.y = 0;

            for (int i = 0, x = 0; x <= _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z <= _mapConfig.chunkSize; z++)
                {
                    float distanceX = Mathf.Abs(selectedCorner.x - x) / _mapConfig.chunkSize;
                    float distanceY = Mathf.Abs(selectedCorner.z - z) / _mapConfig.chunkSize;


                    float distance = CalculateDistance(distanceX, distanceY);

                    if (distance > 1)
                    {
                        distance = 1;
                    }

                    float y = GetHeight(distance);


                    vertices[i] = new Vector3(x, y, z) + ChunkGenerator.meshOffset;
                    i++;
                }
            }

            return vertices;
        }

        float CalculateDistance(float distanceX, float distanceY)
        {
            float distance;
            if (distanceY >= 2 * distanceX)
            {
                distance = distanceY;
            }
            else if (distanceX >= 2 * distanceY)
            {
                distance = distanceX;
            }
            else
            {
                //корень уравнения
                //(distanceX - x)^2 + (distanceY - x)^2 = x^2
                //где х = distance/2 - радиус окружности
                distance = 2 * (distanceX + distanceY - Mathf.Sqrt(2 * distanceX * distanceY));
            }

            return distance;
        }

    }
}