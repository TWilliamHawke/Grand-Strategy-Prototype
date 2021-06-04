using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour
{
    IhaveLabel _parent;
    [SerializeField] Text _labelText;
    [SerializeField] Image _background;
    [SerializeField] float _positionOffset = 2f;
    float _defaultTransparency;

    void OnEnable()
    {
        SelectionController.OnSelect += TryMakeDimmer;
        Settlement.OnCapture += ChangeColorAfterConquest;
        _defaultTransparency = _background.color.a;
    }

    void OnDisable()
    {
        SelectionController.OnSelect -= TryMakeDimmer;
        Settlement.OnCapture -= ChangeColorAfterConquest;

    }

    void LateUpdate()
    {
        if (_parent == null) return;
        Vector3 labelPosition = _parent.transform.position + Vector3.down * _positionOffset;
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, labelPosition);
    }

    void ChangeColorAfterConquest(Settlement settlement)
    {
        if (_parent != (IhaveLabel)settlement) return;

        var color = settlement.baseLabelColor;
        color.a = _background.color.a;
        _background.color = color;
    }

    public void AddParent(IhaveLabel parent)
    {
        _parent = parent;
        var color = parent.baseLabelColor;
        color.a = _defaultTransparency;
        _background.color = color;

        _labelText.text = _parent.GetName();
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
        if (target != _parent)
        {
            MakeDimmer();
        }
        else
        {
            MakeBrighter();
        }
    }
}
