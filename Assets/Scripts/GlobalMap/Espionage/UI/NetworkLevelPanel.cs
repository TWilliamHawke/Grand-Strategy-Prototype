using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class NetworkLevelPanel : UIPanelWithGrid<SpyNetworkLevel>, INeedInit
    {
        [SerializeField] SpyNetworkController _networkController;
        [SerializeField] Text _networkHeader;

        const string HEADER_START = "Spy Network Level ";

        protected override List<SpyNetworkLevel> _layoutElementsData => _networkController.levels;

        private void OnEnable()
        {
            UpdateLayout();
        }

        public void Init()
        {
            _networkController.OnSpyNetworkUpdate += UpdateHeader;
        }

        void OnDestroy()
        {
            _networkController.OnSpyNetworkUpdate -= UpdateHeader;
        }


        protected override void UpdateLayout()
        {
            ClearLayout();

            for (int i = 0; i < _networkController.levels.Count; i++)
            {
                var level = Instantiate(prefab) as NetworkScalePart;
                level.transform.SetParent(layout.transform);
                level.UpdateData(_networkController.levels[i]);
                level.SetLevelNumber(i + 1);
            }
        }

        void UpdateHeader()
        {
            _networkHeader.text = HEADER_START + _networkController.networkData.level.ToString();
        }

    }
}