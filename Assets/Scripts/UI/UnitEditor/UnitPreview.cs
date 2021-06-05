using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitPreview : MonoBehaviour, INeedInit
    {
        [SerializeField] TemplateController templateController;
        [SerializeField] Image _previewImage;

        public void Init()
        {
            templateController.OnTemplateChange += UpdateUnitPreview;
        }

        void OnDestroy()
        {
            templateController.OnTemplateChange -= UpdateUnitPreview;
        }

        public void UpdateUnitPreview(UnitTemplate template)
        {
            _previewImage.sprite = template.unitClass.unitPreview;
        }

    }

}