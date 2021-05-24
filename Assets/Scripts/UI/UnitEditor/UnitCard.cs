using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{

    [RequireComponent(typeof(Button))]
    public class UnitCard : UIDataElement<UnitTemplate>, IPointerClickHandler
    {
        [SerializeField] TemplateController _templateController;
        [Header("UI Elements")]
        [SerializeField] Image _imageForIcon;
        [SerializeField] Text _templateName;

        UnitTemplate _unitTemplate;
        UnitEditorScreen _unitEditor;

        public override void UpdateData(UnitTemplate template)
        {
            _unitTemplate = template;
            _imageForIcon.sprite = _unitTemplate.unitClass.defaultIcon;
            _templateName.text = _unitTemplate.templateName;
        }

        public override string GetTooltipText()
        {
            return _unitTemplate.templateName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _templateController.SelectTemplate(_unitTemplate);
        }
    }


}
