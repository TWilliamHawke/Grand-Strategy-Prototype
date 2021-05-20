using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class ArmorSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.armor;

        public override void AddItemToTemplate(Equipment item)
        {
            templateController.AddArmor(item);
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.armour;
        }
    }
}