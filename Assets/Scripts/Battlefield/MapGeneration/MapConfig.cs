using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Battlefield/Map Config")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] int _mapSize;
        [SerializeField] int _heightPerLevel;
        [SerializeField] int _chunkSize;
        [Range(0, 50)]
        [SerializeField] float _hillPercentage;
        [SerializeField] int _seed;

        IGenerationAlgorithm _generationAlgorithm;

        public int mapSize => _mapSize;
        public int linesCount => _mapSize + 1;
        public int heightPerLevel => _heightPerLevel;
        public int chunkSize => _chunkSize;
        public float hillPercentage => _hillPercentage;
        public int seed => _seed;
        public IGenerationAlgorithm generationAlgorithm => _generationAlgorithm;


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