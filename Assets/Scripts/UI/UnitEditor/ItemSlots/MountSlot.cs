using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class MountSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.mounts;


        public override void AddItemToTemplate(Equipment item)
        {
            templateController.AddMount(item);
        }

        protected override Equipment SelectItemFromTemplate(UnitTemplate template)
        {
            return template.mount;
        }


    }
}