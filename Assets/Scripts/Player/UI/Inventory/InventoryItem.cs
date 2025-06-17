using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{


    public string itemName;
    private string toolType;
    public int itemID;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    [SerializeField] TMP_Text slotTextHolder;

    private int healAmount;
    private int damageAmount;
    private int itemMineLevel;
    //This count is a helper for when an item spawns and need to be equal to zero without being destroyed
    private int virtualCount = 0;
    private int _count = 1;
    //Any change that is made to *count* this will be called
    private int count
    {
        get => _count;
        set 
        {
            if(_count != value)
            {
                _count = value;
                IsItemCountBelowZero();

            }
        
        }
    }
    private float smeltTime;
    private float timeToBreak;
    private float attackWaitTime;
    private float attackRange;
    private bool block;
    private bool interactable;
    private bool energySource;
    private bool isWeapon;

    private Image itemIcon;



    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        Debug.Log("Adada");
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
        attackWaitTime = item.GetAttackWaitTime();
        block = item.block;
        interactable = item.interactable;
        energySource = item.energySource;
        attackRange = item.GetAttackRange();
        isWeapon = item.isWeapon;
        toolType = item.toolType;
        smeltTime = item.GetSmeltTime();

        SetText(count.ToString());
    }
    public InventoryItem InitializeOnHand(Item item)
    {
        activeSlot = null;
        myItem = item;
        itemName = item.name;
        itemIcon.sprite = item.sprite;
        itemID = item.GetItemID();
        healAmount = item.GetHealAmount();
        damageAmount = item.GetDamageAccount();
        itemMineLevel = item.GetMineLevel();
        attackWaitTime = item.GetAttackWaitTime();
        attackRange = item.GetAttackRange();
        block = item.block;
        interactable = item.interactable;
        energySource = item.energySource;
        smeltTime = item.GetSmeltTime();
        isWeapon = item.isWeapon;
        toolType = item.toolType;
        SetText(count.ToString());
        return this;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        //if there is not item in hand call this
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            switch (Inventory.Singleton.IsItemInHand())
            {
                //*this* means the InventoryItem object
                case true: Inventory.Singleton.LeftClickCheck(this); break;
                case false: Inventory.Singleton.SetCarriedItem(this); break;

            }            
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            switch(Inventory.Singleton.IsItemInHand())
            {
                case true: Inventory.Singleton.AddToItemCount(this, 1); break;
                case false: Inventory.Singleton.SpawnInventoryItemInHand(this); break;
            }
            
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

    public int GetDamageAmount()
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
    public int GetCount()
    {
        return count;
    }
    public int GetVirtualCount()
    {
        return virtualCount;
    }
    public float GetTimeBetweenAttacks()
    {
        return attackWaitTime;
    }
    public float GetSmeltTime()
    {
        return smeltTime;
    }
    public float GetAttackRange() 
    {
        return attackRange;
    }
    public string GetItemName()
    {
        return itemName;
    }
    public string GetToolType()
    {
        return toolType;
    }
    public bool isBlock()
    {
        return block;
    }
    public bool isInteractable()
    {
        return interactable;
    }
    //Returns true if item is an energy source and false if it is not
    public bool IsEnergySource()
    {
        return energySource;
    }

    //Returns true if item is a weapon/tool and false for all else
    public bool IsWeapon()
    {
        return isWeapon;
    }

    //increase or decrease count
    public void AddRemoveCount(int num)
    {
        count += num;
        SetText(count.ToString());
    }
    public void SetText(string text)
    {
        slotTextHolder.text = text;
    }
    //Destroyed the gameobject if item count is below zero
    public void IsItemCountBelowZero()
    {
        if (count <= 0) 
        {
            Destroy(gameObject);
        }
    }
    public void AddVirtualCount(int amount)
    {
        virtualCount += amount; 
    }
}