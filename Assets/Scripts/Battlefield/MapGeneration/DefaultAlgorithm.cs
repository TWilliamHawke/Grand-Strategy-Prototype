using System.Collections;
using System.Collections.Generic;
using System;

namespace Battlefield.Generator
{
    public class DefaultAlgorithm : IGenerationAlgorithm
    {
        byte[,] _heightMap;
        int _linesCount;
        Random _generator;

        MapConfig _mapConfig;

        public DefaultAlgorithm(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public byte[,] GenerateHeightMap(Random generator)
        {
            _linesCount = _mapConfig.linesCount;
            _generator = generator;
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
            RemoveRandomHills(50);
            SmoothTerrain();

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

        void SmoothTerrain()
        {
            for (int x = 0; x < _linesCount; x++)
            {
                for (int y = 0; y < _linesCount; y++)
                {
                    if (CountOfNeightborsHaveHeight(2, x, y) >= 2)
                    {
                        //50% chance
                        if (_generator.Next(0, 100) > 50) continue;
                        _heightMap[x, y] = 2;
                    }
                }
            }

        }

        void RemoveRandomHills(int chance)
        {
            for (int x = 0; x < _linesCount; x++)
            {
                for (int y = 0; y < _linesCount; y++)
                {
                    if (CountOfNeightborsHaveHeight(1, x, y) == 4)
                    {
                        if (_generator.Next(0, 100) > chance) continue;
                        _heightMap[x, y] = 1;
                    }
                }
            }

        }



        int CountOfNeightborsHaveHeight(int height, int x, int y)
        {
            int count = 0;

            if (IndexIsCorrect(x + 1) && _heightMap[x + 1, y] == height) count++;
            if (IndexIsCorrect(x - 1) && _heightMap[x - 1, y] == height) count++;
            if (IndexIsCorrect(y + 1) && _heightMap[x, y + 1] == height) count++;
            if (IndexIsCorrect(y - 1) && _heightMap[x, y - 1] == height) count++;

            return count;
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
