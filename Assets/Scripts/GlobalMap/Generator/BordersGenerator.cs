using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class BordersGenerator : MonoBehaviour
    {
        GeneratorConfig _config;

        //components
        [SerializeField] MeshFilter _chunkFilter;
        [SerializeField] MeshRenderer _chunkRenderer;
        [SerializeField] MeshCollider _chunkCollider;

    }
}