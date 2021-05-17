using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitEditor;

public class UnitTemplateViewer : UIScreen
{
    [SerializeField] Image _classSelector;
    [SerializeField] TemplateController _templateController;
    [SerializeField] UIScreensManager _uiScreenManager;
    UnitEditorScreen _unitEditor;

    void OnEnable()
    {
        _unitEditor = FindObjectOfType<UnitEditorScreen>(true);
        HideClassSelector();
    }

    public void HideClassSelector()
    {
        _classSelector.gameObject.SetActive(false);
    }

}
