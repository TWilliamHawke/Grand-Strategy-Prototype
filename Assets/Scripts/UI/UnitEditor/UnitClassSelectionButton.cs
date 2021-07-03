using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitClassSelectionButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] UnitTemplate _unitTemplate;
        [SerializeField] UIScreensManager _screenManager;

        [Header("UI Elements")]
        [SerializeField] Image _imageForIcon;
        [SerializeField] Text _templateName;
        [SerializeField] Text _templateDescription;

        UnitEditorScreen _unitEditor;

        private void Awake()
        {
            _unitEditor = FindObjectOfType<UnitEditorScreen>(true);
            UpdateUnitInfo();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _templateController.SelectTemplate(_unitTemplate);
            _screenManager.OpenScreen(_unitEditor);
        }

        private void UpdateUnitInfo()
        {
            _imageForIcon.sprite = _unitTemplate.unitClass.defaultIcon;
            _templateName.text = _unitTemplate.unitClass.className;
            _templateDescription.text = _unitTemplate.unitClass.description;
        }

    }
}