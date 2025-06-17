using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotTag { None, Head, Chest, Legs, Feet }

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }

    public SlotTag myTag;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
        if(eventData.button == PointerEventData.InputButton.Right && Inventory.Singleton.IsItemInHand())
        {
            Inventory.Singleton.SpawnInventoryItemAtPosition(this, Inventory.carriedItem.myItem);
            Inventory.Singleton.GetItemInHand().AddRemoveCount(-1);
        }
        
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        // Reset old slot
        if(item.activeSlot != null)
        {
            item.activeSlot.myItem = null;
        }

        // Set current slot
        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
        { Inventory.Singleton.EquipEquipment(myTag, myItem); }
    }

    
}

