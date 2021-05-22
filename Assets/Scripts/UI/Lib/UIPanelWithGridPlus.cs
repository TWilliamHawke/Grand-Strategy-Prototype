using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGridPlus<T> : UIPanelWithGrid<T>
{

    [SerializeField] Button _plusButtonPrefab;

    abstract protected void PlusButtonListener();
    abstract protected bool ShouldHidePlusButton();


    protected override void UpdateGrid()
    {
        base.UpdateGrid();

        if(ShouldHidePlusButton()) return;

        var plusButton = Instantiate(_plusButtonPrefab);
        plusButton.transform.SetParent(layout.transform);
        plusButton.onClick.AddListener(PlusButtonListener);

    }

}
