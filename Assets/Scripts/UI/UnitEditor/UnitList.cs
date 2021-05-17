using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitList : UIPanelWithGrid<UnitTemplate>
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] Image _classSelector;

        void Awake()
        {
            _templateController.OnTemplateSave += SaveTemplate;
            FillGridElementsList();
        }

        void OnDestroy()
        {
            _templateController.OnTemplateSave -= SaveTemplate;
        }

        void OnEnable()
        {
            UpdateGrid();
        }

        void SaveTemplate(UnitTemplate savedTemplate)
        {
            var template = Instantiate(savedTemplate);

            if (template.canNotEdit)
            {
                AddTemplateAsNew(template);
            }
            else
            {
                ReplaceTemplate(template);
            }
        }

        private void AddTemplateAsNew(UnitTemplate template)
        {
            template.canNotEdit = false;
            _gridElementsData.Add(template);
        }

        void ReplaceTemplate(UnitTemplate template)
        {
            for (int i = 0; i < _gridElementsData.Count; i++)
            {
                if (_gridElementsData[i] == _templateController.defaultTemplate)
                {
                    _gridElementsData[i] = template;
                }
            }
            UpdateGrid();
        }

        protected override void FillGridElementsList()
        {
            _gridElementsData.AddRange(_templateController.defaultTemplates);
        }

        protected override void PlusButtonListener()
        {
            _classSelector.gameObject.SetActive(true);
        }
    }

}