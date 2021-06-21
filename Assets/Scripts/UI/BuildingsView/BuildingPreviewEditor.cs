using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UnitEditor
{

    public class BuildingPreviewEditor : UIDataElement<Building>, IPointerClickHandler
    {
        Building _buildingData;
        [SerializeField] Image _buildingIcon;
        [SerializeField] TemplateController _templateController;

        public override string GetTooltipText()
        {
            string helpText = "\n\nRight click to remove building";
            return _buildingData.GetFullDescription() + helpText;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                HideTooltip();
                _templateController.RemoveBuilding(_buildingData);
            }
        }

        public override void UpdateData(Building data)
        {
            _buildingData = data;
            _buildingIcon.sprite = data.icon;
        }


    }
}
