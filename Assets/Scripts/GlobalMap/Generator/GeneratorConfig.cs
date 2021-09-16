using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{
    [System.Serializable]
    public class GeneratorConfig
    {
        public float heightMult = 3f;
        [Range(0, 1)]
        public float seaLevel = 0;

        public int startPixelX = 0;
        public int startPixelZ = 0;
        public int mapWidth = 600;
        public int mapHeight = 400;
        [Header("SourceTextures")]
        public Texture2D heightMap;
        public Texture2D colorMap;
        public Texture2D provinceMap;
        [Header("Map Parts")]
        public ChunkGenerator chunkPrefab;
        public MeshFilter sea;
        public MeshRenderer castlePrefab;

        // max size is 255!!!
        const int CHUNK_SIZE = 200;
        
        public int chunkSize => CHUNK_SIZE;

    }
}
