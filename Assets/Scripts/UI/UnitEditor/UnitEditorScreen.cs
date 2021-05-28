using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorScreen : UIScreen
{
    [SerializeField] Image _nameSelector;

    void OnDisable()
    {
        HideNameSelector();
    }

    void OnEnable()
    {
        HideNameSelector();
    }

    public void ShowNameSelector()
    {
        if(_nameSelector.gameObject.activeSelf) return;
    
        _nameSelector.gameObject.SetActive(true);
    }

    public void HideNameSelector()
    {
        _nameSelector.gameObject.SetActive(false);
    }
}
