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

    Button _button;

    //dynamic data
    UnitTemplate _currentTemplate;
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
        if (Settlement.selectedSettlement == null) return;

        _currentTemplate = data;
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
        Settlement.selectedSettlement.AddUnit(_currentTemplate);
    }

    void CheckRequirementBuildings()
    {
        var missingBuildings = _currentTemplate.requiredBuildings
            .Except(Settlement.selectedSettlement.constructedBuildings);

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
        if (template.unitClass == _currentTemplate.unitClass)
        {
            CheckUnitCap();
        }

        //update method called for all buttons in list
        //but tooltip should be updated only for hovered
        if (template == _currentTemplate)
        {
            ShowTooltip();
        }
    }

    void CheckUnitCap()
    {
        var maxCount = Settlement.selectedSettlement.constructedBuildings
            .SelectMany(b => b.effects)
            .OfType<IncreaseCapForUnitClass>()
            .Where(e => e.unitClass == _currentTemplate.unitClass)
            .Sum(e => e.unitCap);

        if (maxCount > 99)
        {
            _unitCount.text = INFINITY_SYMBOL;
            return;
        }

        int count = Settlement.selectedSettlement.unitsFromThisSettelment
            .Where(u => u.unitTemplate.unitClass == _currentTemplate.unitClass)
            .Count();

        _unitCount.text = $"{count}/{maxCount}";

        if (count >= maxCount)
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
