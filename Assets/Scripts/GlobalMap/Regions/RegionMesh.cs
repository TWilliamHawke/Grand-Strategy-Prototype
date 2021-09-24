using System;
using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using Helpers;
using UnityEngine;

namespace GlobalMap.Regions
{
    public class RegionMesh : AbstractMeshGenerator, ISelectable
    {
        [SerializeField] GeneratorConfig _config;
        [SerializeField] Region _region;

        List<Vector3> _vertices = new List<Vector3>();
        List<int> _triangles = new List<int>();

        RegionPosition _position = new RegionPosition();

        public Region region => _region;



        public void AddPoint(int x, int z)
        {
            _position.AddPoint(x, z);
        }

        public void SetColor(Color color)
        {
            meshRenderer.materials[0].color = color;
        }

        public Vector3 FindCenterPosition()
        {
            var flatVector = _position.FindCenterPosition();
            return UpdateVectorHeight(flatVector);
        }

        public void Select()
        {
            _region.castle.Select();
        }

        public void Deselect()
        {
            _region.castle.Deselect();
        }


        public override void GenerateMesh()
        {
            int vi = 0;

            foreach (var point in _position.points)
            {
                var bottomLeftVertice = new Vector3(point.x, 10, point.y);
                _vertices.Add(bottomLeftVertice);
                _vertices.Add(bottomLeftVertice + Vector3.forward);
                _vertices.Add(bottomLeftVertice + Vector3.right);
                _vertices.Add(bottomLeftVertice + Vector3.right + Vector3.forward);

                _triangles.Add(vi);
                _triangles.Add(vi + 1);
                _triangles.Add(vi + 2);
                _triangles.Add(vi + 2);
                _triangles.Add(vi + 1);
                _triangles.Add(vi + 3);
                vi += 4;
            }

            RemoveDublicates();
            UpdateVerticesHeight();

            base.GenerateMesh();
        }

        private void UpdateVerticesHeight()
        {
            for (int i = 0; i < _vertices.Count; i++)
            {

                _vertices[i] = UpdateVectorHeight(_vertices[i]);
            }
        }

        public Vector3 UpdateVectorHeight(Vector3 vector)
        {
            var height = _config.FindHeight(vector.x, vector.z);

            vector.y = Mathf.Max(height, 0);
            return vector;
        }


        private void RemoveDublicates()
        {
            //key - vertice, value - index in new list
            Dictionary<Vector3, int> uniqueVertices = new Dictionary<Vector3, int>();

            //key - index in old list, value - index in new list
            Dictionary<int, int> updatedIndexes = new Dictionary<int, int>();

            //vertice in this list does not repeat
            List<Vector3> updatedVerticies = new List<Vector3>();

            for (int i = 0; i < _vertices.Count; i++)
            {
                var vertice = _vertices[i];

                if (uniqueVertices.TryGetValue(vertice, out var newIndex))
                {
                    //vertice is not unique
                    updatedIndexes.Add(i, newIndex);
                }
                else
                {
                    //vertice found first time, save index in updated list
                    updatedIndexes.Add(i, updatedVerticies.Count);
                    uniqueVertices.Add(vertice, updatedVerticies.Count);
                    updatedVerticies.Add(vertice);
                }

            }

            for (int j = 0; j < _triangles.Count; j++)
            {
                if (updatedIndexes.TryGetValue(_triangles[j], out var newIndex))
                {
                    _triangles[j] = newIndex;
                }
            }

            _vertices = updatedVerticies;

        }

        protected override Vector3[] CreateVerticies()
        {
            return _vertices.ToArray();
        }

        protected override int[] CreateTriangles()
        {
            return _triangles.ToArray();
        }

    }
}