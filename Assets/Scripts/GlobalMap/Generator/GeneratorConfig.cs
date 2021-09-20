using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{
    [CreateAssetMenu(menuName = "Global map/generator config", fileName ="GeneratorConfig")]
    public class GeneratorConfig : ScriptableObject
    {
        public float heightMult = 3f;
        [Range(0, 1)]
        public float seaLevel = 0;

        public int startPixelX = 0;
        public int startPixelZ = 0;
        public int mapWidth = 600;
        public int mapHeight = 400;
        public LayerMask terrainLayer;
        [Header("SourceTextures")]
        public Texture2D heightMap;
        public Texture2D colorMap;
        public Texture2D provinceMap;
        [Header("ProvinceBorders")]
        [Range(0, 0.5f)]
        public float bordersThickness;
        public Color bordersColor = Color.red;

        // max size is 255!!!
        const int CHUNK_SIZE = 200;

        public int chunkSize => CHUNK_SIZE;

        public float FindHeight(float x, float z)
        {
            return FindHeight((int)x, (int)z);
        }

        public float FindHeight(int x, int z)
        {
            var pixel = heightMap.GetPixel(x, z);

            float height = pixel.b > pixel.r ? -0.5f : (pixel.r - seaLevel) * heightMult;
            return height;
        }

    }
}
