using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour
{
    IhaveLabel _parent;
    [SerializeField] Text _labelText;
    [SerializeField] Image _background;
    [SerializeField] float _positionOffset = 2f;
    float _defaultTransparency;

    public void AddParent(IhaveLabel parent)
    {
        _parent = parent;
        _labelText.text = _parent.GetName();
    }

    private void OnEnable() {
        SelectionController.OnSelect += TryMakeDimmer;
        _defaultTransparency = _background.color.a;
    }

    private void OnDisable() {
        SelectionController.OnSelect -= TryMakeDimmer;
    }

    void LateUpdate()
    {
        if(_parent == null) return;
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _parent.transform.position + Vector3.down * _positionOffset);
    }

    public void MakeBrighter()
    {
        var color = _background.color;
        color.a = 1;
        _background.color = color;
    }

    public void MakeDimmer()
    {
        var color = _background.color;
        color.a = _defaultTransparency;
        _background.color = color;
    }

    void TryMakeDimmer(ISelectable target)
    {
        if(target != _parent)
        {
            MakeDimmer();
        }
        else
        {
            MakeBrighter();
        }
    }
}
