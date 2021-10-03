using System.Collections;
using System.Collections.Generic;
using Battlefield;
using GlobalMap.Diplomacy;
using GlobalMap.Factions;
using UnityEngine;
using UnityEngine.Events;

namespace GlobalMap.Espionage
{
    [CreateAssetMenu(fileName = "SpyNetworkLevels", menuName = "Global map/Spy Network Controller")]
    public class SpyNetworkController : ScriptableObject
    {
        [SerializeField] DiplomacyController _diplomacyController;
        [SerializeField] Timer _timer;
        [Header("Config")]
        [SerializeField] Color _networkLevelsColor = Color.green;
        [SerializeField] float _spyPointsPerMonth = 5f;
        [SerializeField] GuardState _defaultGuardState;

        [SerializeField] List<SpyNetworkLevel> _levels = new List<SpyNetworkLevel>();

        SpyNetworkData _targetFactionNetworkData;
        Dictionary<SpyAction, ISpyActionWindow> _windows = new Dictionary<SpyAction, ISpyActionWindow>();

        //getters
        public List<SpyNetworkLevel> levels => _levels;
        public Color networkLevelsColor => _networkLevelsColor;
        public float spyPointsPerMonth => _spyPointsPerMonth;
        public float visibilityDecreaces => currentGuardState.visibilityPerMonth;
        public SpyNetworkData networkData => _targetFactionNetworkData;
        public int maxVisibility => currentGuardState.visibilityCap;
        public GuardState currentGuardState => networkData?.GetGuardState(0) ?? _defaultGuardState;

        //events
        public event UnityAction OnSpyNetworkUpdate;
        public event UnityAction OnSpyNetworkLevelUp;

        void OnEnable()
        {
            _diplomacyController.OnFactionSelect += SetNetworkData;
            _timer.OnMonthChange += UpdateNetworkData;
        }

        public void SpendSpyPoints(int points)
        {
            if (_targetFactionNetworkData.spyPoints < points) return;
            _targetFactionNetworkData.spyPoints -= points;
        }

        public void IncreaseNetworkLevel()
        {
            if (networkData.level >= levels.Count) return;
            networkData.level++;
            OnSpyNetworkUpdate?.Invoke();
            OnSpyNetworkLevelUp?.Invoke();
        }

        public void AddWindow(ISpyActionWindow window)
        {
            _windows[window.spyAction] = window;
        }

        public void OpenWindow(SpyAction action)
        {
            if (_windows.TryGetValue(action, out var window))
            {
                window.Open();
            }
        }

        public GuardState GetGuardState(int pointer)
        {
            return _targetFactionNetworkData.GetGuardState(pointer);
        }



        void SetNetworkData(Faction faction)
        {
            _targetFactionNetworkData = new SpyNetworkData(faction);
            _targetFactionNetworkData.level = 1;
            OnSpyNetworkUpdate?.Invoke();
        }

        void UpdateNetworkData()
        {
            _targetFactionNetworkData.spyPoints += _spyPointsPerMonth;

            _targetFactionNetworkData.visibility += visibilityDecreaces;
            if (_targetFactionNetworkData.visibility < 0)
            {
                _targetFactionNetworkData.visibility = 0;
            }

            OnSpyNetworkUpdate?.Invoke();
        }



    }

    public interface ISpyActionWindow
    {
        SpyAction spyAction { get; }
        void Open();
    }

}