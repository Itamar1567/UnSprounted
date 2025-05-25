using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{


    private class Recipes
    {
        public int itemId;
        public string itemName;
        public string ingredients;
    }


    public static Crafting Singleton;
    [SerializeField] private InventorySlot resaultingSlotInvTbl;
    [SerializeField] private InventorySlot resaultingSlotCrftTbl;
    [SerializeField] private InventoryItem itemPrefab;
    private string currentItemsInSlots;
    private GameObject craftedItemReference;
    private GameObject[] slotsReference;
    private InventorySlot resualtSlotUsed;
    private List<Recipes> RecipeList = new List<Recipes>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
    void Start()
    {
        RecipeList.Add(new Recipes { itemId = 2, itemName = "Crafting Table", ingredients = "loglogloglog" });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnItemInResaultingSlot(int craftedItemId)
    {
        Item item;
        item = Inventory.Singleton.items[craftedItemId];
        Instantiate(itemPrefab, resualtSlotUsed.transform).Initialize(item, resualtSlotUsed);
    }

    public bool CraftItem(string craftingSlotsRslt)
    {

        for (int i = 0; i < RecipeList.Count; i++)
        {
            if (craftingSlotsRslt == RecipeList[i].ingredients)
            {
                Debug.Log("Crafted");
                //This will destroy the item in the resault slot, if item already existed in the resault slot but player changed the recipe and another item is to be placed in the resault slot
                DestroyItemInResaultSlot();
                SpawnItemInResaultingSlot(RecipeList[i].itemId);
                AddButtonComponent();
                return true;
            }
        }
        //Destroy the spawned craft item if the player changes the items in the slots

        DestroyItemInResaultSlot();
        return false;
    }

    //This function checks all slots and adds the names of slots that have items in them
    public async void InspectCraftingSlots(GameObject[] slots, int craftingSystem)
    {
        //using await so Unity registers that an item is in a slot, so as to not skip a newly added item
        await Task.Delay(1);
        string itemInSlots = "";
        foreach (GameObject item in slots)
        {

            if (item.transform.childCount > 0)
            {
                //Getting child 0 due to slots hosting items as their first child
                itemInSlots += item.transform.GetChild(0).GetComponent<InventoryItem>().itemName;
            }
            else
            {
                //itemInSlots += "null";
            }

        }

        switch (craftingSystem)
        {
            //Checks what crafting system the player is using in order to decide which resaulting slot to place the crafted item
            case 4: resualtSlotUsed = resaultingSlotInvTbl; break;
            case 9: resualtSlotUsed = resaultingSlotCrftTbl; break;
        }

        itemInSlots = itemInSlots.ToLower();
        Debug.Log(itemInSlots);
        slotsReference = slots;
        currentItemsInSlots = itemInSlots;
        if (CraftItem(itemInSlots))
        {
            //DestroyItemsInSlots(slots); 
        }
    }

    public void DestroyItemsInSlots()
    {
        foreach (GameObject slot in slotsReference)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }
    }
    //This function adds a button to the crafted item so as to confirm the player accually wants that item crafted
    private void AddButtonComponent()
    {
        //Child 0 due to it being the item in the slot
        craftedItemReference = resualtSlotUsed.transform.GetChild(0).gameObject;
        craftedItemReference.AddComponent<Button>();
        craftedItemReference.GetComponent<Button>().onClick.AddListener(ConfirmCraftedItem);
    }
    //This function removes the button once the player clicks the crafted item("Confirms that they want it)
    private void RemoveButtonComponent()
    {
        if (craftedItemReference.GetComponent<Button>())
        {
            Destroy(craftedItemReference.GetComponent<Button>());
        }
    }
    private void ConfirmCraftedItem()
    {
        DestroyItemsInSlots();
        RemoveButtonComponent();
        //Extra protection as to not keep the reference "dangaling"
        craftedItemReference = null;
    }

    private void DestroyItemInResaultSlot()
    {
        if (resualtSlotUsed.transform.childCount > 0)
        {
            Destroy(resualtSlotUsed.transform.GetChild(0).gameObject);
        }
    }

    //This function checks if the item in slot has been taken out or placed in 
    private void haveSlotsChanged()
    {
    }

}
