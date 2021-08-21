using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class FrameGenerator : MonoBehaviour
    {
        [SerializeField] MapConfig _mapConfig;
        [SerializeField] int _framePadding;
        [SerializeField] int _frameWidth;
        [SerializeField] FramePartGenerator[] _frame;

        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;

        //components
        MeshFilter _meshFilter;
        MeshRenderer _meshRenderer;



        private void OnValidate()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }



    }
}