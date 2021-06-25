using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitListController", menuName = "Core Game/Unit List")]
public class UnitsListController : ScriptableObject
{
    [SerializeField] List<UnitTemplate> _defaultUnits;
    [SerializeField] List<UnitTemplate> _mecrcenaryUnits;
    [SerializeField] TemplateController _templateController;
    //HACK this value should store in special config class
    [SerializeField] int _maxUnitsPerArmy = 7;

    List<UnitTemplate> _currentTemplates = new List<UnitTemplate>();

    //getters
    public List<UnitTemplate> mecrcenaryUnits => _mecrcenaryUnits;
    public List<UnitTemplate> currentTemplates => _currentTemplates;

    void OnEnable()
    {
        _currentTemplates.Clear();
        _currentTemplates.AddRange(_defaultUnits);
    }

    //HACK move this somewhere
    public bool ForceIsFull(IHaveUnits unitsOwner)
    {
        return unitsOwner.unitList.Count >= _maxUnitsPerArmy;
    }

    public void SaveUnitTemplate(UnitTemplate savedTemplate)
    {
        var template = savedTemplate.Clone();

        if (template.canNotEdit)
        {
            AddTemplateAsNew(template);
        }
        else
        {
            ReplaceExistingTemplate(template);
        }
    }

    void AddTemplateAsNew(UnitTemplate template)
    {
        template.AllowEdit();
        _currentTemplates.Add(template);
    }

    void ReplaceExistingTemplate(UnitTemplate template)
    {
        for (int i = 0; i < _currentTemplates.Count; i++)
        {
            if (_currentTemplates[i] == _templateController.defaultTemplate)
            {
                _currentTemplates[i] = template;
            }
        }
    }
}
