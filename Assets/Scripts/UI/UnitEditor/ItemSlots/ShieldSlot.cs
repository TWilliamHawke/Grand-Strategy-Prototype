using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class ShieldSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.shields;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (itemSlotController.ConvertEquipment<Shield>(item, out var shield))
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