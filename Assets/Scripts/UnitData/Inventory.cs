using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
	[System.Serializable]
	public class Inventory
	{
		[SerializeField]
		List<EquipmentPair> _equipmentList = new List<EquipmentPair>();

		public Equipment this[EquipmentSlots slot]
		{
			set
			{
				if(slot == EquipmentSlots.none)
				{
					Debug.LogError("Item has been added into none slot!!!");
				}

				for (int i = 0; i < _equipmentList.Count; i++)
				{
					if(_equipmentList[i].equpmentSlot == slot)
					{
						_equipmentList[i] = new EquipmentPair(slot, value);
						return;
					}
				}
				//if slot not found
				_equipmentList.Add(new EquipmentPair(slot, value));
			}

			get
			{
				foreach(var pair in _equipmentList)
				{
					if(pair.equpmentSlot == slot)
					{
						return pair.equipment;
					}
				}
				return null;
			}
		}
	    
		[System.Serializable]
		struct EquipmentPair
		{
			public EquipmentSlots equpmentSlot;
			public Equipment equipment;

            public EquipmentPair(EquipmentSlots equpmentSlot, Equipment equipment)
            {
                this.equpmentSlot = equpmentSlot;
                this.equipment = equipment;
            }
        }
	}
}