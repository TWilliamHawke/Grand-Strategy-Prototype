using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class SecondaryWeaponSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.weapon;

        protected override void AddItemToTemplate(Equipment item)
        {
            templateController.AddSecondaryWeapon(item);
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.secondaryWeapon;
        }
    }
}