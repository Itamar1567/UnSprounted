using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
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
    public Item[] items;

    void Awake()
    {


        if (Singleton == null)
        {
            Singleton = this;
        }

        positionOnHotBar = 0;
        //giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(500); });
    }

    void Update()
    {
      
        if (carriedItem == null) return;

        carriedItem.transform.position = Mouse.current.position.ReadValue();
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        { EquipEquipment(item.activeSlot.myTag, null); }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
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
            // Check if the slot is empty
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().itemName = _item.itemName;
                break;
            }
        }
    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
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

    public void DeleteItem(GameObject objToDestroy)
    {

        Destroy(objToDestroy);

    }

    public bool IsInventoryFull()
    {
        itemsInInventoryCount = itemsCountInInventory();
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

    private int itemsCountInInventory()
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