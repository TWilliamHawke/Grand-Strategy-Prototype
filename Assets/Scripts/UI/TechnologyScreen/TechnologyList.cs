using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyList : UIPanelWithGrid<Technology>
{
    [SerializeField] TechnologiesController _controller;

    private void Awake() {
        FillLayoutElementsList();
    }

    protected override void FillLayoutElementsList()
    {
        _layoutElementsData.AddRange(_controller.allTechnologies);
    }

    protected override void PlusButtonListener()
    {
    }

}
