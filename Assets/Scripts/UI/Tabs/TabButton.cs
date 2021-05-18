using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainUI.Tabs
{
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TabsController _tabsController;
        [SerializeField] TabPanel _tabPanel;


        public void OnPointerClick(PointerEventData eventData)
        {
            _tabsController.ShowPanel(this);
        }

        public void Awake()
        {
            _tabsController.AddPair(this, _tabPanel);
        }

    }

}