using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class PrimaryWeaponSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.weapon;

        protected override void AddItemToTemplate(Equipment item)
        {
            if (itemSlotController.ConvertEquipment<Weapon>(item, out var weapon))
            {
                templateController.currentTemplate.primaryWeapon = weapon;
            }
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.primaryWeapon;
        }
    }
}