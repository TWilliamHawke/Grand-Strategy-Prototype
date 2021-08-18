using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class DefaultAlgorithm : IGenerationAlgorithm
    {
        byte[,] _heightMap;
        int _linesCount;
        System.Random _generator;

        MapConfig _mapConfig;

        public DefaultAlgorithm(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
			_linesCount = mapConfig.linesCount;

        }

        public byte[,] GenerateHeightMap()
        {
			_generator = new System.Random(_mapConfig.seed);
            _heightMap = new byte[_linesCount, _linesCount];

            for (int x = 0; x < _linesCount; x++)
            {
                for (int y = 0; y < _linesCount; y++)
                {
                    int randomNumber = _generator.Next(0, 100);
                    if (randomNumber < _mapConfig.hillPercentage)
                    {
                        _heightMap[x, y] = 2;
                    }
                    else
                    {
                        _heightMap[x, y] = 1;
                    }
                }
            }

            RemoveHoles();

            return _heightMap;
        }

        void RemoveHoles()
        {
            for (int x = 0; x < _linesCount; x++)
            {
                for (int y = 0; y < _linesCount; y++)
                {
                    if (AllNeightBorIsHills(x, y))
                    {
                        _heightMap[x, y] = 2;
                    }
                }
            }
        }

        bool AllNeightBorIsHills(int x, int y)
        {
            if (_heightMap[x, y] == 2) return false;

            if (IndexIsCorrect(x + 1) && _heightMap[x + 1, y] == 1) return false;
            if (IndexIsCorrect(x - 1) && _heightMap[x - 1, y] == 1) return false;
            if (IndexIsCorrect(y + 1) && _heightMap[x, y + 1] == 1) return false;
            if (IndexIsCorrect(y - 1) && _heightMap[x, y - 1] == 1) return false;

            return true;
        }

        bool IndexIsCorrect(int x)
        {
            return x >= 0 && x < _linesCount;
        }

    }
}
