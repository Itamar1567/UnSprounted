using UnityEngine;

//public enum SlotTag { None, Head, Chest, Legs, Feet };
[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{

    public Sprite sprite;
    public SlotTag itemTag;
    public string itemName;
    public int itemID;
    public bool block;
    public bool interactable;
    

    [Header("If the Item can be equipped")]
    public GameObject equipmentPrefab;

    [Header("If the item is a weapon/tool")]
    public bool isWeapon = false;
    [SerializeField] private int damageAmount = 0;
    [SerializeField] private float attackWaitTime = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int itemMineLevel = 0;

    [Header("If the item is a tool")]
    public bool isTool = false;
    public string toolType = string.Empty;


    [Header("If the item is an energy source")]
    public bool energySource;
    [SerializeField] private float smeltTime = 0;

    [Header("If the item is interactable")]
    [SerializeField] private int healAmount = 0;

    [Header("If the item imits light")]
    [SerializeField] float illuminance = 0;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemID(int retrievedItemID)
    {
        itemID = retrievedItemID;
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

    public float GetAttackWaitTime()
    {
        return attackWaitTime;
    }
    public float GetAttackRange()
    {
        return attackRange;
    }
    public float GetSmeltTime()
    {
        return smeltTime;
    }
    public float GetIlluminance()
    {
        return illuminance;
    }


}
