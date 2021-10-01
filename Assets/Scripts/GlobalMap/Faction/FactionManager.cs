using System.Collections;
using System.Collections.Generic;
using GlobalMap.Diplomacy;
using UnityEngine;
using System.Linq;

namespace GlobalMap.Factions
{
    public class FactionManager : MonoBehaviour
    {
        [SerializeField] DiplomacyController _diplomacyController;
        [SerializeField] List<FactionData> _factions = new List<FactionData>();

        List<Faction> _aiFactions = new List<Faction>();

        Faction _playerFaction;

        public List<Faction> aiFactions => _aiFactions;

        void Awake()
        {
            CreateFactions();

        }

        void Start()
        {
            _diplomacyController.playerFaction = _playerFaction;
            _diplomacyController.SetTargetFaction(_aiFactions.FirstOrDefault());

        }

        void CreateFactions()
        {
            foreach (var dataObject in _factions)
            {
                if (dataObject.isPlayerFaction)
                {
                    _playerFaction = new Faction(dataObject);
                }
                else
                {
                    _aiFactions.Add(new Faction(dataObject));
                }
            }
        }

    }
}