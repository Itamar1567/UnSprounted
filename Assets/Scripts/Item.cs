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
    [SerializeField] int healAmount;
    [SerializeField] int damageAmount;
    [SerializeField] int itemMineLevel;
    [SerializeField] float timeToBreak;
    [SerializeField] float timeBetweenAttack;

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

    public float GetTimeToBreak()
    {
        return timeToBreak;
    }

    public float TimeBetweenAttacks()
    {
        return timeBetweenAttack;
    }

}
