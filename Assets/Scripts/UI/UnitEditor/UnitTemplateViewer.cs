using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitEditor;

public class UnitTemplateViewer : UIScreen
{
    [SerializeField] Image _classSelector;
    [SerializeField] Button _editTemplateButton;
    [SerializeField] TemplateController _templateController;
    [SerializeField] UIScreensManager _uiScreenManager;
    UnitEditorScreen _unitEditor;

    void Awake()
    {
        _editTemplateButton.interactable = false;
        _templateController.OnTemplateSelection += EnableEditing;
        _editTemplateButton.onClick.AddListener(OpenEditor);
    }

    void OnDestroy()
    {
        _templateController.OnTemplateSelection -= EnableEditing;
    }

    void OnEnable()
    {
        _unitEditor = FindObjectOfType<UnitEditorScreen>(true);
        HideClassSelector();
    }

    public void HideClassSelector()
    {
        _classSelector.gameObject.SetActive(false);
    }

    public void EnableEditing()
    {
        _editTemplateButton.interactable = true;
    }

    public void OpenEditor()
    {
        var editor = FindObjectOfType<UnitEditorScreen>(true);
        _uiScreenManager.OpenScreen(editor);
    }



}
