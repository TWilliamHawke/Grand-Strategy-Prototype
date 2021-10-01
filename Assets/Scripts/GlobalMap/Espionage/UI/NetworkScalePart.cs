using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace GlobalMap.Espionage.UI
{
    public class NetworkScalePart : UIDataElement<SpyNetworkLevel>
    {
        [SerializeField] Image _background;
        [SerializeField] SpyNetworkController _netWorkController;

        string _tooltipText;
        bool _isUnlocked;
        SpyNetworkLevel _levelData;
        int _level;

        string _prevLevelWarning = "<color=red>You should unlock previous level</color>";

        public bool isUnlocked => _isUnlocked;

        void Awake()
        {
            _netWorkController.OnSpyNetworkUpdate += CheckUnlockState;
        }

        void OnDestroy()
        {
            _netWorkController.OnSpyNetworkUpdate -= CheckUnlockState;
        }

        public override string GetTooltipText()
        {
            return CreateTooltipText();
        }

        public void Unlock()
        {
            _background.color = _netWorkController.networkLevelsColor;
            _isUnlocked = true;
        }

        public override void UpdateData(SpyNetworkLevel data)
        {
            _levelData = data;
        }

        public void SetLevelNumber(int level)
        {
            _level = level;
        }

        void CheckUnlockState()
        {
            if (_netWorkController.networkData.level < _level) return;
            Unlock();
        }

        string CreateTooltipText()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Spy Network level {_level}");
            if (!_isUnlocked)
            {
                sb.AppendLine();
                if (_netWorkController.networkData.level + 1 >= _level)
                {
                    sb.AppendLine($"Required {_levelData.requiredSpyPoints} spy points to unlock");
                }
                else
                {
                    sb.AppendLine(_prevLevelWarning);
                }
            }
            if (_levelData.activeActions.Count > 0)
            {
                sb.AppendLine();
                sb.Append("Possible active actions:");
                foreach (var action in _levelData.activeActions)
                {
                    sb.AppendLine();
                    sb.Append("-");
                    sb.Append(action.title);
                }
            }
            return sb.ToString();
        }


    }
}