using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Generator
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BordersGenerator : MonoBehaviour
    {
        GeneratorConfig _config;

        //components
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;

        //meshData
        Mesh _bordersMesh;
        List<Vector3> _verticies = new List<Vector3>();
        List<int> _triangles = new List<int>();


        public void Generate(GeneratorConfig config, ChunkGenerator chunk)
        {
            _config = config;
            _bordersMesh = new Mesh();


            var thicknessOffsetHorizontal = new Vector3(-_config.bordersThickness, 0, 0);
            var thicknessOffsetVertical = new Vector3(0, 0, _config.bordersThickness);

            for (int z = chunk.startZ; z < chunk.endZ; z++)
            {
                Color thisPixelColor = _config.provinceMap.GetPixel(chunk.startX, z);
                for (int x = chunk.startX; x < chunk.endX; x++)
                {
                    Color nextPixelHorizontal = _config.provinceMap.GetPixel(x + 1, z);
                    Color nextPixelVertical = _config.provinceMap.GetPixel(x, z + 1);

                    if (thisPixelColor != nextPixelHorizontal)
                    {
                        AddBorder(x, z, Vector3.back, thicknessOffsetHorizontal);
                    }

                    if (thisPixelColor != nextPixelVertical)
                    {
                        AddBorder(x, z, Vector3.left, thicknessOffsetVertical);
                    }

                    thisPixelColor = nextPixelHorizontal;
                }

            }

            _bordersMesh.vertices = _verticies.ToArray();
            _bordersMesh.triangles = _triangles.ToArray();
            _bordersMesh.RecalculateNormals();
            _filter.sharedMesh = _bordersMesh;
			_renderer.materials[0].color = _config.bordersColor;

        }

        private void AddBorder(int x, int z, Vector3 borderDirection, Vector3 thicknessOffset)
        {
            int verticeIndex = _verticies.Count;

            var borderStart = new Vector3(x + 1, 10, z + 1);
            var borderEnd = borderStart + borderDirection;

            borderStart = VectorHeightToMap(borderStart);
            borderEnd = VectorHeightToMap(borderEnd);

            _verticies.Add(borderStart + thicknessOffset);
            _verticies.Add(borderStart - thicknessOffset);
            _verticies.Add(borderEnd - thicknessOffset);
            _verticies.Add(borderEnd + thicknessOffset);

            _triangles.Add(verticeIndex);
            _triangles.Add(verticeIndex + 1);
            _triangles.Add(verticeIndex + 2);
            _triangles.Add(verticeIndex);
            _triangles.Add(verticeIndex + 2);
            _triangles.Add(verticeIndex + 3);

        }

        public Vector3 VectorHeightToMap(Vector3 vector)
        {
            var height = _config.FindHeight(vector.x, vector.z);

            vector.y = Mathf.Max(height, 0) + .05f;
            return vector;
        }

    }
}