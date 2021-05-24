using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class ArmorSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.armor;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (itemSlotController.ConvertEquipment<ArmourInfo>(item, out var armor))
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