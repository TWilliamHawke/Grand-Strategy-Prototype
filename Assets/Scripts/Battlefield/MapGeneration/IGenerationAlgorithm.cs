using System;

namespace Battlefield.Generator
{
    public interface IGenerationAlgorithm
    {
        byte[,] GenerateHeightMap(Random generator);
    }
}
