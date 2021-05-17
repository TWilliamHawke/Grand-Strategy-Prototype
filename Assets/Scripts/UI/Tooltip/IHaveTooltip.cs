using UnityEngine.EventSystems;

public interface IHaveTooltip: IPointerEnterHandler, IPointerExitHandler
{
    string GetTooltipText();
}
