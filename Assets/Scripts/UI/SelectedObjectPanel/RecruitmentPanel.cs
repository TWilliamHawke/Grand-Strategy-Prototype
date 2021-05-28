using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class RecruitmentPanel : UIPanelWithGrid<UnitTemplate>
{
    [SerializeField] UnitsListController _unitListController;

    protected override List<UnitTemplate> _layoutElementsData => _unitListController.currentTemplates;

    private void OnEnable()
    {
        UpdateGrid();
        Canvas.ForceUpdateCanvases();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}
