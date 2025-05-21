using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;

    public string itemName;
    public int itemID;

    int healAmount;
    int damageAmount;
    int itemMineLevel;
    float timeToBreak;
    float timeBetweenAttack;
    bool block;
    bool interactable;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        if(itemIcon == null)
        {
            itemIcon = GetComponent<Image>();
        }
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemName = item.name;
        itemIcon.sprite = item.sprite;
        itemID = item.GetItemID();
        healAmount = item.GetHealAmount();
        damageAmount = item.GetDamageAccount();
        itemMineLevel = item.GetMineLevel();
        block = item.block;
        interactable = item.interactable;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }

  
    public int GetItemID()
    {
        return itemID;
    }

    public int GetHealAmount()
    {
        return healAmount;
    }

    public int GetDamageAccount()
    {
        return damageAmount;
    }

    public int GetMineLevel()
    {
        return itemMineLevel;
    }

    public float GetTimeToBreak()
    {
        return timeToBreak;
    }

    public float GetTimeBetweenAttacks()
    {
        return timeBetweenAttack;
    }
    public string GetItemName()
    {
        return itemName;
    }
    public bool isBlock()
    {
        return block;
    }
    public bool isInteractable()
    {
        return interactable;
    }




}