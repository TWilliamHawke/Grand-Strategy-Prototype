using System.Collections.Generic;

namespace UnitEditor
{
    public class SecondaryWeaponSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.weapon;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (item.ConvertTo<Weapon>(out var weapon))
            {
                templateController.currentTemplate.secondaryWeapon = weapon;
            }
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.secondaryWeapon;
        }
    }
}