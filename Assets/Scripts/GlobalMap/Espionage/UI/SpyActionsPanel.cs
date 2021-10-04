using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace GlobalMap.Espionage.UI
{
    public class SpyActionsPanel : UIPanelWithGrid<SpyAction>
    {
        [SerializeField] SpyNetworkController _controller;

        protected override List<SpyAction> _layoutElementsData => SelectSpyActionsToRender();

        void Awake()
        {
            _controller.OnSpyNetworkUpdate += UpdateLayout;
        }

        void OnEnable()
        {
            //if diplomacy screen is active in editor, onEnable calls before controller init
            if(_controller.networkData == null) return;
            UpdateLayout();
        }

        void OnDestroy()
        {
            _controller.OnSpyNetworkUpdate -= UpdateLayout;
        }

        List<SpyAction> SelectSpyActionsToRender()
        {
            return _controller.levels
                    .Where((_, idx) => idx < _controller.networkData.level)
                    .SelectMany(level => level.activeActions)
                    .ToList();

        }
    }
}