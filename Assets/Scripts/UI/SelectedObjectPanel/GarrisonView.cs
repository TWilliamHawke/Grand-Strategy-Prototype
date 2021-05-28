using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarrisonView : UnitListPanel
{
    public Vector3 settlementPosition => _unitsOwner.position;
    public bool AnyCardSelected() => selectedCards.Count != 0;

    [SerializeField] Button _addUnitButton;
    [SerializeField] RecruitmentPanel _unitSelector;
    [SerializeField] UnitsListController _unitListController;

    void Start()
    {
        _addUnitButton.onClick.AddListener(_unitSelector.Show);
    }

    private void OnEnable()
    {
        SettlementData.OnUnitAdded += UpdateUnitsCards;
    }

    private void OnDisable()
    {
        SettlementData.OnUnitAdded -= UpdateUnitsCards;
        _unitSelector.Close();
    }

    public List<Unit> GetSelectedUnits()
    {
        var unitList = new List<Unit>();

        foreach (var unitcard in selectedCards)
        {
            unitList.Add(unitcard.unit);
        }

        return unitList;
    }

    void UpdateUnitsCards(UnitTemplate _)
    {
        UpdateUnitsCards();
    }

    protected override void UpdateUnitsCards()
    {
        base.UpdateUnitsCards();

        if (_unitListController.ForceIsFull(_unitsOwner))
        {
            _addUnitButton.gameObject.SetActive(false);
            _unitSelector.Close();
        }
        else
        {
            _addUnitButton.gameObject.SetActive(true);
        }
    }

}
