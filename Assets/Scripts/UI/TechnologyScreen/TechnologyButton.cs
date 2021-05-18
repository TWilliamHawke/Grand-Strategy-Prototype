using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TechnologyButton : UIDataElement<Technology>
{
    [SerializeField] TechnologiesController _techController;
    [Header("Frame Colors")]
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _selectedColor;
    [SerializeField] Color _researchedColor;

    [Header("UI Elements")]
    [SerializeField] Image _frame;
    [SerializeField] Image _tick;
    [SerializeField] Text _textField;

    //cache
    Button _button;

    //data
    public Technology technologyData { get; private set; }
    bool _isResearched = false;


    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectTechnology);
        _tick.color = _researchedColor;
    }

    void SelectTechnology()
    {
        if(_isResearched) return;

        _frame.color = _selectedColor;
        _techController.Select(this);
    }

    public override void UpdateData(Technology data)
    {
        _textField.text = data.localizedName;
        technologyData = data;

        if(_techController.researchedTechnologies.Contains(technologyData))
        {
            MarkAsResearched();
        }
    }

    public override string GetTooltipText()
    {
        var text = technologyData.GetEffectsDescription();
        return text;
    }

    public void MarkAsResearched()
    {
        _tick.gameObject.SetActive(true);
        _frame.color = _researchedColor;
        _isResearched = true;
    }
}
