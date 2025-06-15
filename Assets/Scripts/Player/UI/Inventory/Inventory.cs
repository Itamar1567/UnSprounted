using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
    public Item[] items;
    [SerializeField] InventoryItem mockItemForNullCases;

    [SerializeField] InventorySlot inventoryCraftingResaultSlot;
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] GameObject[] craftingSlots;

    // 0=Head, 1=Chest, 2=Legs, 3=Feet
    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    private int positionOnHotBar;
    private int itemsInInventoryCount = 0;
    //A dictinary/unordered_map that eqautes 1 item id to another's, Ex: ironID = 1, IronIngot = 2 if I look up [1] i will get 2
    private Dictionary<int, int> itemSmeltedEquivelent = new Dictionary<int, int>();

    void Awake()
    {


        if (Singleton == null)
        {
            Singleton = this;
        }

        positionOnHotBar = 0;
        //giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(500); });
        
        itemSmeltedEquivelent[5] = 6;
    }

    void Update()
    {

        if (carriedItem == null) return;

        carriedItem.transform.position = Mouse.current.position.ReadValue();
    }

    //Called when an item is in the player's hand
    public void SetCarriedItem(InventoryItem item)
    {
        if(item.activeSlot != null)
        {
            if (carriedItem != null)
            {
                if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
                item.activeSlot.SetItem(carriedItem);
            }

            if (item.activeSlot.myTag != SlotTag.None)
            { EquipEquipment(item.activeSlot.myTag, null); }

            //Reset the old slot
            item.activeSlot.myItem = null;
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        
        item.transform.SetParent(draggablesTransform);
        //Debug.Log("Entered");
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                if (item == null)
                {
                    // Destroy item.equipmentPrefab on the Player Object;
                    Debug.Log("Unequipped helmet on " + tag);
                }
                else
                {
                    // Instantiate item.equipmentPrefab on the Player Object;
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                }
                break;
            case SlotTag.Chest:
                break;
            case SlotTag.Legs:
                break;
            case SlotTag.Feet:
                break;
        }
    }

    public void SpawnInventoryItem(int itemID = 0)
    {

        Item _item = items[itemID];
        if (_item == null)
        { _item = PickRandomItem(); }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Debug.Log(inventorySlots[i].myItem);
            Debug.Log(_item);

            // Check if the slot is empty
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().itemName = _item.itemName;
                break;
            }
            if (inventorySlots[i].myItem.itemID == _item.itemID)
            {
                inventorySlots[i].myItem.AddRemoveCount(1);
                break;
            }
        }
    }
    //Spawns an inventory item at a requested and valid slot position
    public void SpawnInventoryItemAtPosition(InventorySlot slot, Item item)
    {
        Item _item = item;
        if(slot.myItem == null)
        {
          Instantiate(itemPrefab, slot.transform).Initialize(_item, slot);
        }

    }
    //Spawns an Item in the player's hand/draggable component
    public void SpawnInventoryItemInHand(InventoryItem item)
    {
        //using inventorySlots[0] because using draggable changes the scaling and the item must be in the inventorySlot scale(Potential issues with this piece of code though): This may need to change
        InventoryItem _item = Instantiate(itemPrefab, inventorySlots[0].transform).InitializeOnHand(item.myItem);
        SetCarriedItem(_item);
        //Divide the old item count by 2 and add that to the new item
        Debug.Log(item.GetCount()/2);
        carriedItem.AddRemoveCount(item.GetCount()/2 - 1); //-1 do to the newly instatiated item starting with a count of 1
        item.AddRemoveCount(-carriedItem.GetCount());
    }
    //Increments the item in the slot's count and decrements the carried item's count
    public void AddToItemCount(InventoryItem item, int amount)
    {
        item.AddRemoveCount(amount);
        carriedItem.AddRemoveCount(-amount);
    }
    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }

    //This moves an exact amount of items from one stack to another one, a - amount, b + amount
    public void ExchangeItemCount(InventorySlot a, InventorySlot b, int amount)
    {
        a.myItem.AddRemoveCount(-amount);
        b.myItem.AddRemoveCount(amount);
    }

    public Item GetSmeltedItemEquivalence(InventoryItem item)
    {
        int initialItemId = item.myItem.GetItemID();
        //Checks if the given id has a corresponding smelted item
        Debug.Log("Given item ID: " + item.myItem.GetItemID());
        Debug.Log("Item equivalnce: " + itemSmeltedEquivelent[initialItemId]);
        if(itemSmeltedEquivelent.TryGetValue(initialItemId, out int smeltedItemId))
        {
            Debug.Log(smeltedItemId);
            if(smeltedItemId >= 0 && smeltedItemId < items.Length)
            {
                Debug.Log("Entered...");
                return items[smeltedItemId];
            }
            else
            {
                Debug.Log(smeltedItemId + " Item was out of range, " + "array max = " + items.Length);
                return null;
            }
        }
        Debug.Log("Item does not smelt ");
        return null;



    }
    public void CycleThroughHotBar()
    {

        int childCount = hotbarSlots.Length;
        for (int i = 0; i < childCount; i++)
        {
            if (i == positionOnHotBar)
            {
                hotbarSlots[i].gameObject.GetComponent<Image>().color = Color.black;
                continue;
            }
            hotbarSlots[i].gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    //Called when the player left clicks whilst holding an item and checks wether to swap the items or if they are the same add them together
    public void LeftClickCheck(InventoryItem item)
    {
        if(carriedItem.itemID == item.itemID)
        {
            item.AddRemoveCount(carriedItem.GetCount());
            carriedItem.AddRemoveCount(-carriedItem.GetCount());
        }
        else
        {
            SetCarriedItem(item);
        }
    }

    public void DeleteItem(GameObject objToDestroy)
    {

        Destroy(objToDestroy);

    }

    public bool IsInventoryFull()
    {
        itemsInInventoryCount = ItemsCountInInventory();
        Debug.Log(itemsInInventoryCount);
        if (inventorySlots.Length <= itemsInInventoryCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Returns true or false based by checking if an item is being carried by the player
    public bool IsItemInHand()
    {
        if (carriedItem != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Returns the item that the player is currently holding
    public InventoryItem GetItemInHand()
    {
        return carriedItem;
    }
    private int ItemsCountInInventory()
    {
        int count = 0;

        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount > 0)
            {
                count++;
            }
        }

        return count;
    }


    private void OnDestroy()
    {
        Singleton = null;
    }
}