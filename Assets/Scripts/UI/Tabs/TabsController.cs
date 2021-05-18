using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI.Tabs
{

    [CreateAssetMenu(fileName = "TabsController", menuName = "UI/Tabs Controller")]
    public class TabsController : ScriptableObject
    {
        Dictionary<TabButton, TabPanel> _tabButtons = new Dictionary<TabButton, TabPanel>();

        private void OnEnable()
        {
            _tabButtons.Clear();
        }

        public void AddPair(TabButton button, TabPanel panel)
        {
            if (!_tabButtons.ContainsKey(button))
                _tabButtons.Add(button, panel);
        }

        public void ShowPanel(TabButton button)
        {
            foreach (var pair in _tabButtons)
            {
                if (pair.Key == button)
                {
                    pair.Value.Show();
                }
                else
                {
                    pair.Value.Hide();
                }
            }
        }
    }

}
