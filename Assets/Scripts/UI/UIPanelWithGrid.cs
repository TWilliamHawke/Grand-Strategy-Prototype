using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGrid<T> : MonoBehaviour
{
    [SerializeField] UIDataElement<T> _gridElementPrefab;
    [SerializeField] Button _plusButtonPrefab;
    [SerializeField] GridLayoutGroup _grid;

    protected List<T> _gridElementsData = new List<T>();

    abstract protected void FillGridElementsList();
    abstract protected void PlusButtonListener();

    protected void UpdateGrid()
    {
        foreach (Transform children in _grid.transform)
        {
            Destroy(children.gameObject);
        }

        foreach (var template in _gridElementsData)
        {
            var unitCard = Instantiate(_gridElementPrefab);
            unitCard.transform.SetParent(_grid.transform);
            unitCard.UpdateData(template);
        }

        if(_plusButtonPrefab == null) return;

        var plusButton = Instantiate(_plusButtonPrefab);
        plusButton.transform.SetParent(_grid.transform);
        plusButton.onClick.AddListener(PlusButtonListener);
    }




}
