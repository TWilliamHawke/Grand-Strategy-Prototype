using System.Collections.Generic;

namespace UnitEditor
{
    public class ArmorSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.armor;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (item.ConvertTo<ArmourInfo>(out var armor))
            {
                templateController.currentTemplate.armour = armor;
            }
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.armour;
        }
    }
}