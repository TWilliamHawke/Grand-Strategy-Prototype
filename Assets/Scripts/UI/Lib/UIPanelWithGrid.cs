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
    protected UIDataElement<T> prefab => _layoutElementPrefab;

    void Awake()
    {
        UpdateLayout();
    }

    protected virtual void UpdateLayout()
    {
        ClearLayout();

        foreach (var template in _layoutElementsData)
        {
            var uiElement = Instantiate(_layoutElementPrefab);
            uiElement.transform.SetParent(_layout.transform);
            uiElement.UpdateData(template);
        }

    }

    protected void ClearLayout()
    {
        foreach (Transform children in _layout.transform)
        {
            Destroy(children.gameObject);
        }

    }



}
