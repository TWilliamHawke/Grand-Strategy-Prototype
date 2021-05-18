using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyList : UIPanelWithGrid<Technology>
{
    [SerializeField] TechnologiesController _controller;

    private void Awake() {
        FillGridElementsList();
    }

    protected override void FillGridElementsList()
    {
        _gridElementsData.AddRange(_controller.allTechnologies);
    }

    protected override void PlusButtonListener()
    {
    }

}
