using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class PrimaryWeaponSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.weapon;

        public override void AddItemToTemplate(Equipment item)
        {
            templateController.AddPrimaryWeapon(item);
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.primaryWeapon;
        }
    }
}