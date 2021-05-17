using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
public class UnitPreview : MonoBehaviour
{
    [SerializeField] TemplateController templateController;
    [SerializeField] Image _previewImage;

    private void Awake() {
        templateController.OnTemplateChange += UpdateUnitPreview;
    }

    public void UpdateUnitPreview(UnitTemplate template)
    {
        _previewImage.sprite = template.unitClass.unitPreview;
    }

    
}

}