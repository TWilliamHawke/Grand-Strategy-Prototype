using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
public class UnitName : MonoBehaviour
{
    [SerializeField] Text _unitName;
    [SerializeField] TemplateController _templateController;

    private void OnEnable() {
        _templateController.OnTemplateChange += UpdateUnitName;
    }

    private void OnDisable() {
        _templateController.OnTemplateChange -= UpdateUnitName;
    }

    void UpdateUnitName(UnitTemplate template)
    {
        _unitName.text = template.templateName;
    }
}

}