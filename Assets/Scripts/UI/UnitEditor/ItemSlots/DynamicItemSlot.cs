using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

namespace UnitEditor
{
	public class DynamicItemSlot : AbstractItemSlot
	{
		[SerializeField] Text _slotName;

	    protected override void UpdateSlotUI(UnitTemplate template)
		{
			
			if (template.unitClass.lastItemSlot != EquipmentSlots.none)
			{
				gameObject.SetActive(true);
				var slotName = template.unitClass.lastItemSlot.ToString();
				_slotName.text = slotName.InsertSpaces();
				base.UpdateSlotUI(template);
			}
			else
			{
				gameObject.SetActive(false);
				_itemInSlot = null;
			}
		}
	}
}