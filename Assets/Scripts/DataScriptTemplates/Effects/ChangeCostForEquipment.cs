using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "DecreaseItemsCost", menuName = "Effects/Decrease Items Cost")]
    public class ChangeCostForEquipment : Effect, IEffectChangeEquipment
    {
        [SerializeField] EquipmentList _equipmentList;
        [SerializeField] int _costDecresePct;

        public int costDecresePct => _costDecresePct;

        public override string GetText()
        {
            return $"Decrease cost for {_equipmentList.listName.ToLower()} by {_costDecresePct}%";
        }

        public bool AffectsItem(Equipment item)
        {
            return _equipmentList.equipmentList.Contains(item);
        }

    }
}