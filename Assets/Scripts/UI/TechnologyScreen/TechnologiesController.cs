using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TechController", menuName = "Core Game/Tech Controller")]
public class TechnologiesController : ScriptableObject
{
    [SerializeField] List<Technology> _allTechnologies = new List<Technology>();
    public List<Technology> allTechnologies => _allTechnologies;


    TechnologyButton _selectedTechnologyButton;
    List<Technology> _researchedTechnologies = new List<Technology>();
    public List<Technology> researchedTechnologies => _researchedTechnologies;

    private void OnEnable() {
        _researchedTechnologies.Clear();
    }

    public void Select(TechnologyButton button)
    {
        _selectedTechnologyButton = button;
    }

    public void ResearchSelected()
    {
        if(_selectedTechnologyButton == null) return;

        _researchedTechnologies.Add(_selectedTechnologyButton.technologyData);
        _selectedTechnologyButton.MarkAsResearched();
        _selectedTechnologyButton = null;
    }

    public List<Effect> GetAllEffects()
    {
        return _researchedTechnologies.SelectMany(t => t.effects).ToList();
    }
}
