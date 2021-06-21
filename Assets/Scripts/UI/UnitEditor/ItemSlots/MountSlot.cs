using System.Collections.Generic;

namespace UnitEditor
{
    public class MountSlot : AbstractItemSlot
    {
        public override List<Equipment> itemsForSlot => itemSlotController.mounts;


        protected override void AddItemToTemplate(Equipment item)
        {
            if (item.ConvertTo<Mount>(out var mount))
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