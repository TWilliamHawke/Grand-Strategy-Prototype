using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class FramePartGenerator : MonoBehaviour
    {
        enum Vectors
        {
            up,
            right,
            left,
            down
        }

        static Dictionary<Vectors, Vector3> _vectorsList = new Dictionary<Vectors, Vector3>()
        {
            { Vectors.up, Vector3.forward },
            { Vectors.right, Vector3.right },
            { Vectors.left, Vector3.left },
            { Vectors.down, Vector3.back },
        };

        [SerializeField] MapConfig _mapConfig;
        [SerializeField] Vectors _directionFromCorner;
        [SerializeField] Vectors _widthShift;
        [SerializeField] [Range(0, 3)] int _startCornerIndex;
        [SerializeField] bool _startFromCenter;

        //components
        MeshFilter _meshFilter;
        MeshRenderer _meshRenderer;

        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;
        ChunkGenerator _chunkGenerator;

        //1 - up, -1 = down, 0 = flat
        float _slopeType = 0;

        public void GenerateMesh(ChunkGenerator chunkGenerator)
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _chunkGenerator = chunkGenerator;

            Vector3 start = FindStartPosition(chunkGenerator);
            Vector3 width = GetVectorFromList(_widthShift);
            width = width * chunkGenerator.frameWidth * _mapConfig.chunkSize / 100;
            Vector3 step = GetVectorFromList(_directionFromCorner);
            FindSlopeDirection(chunkGenerator.corners, step);

            float stepMult = (49 - chunkGenerator.frameWidth - chunkGenerator.framePadding) / 100f;
            step = step * stepMult;
            transform.localPosition = new Vector3(0, 0, 0) + ChunkGenerator.meshOffset;

            GenerateLine(start, step, width);
        }

        Vector3 FindStartPosition(ChunkGenerator chunkGenerator)
        {
            Vector3 startCorner = chunkGenerator.corners[_startCornerIndex];
            int index = _startCornerIndex + 2;
            int oppositeCornerIndex = index > 3 ? index - 4 : index;
            Vector3 oppositeCorner = chunkGenerator.corners[oppositeCornerIndex];
            float position = (chunkGenerator.frameWidth + chunkGenerator.framePadding) / 100f;

            var targetCorner = Vector3.Lerp(startCorner, oppositeCorner, position);

            return targetCorner;
        }

        Vector3 GetVectorFromList(Vectors vectorType)
        {
            var vector = Vector3.zero;

            if (_vectorsList.TryGetValue(vectorType, out var v))
            {
                vector = v;
            }

            return vector;
        }

        void GenerateLine(Vector3 startPoint, Vector3 step, Vector3 width)
        {
            _mesh = new Mesh();
            _meshFilter.sharedMesh = _mesh;

            _vertices = new Vector3[_mapConfig.chunkLines * 2];

            Vector3 point = startPoint;
            float startDistance = (_chunkGenerator.frameWidth + _chunkGenerator.framePadding) / 100f;

            for (int i = 0, x = 0; x < _mapConfig.chunkLines; x++)
            {
                Vector3 stepSum = step * x;
                var distance = startDistance + stepSum.magnitude / _mapConfig.chunkSize;
                float y = GetY(distance);

                _vertices[i] = new Vector3(point.x, y, point.z);
                i++;
                var point2 = point + width;
                _vertices[i] = new Vector3(point2.x, y, point2.z);
                i++;
                point += step;
            }

            _triangles = new int[_mapConfig.chunkSize * 6];
            int ti = 0;

            for (int j = 0; j < _mapConfig.chunkSize; j++)
            {
                _triangles[ti++] = j * 2 + 0;
                _triangles[ti++] = _startFromCenter ? j * 2 + 2 : j * 2 + 1;
                _triangles[ti++] = _startFromCenter ? j * 2 + 1 : j * 2 + 2;
                _triangles[ti++] = _startFromCenter ? j * 2 + 1 : j * 2 + 2;
                _triangles[ti++] = _startFromCenter ? j * 2 + 2 : j * 2 + 1;
                _triangles[ti++] = j * 2 + 3;
            }


            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
            _meshRenderer.materials[0].color = _mapConfig.battleRules.defaultColor;


        }

        float GetY(float distance)
        {
            float startHeight = _chunkGenerator.corners[_startCornerIndex].y;
            float padding = 0.2f;
            if (_slopeType == 0)
            {
                return (startHeight) * _mapConfig.heightPerLevel + padding;
            }
            else
            {
                float height = _mapConfig.slopeCurve.Evaluate(distance);

                if (_slopeType == 1)
                {

                    return (startHeight + height) * _mapConfig.heightPerLevel + padding;
                }
                else
                {
                    return (startHeight - height) * _mapConfig.heightPerLevel + padding;
                }
            }

        }

        void FindSlopeDirection(Vector3[] corners, Vector3 step)
        {
            Vector3 startCorner = corners[_startCornerIndex];
            Vector3 targetCorner = corners[_startCornerIndex] + step * _mapConfig.chunkSize;

            foreach (var corner in corners)
            {
                if (targetCorner.x == corner.x && targetCorner.z == corner.z)
                {
                    targetCorner.y = corner.y;
                }
            }

            _slopeType = targetCorner.y - startCorner.y;
        }
    }
}