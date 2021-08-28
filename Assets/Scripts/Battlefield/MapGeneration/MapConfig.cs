using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Battlefield/Map Config")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] int _gridSize;
        [SerializeField] int _chunksBehindGrid;
        [SerializeField] float _heightPerLevel;
        [SerializeField] int _chunkSize;
        [Range(0, 50)]
        [SerializeField] float _hillPercentage;
        [SerializeField] int _seed;
        [SerializeField] AnimationCurve _slopeCurve;
        [SerializeField] BattleRules _battleRules;
        [SerializeField] LayerMask _gridLayer;

        IGenerationAlgorithm _generationAlgorithm;

        public int gridSize => _gridSize;
        public int linesCount => _gridSize + _chunksBehindGrid * 2 + 1;
        public float heightPerLevel => _heightPerLevel;
        public int chunkSize => _chunkSize;
        public int chunkLines => _chunkSize + 1;
        public float hillPercentage => _hillPercentage;
        public int seed => _seed;
        public IGenerationAlgorithm generationAlgorithm => _generationAlgorithm;
        public AnimationCurve slopeCurve => _slopeCurve;
        public BattleRules battleRules => _battleRules;
        public int chunksBehindGrid => _chunksBehindGrid;
        public LayerMask gridLayer => _gridLayer;


        private void OnEnable()
        {
            _generationAlgorithm = new DefaultAlgorithm(this);
        }

        public void SetAlgorithm(IGenerationAlgorithm algorithm)
        {
            _generationAlgorithm = algorithm;
        }

        public void GenerateNewSeed()
        {
            var rng = new System.Random();
            _seed = rng.Next();
        }
    }

}