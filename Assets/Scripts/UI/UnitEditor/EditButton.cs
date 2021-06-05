using UnityEngine.EventSystems;

public class EditButton : UIElementWithTooltip, IPointerClickHandler
{
    public override string GetTooltipText()
    {
        return "Edit Unit Template";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideTooltip();
    }
}
