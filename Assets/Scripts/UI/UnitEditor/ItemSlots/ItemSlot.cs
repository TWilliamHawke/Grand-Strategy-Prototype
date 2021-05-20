using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Text;

namespace UnitEditor
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] EquipmentList _equipmentList;
    [SerializeField] ItemSlotController _itemSlotController;
    [SerializeField] Text _itemName;
    [SerializeField] Image _itemIcon;
    [SerializeField] Equipment _defaultItem;

    public UnityEvent<Equipment> itemCallback;
    Equipment _itemInSlot;

    void Awake()
    {
        itemCallback.AddListener(UpdateItemInSlot);
        _itemInSlot = _defaultItem;
    }

    private void OnDisable()
    {
        itemCallback.RemoveListener(UpdateItemInSlot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if((int)eventData.button == 0) {
            //_itemSlotController.SelectItemSlot(itemCallback, _equipmentList.equipmentList);
        }
        if((int)eventData.button == 1) {
            itemCallback.Invoke(_defaultItem);
        }
    }

    public void UpdateItemInSlot(Equipment item)
    {
        _itemInSlot = item;
        _itemName.text = item.Name;
        _itemIcon.sprite = item.sprite;
    }

}

}