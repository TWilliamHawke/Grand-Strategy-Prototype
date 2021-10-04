using System.Collections;
using System.Collections.Generic;
using Battlefield;
using GlobalMap.Diplomacy;
using GlobalMap.Espionage.UI;
using GlobalMap.Factions;
using UnityEngine;
using UnityEngine.Events;

namespace GlobalMap.Espionage
{
    [CreateAssetMenu(fileName = "SpyNetworkLevels", menuName = "Global map/Spy Network Controller")]
    public class SpyNetworkController : ScriptableObject
    {
        [SerializeField] Timer _timer;
        [Header("Config")]
        [SerializeField] Color _networkLevelsColor = Color.green;
        [SerializeField] float _spyPointsPerMonth = 5f;
        [SerializeField] GuardState _defaultGuardState;

        [SerializeField] List<SpyNetworkLevel> _levels = new List<SpyNetworkLevel>();

        SpyNetworkData _targetFactionNetworkData;
        Dictionary<SpyAction, ISpyActionWindow> _windows = new Dictionary<SpyAction, ISpyActionWindow>();
        TestActionWindow _testWindow;

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
        public event UnityAction OnSpyActionSuccess;
        public event UnityAction OnSpyActionFailure;

        void OnEnable()
        {
            _timer.OnMonthChange += UpdateNetworkData;
        }

        public void ExecuteSpyAction(SpyAction action)
        {
            var result = Random.Range(0, 100);
            networkData.spyPoints -= action.cost;
            networkData.visibility += action.visibility;

            if(result < action.successChance)
            {
                OnSpyActionSuccess?.Invoke();
            }
            else
            {
                OnSpyActionFailure?.Invoke();
            }

            if (networkData.visibility >= currentGuardState.visibilityCap)
            {
                DestroySpyNetwork();
            }

            _testWindow.ShowResultPanel(result);
            OnSpyNetworkUpdate?.Invoke();
        }

        void DestroySpyNetwork()
        {
            networkData.level = 1;
            networkData.spyPoints = 0;
            networkData.visibility = 0;
        }

        public void IncreaseNetworkLevel()
        {
            int level = networkData.level;
            
            if (level>= levels.Count) return;
            if (networkData.spyPoints < _levels[level].requiredSpyPoints) return;

            networkData.level++;
            networkData.spyPoints -= _levels[level].requiredSpyPoints;

            OnSpyNetworkUpdate?.Invoke();
            OnSpyNetworkLevelUp?.Invoke();
        }

        public void AddTestWindow(TestActionWindow window)
        {
            _testWindow = window;
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

            _testWindow.Open(action);
        }

        public GuardState GetGuardState(int pointer)
        {
            return networkData.GetGuardState(pointer);
        }

        public void SetNetworkData(Faction faction)
        {
            _targetFactionNetworkData = new SpyNetworkData(faction);
            networkData.level = 1;
            OnSpyNetworkUpdate?.Invoke();
        }

        //execute every month
        public void UpdateNetworkData()
        {
            networkData.spyPoints += _spyPointsPerMonth;
            networkData.ChangeGuardState();

            networkData.visibility += visibilityDecreaces;
            if (networkData.visibility < 0)
            {
                networkData.visibility = 0;
            }

            OnSpyNetworkUpdate?.Invoke();
        }



    }

}