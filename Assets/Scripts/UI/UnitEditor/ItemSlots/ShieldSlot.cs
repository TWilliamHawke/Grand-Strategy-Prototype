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
            templateController.AddShield(item);
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.shield;
        }
    }
}