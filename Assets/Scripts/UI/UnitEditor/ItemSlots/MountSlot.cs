using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class MountSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.mounts;


        protected override void AddItemToTemplate(Equipment item)
        {
            if (itemSlotController.ConvertEquipment<Mount>(item, out var mount))
            {
                templateController.currentTemplate.mount = mount;
            }
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.mount;
        }


    }
}