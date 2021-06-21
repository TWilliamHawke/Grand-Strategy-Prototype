using System.Collections.Generic;

namespace UnitEditor
{
    public class ShieldSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.shields;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (item.ConvertTo<Shield>(out var shield))
            {
                templateController.currentTemplate.shield = shield;
            }
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.shield;
        }
    }
}