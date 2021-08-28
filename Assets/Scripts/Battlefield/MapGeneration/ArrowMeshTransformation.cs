using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class ArrowMeshTransformation : MonoBehaviour
    {
        static Dictionary<int, (float, float)> _sinCos = new Dictionary<int, (float, float)>() {
            { 0, (0, 1) },
            { 1, (.7f, .7f) },
            { 2, (1, 0) },
            { 3, (.7f, -.7f) },
            { 4, (0, -1) },
            { 5, (-.7f, -.7f) },
            { 6, (-1, 0) },
            { 7, (-.7f, .7f) },
        };


        [SerializeField] MapConfig _mapConfig;
        [SerializeField] float _verticalOffset = 0.2f;

        MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void ChangeShape(float angle)
        {
            int key = Mathf.RoundToInt(angle / 45);
            if (_sinCos.TryGetValue(key, out var pair))
            {
                ChangeShape(pair.Item1, pair.Item2);
            }
            else
            {
                Debug.Log($"Not found! {angle}");
            }
        }

        void ChangeShape(float sin, float cos)
        {
            Mesh mesh = CloneMesh();
            var vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                var scaleX = transform.localScale.x;
                var scaleZ = transform.localScale.z;
                //координаты после поворота
                float x = vertices[i].x * scaleX * cos + vertices[i].z * scaleZ * sin;
                float z = vertices[i].z * scaleZ * cos - vertices[i].x * scaleX * sin;

                var startPoint = transform.position + new Vector3(x, 0, z);

                var point = Raycasts.VerticalDown(startPoint, _mapConfig.gridLayer);
                float y = point.y - transform.position.y + _verticalOffset;
                vertices[i].y = y;
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