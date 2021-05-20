using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{

    [RequireComponent(typeof(Button))]
    public class UnitCard : UIDataElement<UnitTemplate>
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] UnitTemplate _unitTemplate;
        [SerializeField] UIScreensManager _screenManager;
        [Header("UI Elements")]
        [SerializeField] Image _imageForIcon;
        [SerializeField] Text _templateName;
        [SerializeField] bool isDefaultType;

        UnitEditorScreen _unitEditor;

        Button _button;

        private void Awake()
        {
            _unitEditor = FindObjectOfType<UnitEditorScreen>(true);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenEditorWithTemplate);
            UpdateUnitInfo();
        }

        public void OpenEditorWithTemplate()
        {
            _templateController.SelectTemplate(_unitTemplate);
            if (isDefaultType)
            {
                HideTooltip();
                _screenManager.OpenScreen(_unitEditor);
            }
        }

        public override void UpdateData(UnitTemplate template)
        {
            _unitTemplate = template;
            UpdateUnitInfo();
        }

        private void UpdateUnitInfo()
        {
            _imageForIcon.sprite = _unitTemplate.unitClass.defaultIcon;
            _templateName.text = isDefaultType ? _unitTemplate.unitClass.className : _unitTemplate.templateName;
        }

        public override string GetTooltipText()
        {
            return _unitTemplate.templateName;
        }
    }


}
