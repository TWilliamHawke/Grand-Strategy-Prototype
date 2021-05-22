using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelWithGrid<T> : MonoBehaviour
{
    [SerializeField] UIDataElement<T> _layoutElementPrefab;
    [SerializeField] LayoutGroup _layout;

    protected abstract List<T> _layoutElementsData { get; }
    protected LayoutGroup layout => _layout;

    void Awake()
    {
        UpdateGrid();
    }

    protected virtual void UpdateGrid()
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

    }




}
