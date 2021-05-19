using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGrid<T> : MonoBehaviour
{
    [SerializeField] UIDataElement<T> _layoutElementPrefab;
    [SerializeField] Button _plusButtonPrefab;
    [SerializeField] LayoutGroup _layout;

    protected List<T> _layoutElementsData = new List<T>();

    abstract protected void FillLayoutElementsList();
    abstract protected void PlusButtonListener();

    void Awake()
    {
        UpdateGrid();
    }

    protected void UpdateGrid()
    {
        foreach (Transform children in _layout.transform)
        {
            Destroy(children.gameObject);
        }

        foreach (var template in _layoutElementsData)
        {
            var unitCard = Instantiate(_layoutElementPrefab);
            unitCard.transform.SetParent(_layout.transform);
            unitCard.UpdateData(template);
        }

        if (_plusButtonPrefab == null) return;

        var plusButton = Instantiate(_plusButtonPrefab);
        plusButton.transform.SetParent(_layout.transform);
        plusButton.onClick.AddListener(PlusButtonListener);
    }




}
