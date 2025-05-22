using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
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
    private bool isCraftingtable = false;
    private GameObject[] slotsReference;
    private List<Recipes> RecipeList = new List<Recipes>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
    }
    void Start()
    {
        RecipeList.Add(new Recipes{ itemId = 2, itemName = "Crafting Table", ingredients = "loglogloglog" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItemInResaultingSlot(int craftedItemId)
    {
        Item item;
        item = Inventory.Singleton.items[craftedItemId];
        if(isCraftingtable == false)
        {
            Instantiate(itemPrefab, resaultingSlotInvTbl.transform).Initialize(item, resaultingSlotInvTbl);
        }
        if (isCraftingtable == true)
        {
            Instantiate(itemPrefab, resaultingSlotCrftTbl.transform).Initialize(item, resaultingSlotCrftTbl);
        }
    }

    public bool CraftItem(string craftingSlotsRslt)
    {
        for(int i = 0; i < RecipeList.Count; i++)
        {
            if(craftingSlotsRslt == RecipeList[i].ingredients)
            {
                Debug.Log("Crafted");
                SpawnItemInResaultingSlot(RecipeList[i].itemId);
                return true;
            }
        }

        return false;
    }

    //This function checks all slots and adds the names of slots that have items in them
    public async void InspectCraftingSlots(GameObject[] slots, int craftingSystem)
    {
        //using await so Unity registers that an item is in a slot, so as to not skip a newly added item
        await Task.Delay(1);
        string itemInSlots = "";
        foreach(GameObject item in slots) 
        {

            if (item.transform.childCount > 0)
            {
                //Getting child 0 due to slots hosting items as their first child
                itemInSlots += item.transform.GetChild(0).GetComponent<InventoryItem>().itemName;
            }
        
        }
        
        switch(craftingSystem)
        {
            //Checks what crafting system the player is using in order to decide which resaulting slot to place the crafted item
            case 4: isCraftingtable = false; break;
                case 9: isCraftingtable = true; break;
        }

        itemInSlots = itemInSlots.ToLower();
        Debug.Log(itemInSlots);
        slotsReference = slots;

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

}
