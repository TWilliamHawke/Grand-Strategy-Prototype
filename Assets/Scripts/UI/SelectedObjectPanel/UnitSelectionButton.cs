using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Effects;

public class UnitSelectionButton : UIDataElement<UnitTemplate>, IPointerClickHandler
{
    [Header("UI Elements")]
    [SerializeField] Image _unitIcon;
    [SerializeField] Text _unitName;
    [SerializeField] Text _unitCount;

    const string INFINITY_SYMBOL = "\u221E";

    //HACK replace this by selectionController scriptableObject
    static Settlement _selectedSetlement => Settlement.selectedSettlement;

    Button _button;

    //dynamic data
    UnitTemplate _thisButtonTemplate;
    string _tooltipText;

    //config
    bool _isDisable = false;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        Settlement.OnUnitAdded += UpdateUnitCapForRecruitedClass;
    }

    private void OnDisable()
    {
        Settlement.OnUnitAdded -= UpdateUnitCapForRecruitedClass;
    }

    public override string GetTooltipText()
    {
        return _tooltipText;
    }

    public override void UpdateData(UnitTemplate data)
    {
        if (_selectedSetlement == null) return;

        _thisButtonTemplate = data;
        _unitIcon.sprite = data.unitClass.defaultIcon;
        _unitName.text = data.templateName;
        SetDefaultTooltip();
        CheckRequirementBuildings();
        CheckUnitCap();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isDisable) return;
        HideTooltip();
        _selectedSetlement.AddUnit(_thisButtonTemplate);
    }

    void CheckRequirementBuildings()
    {
        var missingBuildings = _thisButtonTemplate.requiredBuildings
            .Except(_selectedSetlement.constructedBuildings);

        if (missingBuildings.Count() > 0)
        {
            _tooltipText = "Unit need this buildings: ";

            foreach (var bulding in missingBuildings)
            {
                _tooltipText += bulding.localizedName + ", ";
            }
            MakeDisable();
        }
    }

    void UpdateUnitCapForRecruitedClass(UnitTemplate template)
    {
        if (template.unitClass == _thisButtonTemplate.unitClass)
        {
            CheckUnitCap();
        }

        //updateBlaBlaBla method called for all buttons in list
        //but tooltip should be updated only for hovered
        if (template == _thisButtonTemplate)
        {
            ShowTooltip();
        }
    }

    void CheckUnitCap()
    {
        int unitCap = _selectedSetlement.constructedBuildings
            .SelectMany(building => building.effects)
            .OfType<IncreaseCapForUnitClass>()
            .Where(effect => effect.unitClass == _thisButtonTemplate.unitClass)
            .Sum(effect => effect.unitCap);

        if (unitCap > 99)
        {
            _unitCount.text = INFINITY_SYMBOL;
            return;
        }

        int unitCount = _selectedSetlement.unitsFromThisSettelment
            .Count(unit => unit.unitTemplate.unitClass == _thisButtonTemplate.unitClass);

        _unitCount.text = $"{unitCount}/{unitCap}";

        if (unitCount >= unitCap)
        {
            MakeDisable();
            _tooltipText = "You reached max capacity for units of this type";
        }
        else
        {
            SetDefaultTooltip();
        }
    }

    void SetDefaultTooltip()
    {
        _tooltipText = "Click LMB for recruitment";
    }

    void MakeDisable()
    {
        _isDisable = true;
        _unitCount.color = Color.red;
        _unitName.color = Color.red;
        _button.interactable = false;
    }
}
