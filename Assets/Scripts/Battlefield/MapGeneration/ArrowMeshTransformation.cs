using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class ArrowMeshTransformation : MonoBehaviour
    {
        //layer for raycasts
        [SerializeField] LayerMask _layerMask;
        [SerializeField] float _verticalOffset = 0.2f;

        MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void ChangeShape()
        {
            Mesh mesh = CloneMesh();
            var vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                var startPoint = transform.position + vertices[i];
                startPoint.y = 100;
                var direction = Vector3.down;

                var ray = new Ray(startPoint, direction);

                if (Physics.Raycast(ray, out var hitInfo, _layerMask))
                {
                    Debug.Log(hitInfo.point.y);
                    float y = hitInfo.point.y - transform.position.y + _verticalOffset;
                    vertices[i].y = y;

                }
            }

            mesh.vertices = vertices;

            _meshFilter.sharedMesh = mesh;

        }

        Mesh CloneMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = _meshFilter.sharedMesh.vertices;
            mesh.triangles = _meshFilter.sharedMesh.triangles;
            mesh.uv = _meshFilter.sharedMesh.uv;
            mesh.normals = _meshFilter.sharedMesh.normals;

            return mesh;
        }



    }
}