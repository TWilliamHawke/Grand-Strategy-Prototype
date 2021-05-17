using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;


namespace UnitEditor
{

public class ItemList : MonoBehaviour
{
    [SerializeField] ItemSlotController _itemSlotController;
    [SerializeField] LayoutGroup _content;
    [SerializeField] Item _itemPrefab;

    private void Awake() {
        _itemSlotController.OnItemSlotSelection += UpdateItemList;
    }

    private void OnDisable() {
        _itemSlotController.OnItemSlotSelection -= UpdateItemList;
    }

    void UpdateItemList(UnityEvent<Equipment> callback, List<Equipment> equipmentList)
    {
        //TODO replace with pooling system
        foreach(Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }

        //cache
        int freeGold = Mathf.Max(_itemSlotController.freeGold, 0);
        
        foreach(var item in equipmentList)
        {
            var itemRow = Instantiate(_itemPrefab);
            itemRow.transform.SetParent(_content.transform);
            itemRow.AddCallback(callback);
            itemRow.SetItemData(item);
            
            if (freeGold < item.goldCost)
            {
                itemRow.SetInactiveDueGold();
            }

            int itemSkill = (item as RequireSkillEquipment)?.requiredSkill ?? 0;

            if(itemSkill > _itemSlotController.classSkill )
            {
                itemRow.SetInactiveDueSkill();
            }
        }
    }

}
}