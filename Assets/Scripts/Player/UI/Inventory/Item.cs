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
    public bool energySource;

    [Header("If the Item can be equipped")]
    public GameObject equipmentPrefab;
    [SerializeField] private int healAmount;
    [SerializeField] private int damageAmount;
    [SerializeField] private int itemMineLevel;
    [SerializeField] private float attackWaitTime;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float smeltTime = 0;


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
}
