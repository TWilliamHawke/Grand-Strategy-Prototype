using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitListController", menuName = "Core Game/Unit List Controller")]
public class UnitsListController : ScriptableObject
{
    [SerializeField] List<UnitTemplate> _defaultUnits;
    [SerializeField] List<UnitTemplate> _mecrcenaryUnits;
    [SerializeField] TemplateController _templateController;

    List<UnitTemplate> _currentTemplates = new List<UnitTemplate>();

    //getters
    public List<UnitTemplate> mecrcenaryUnits => _mecrcenaryUnits;
    public List<UnitTemplate> currentTemplates => _currentTemplates;

    void Awake()
    {
        _templateController.OnTemplateSave += SaveTemplate;
    }

    void OnEnable()
    {
        _currentTemplates.Clear();
        _currentTemplates.AddRange(_defaultUnits);
    }

    void SaveTemplate(UnitTemplate savedTemplate)
    {
        var template = Instantiate(savedTemplate);

        if (template.canNotEdit)
        {
            AddTemplateAsNew(template);
        }
        else
        {
            ReplaceTemplate(template);
        }
    }

    void AddTemplateAsNew(UnitTemplate template)
    {
        template.canNotEdit = false;
        _currentTemplates.Add(template);
    }

    void ReplaceTemplate(UnitTemplate template)
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
