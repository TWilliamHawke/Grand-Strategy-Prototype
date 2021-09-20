using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public abstract class AbstractMeshGenerator : MonoBehaviour
    {
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] MeshCollider _collider;

        protected MeshRenderer meshRenderer => _renderer;
        protected MeshCollider meshCollider => _collider;

        abstract protected Vector3[] CreateVerticies();
        abstract protected int[] CreateTriangles();

        public virtual void GenerateMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = CreateVerticies();
            mesh.triangles = CreateTriangles();
            mesh.RecalculateNormals();
			mesh.uv = UpdateTextureGrid(mesh.vertices);

            _filter.sharedMesh = mesh;
        }

        Vector2[] UpdateTextureGrid(Vector3[] vertices)
        {
            Vector2[] uvs = new Vector2[vertices.Length];

            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
            }

            return uvs;
        }
    }
}