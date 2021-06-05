using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyPanel : UIPanelWithGrid<Technology>
{
    [SerializeField] TechnologiesController _controller;

    protected override List<Technology> _layoutElementsData => _technologyList;
    List<Technology> _technologyList = new List<Technology>();

    private void Awake()
    {
        _technologyList.AddRange(_controller.allTechnologies);
        UpdateLayout();
    }

}
