using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Smelting : MonoBehaviour
{

    [SerializeField] private float smeltTime = 1f;
    [SerializeField] private Image fireImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private InventorySlot smeltSlot;
    [SerializeField] private InventorySlot smeltedSlot;
    [SerializeField] private InventorySlot energySourceSlot;
    // this value checks for any changes in sprite index and then changes the sprite, used to reduce redundancy
    private int _spriteCurrentIndex = -1;
    private int spriteCurrentIndex
    {
        get => _spriteCurrentIndex;

        set
        {
            if (_spriteCurrentIndex != value)
            {
                _spriteCurrentIndex = value;
                ChangeSprite(value);
            }
        }

    }
    private bool isSmelting = false;
    private bool canSmelt = false;
    private Coroutine smeltCoroutineRef;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(smeltSlot.transform.childCount);
        //Debug.Log(isSmelting);
        if (energySourceSlot.transform.childCount > 0 && energySourceSlot.myItem.IsEnergySource() && canSmelt == false)
        {
            Debug.Log("Entered");
            UseEnergySource();
        }
        if (canSmelt == true)
        {
            if (smeltSlot.transform.childCount > 0 && isSmelting == false)
            {
                Smelt();
            }
        }
        if (isSmelting == true && smeltCoroutineRef != null)
        {
            if (smeltSlot.transform.childCount == 0)
            {
                StopCoroutine(smeltCoroutineRef);
                isSmelting = false;
                Debug.Log("Stopped smelting");
            }
            if (canSmelt == false)
            {
                StopCoroutine(smeltCoroutineRef);
                isSmelting = false;
                Debug.Log("Stopped smelting");
            }
        }
    }

    private void Smelt()
    {
        Debug.Log("Smelting...");
        isSmelting = true;
        smeltCoroutineRef = StartCoroutine(SmeltSequence());
    }

    private IEnumerator SmeltSequence()
    {



        Debug.Log(Inventory.Singleton.GetSmeltedItemEquivalence(smeltSlot.myItem));
        Item _newItem = Inventory.Singleton.GetSmeltedItemEquivalence(smeltSlot.myItem);
        if (_newItem == null)
        {
            isSmelting = false;
            yield break;
        }

        if (smeltedSlot.transform.childCount > 0 && _newItem.GetItemID() != smeltedSlot.transform.GetChild(0).GetComponent<InventoryItem>().GetItemID())
        {
            isSmelting = false;
            yield break;
        }

        yield return new WaitForSecondsRealtime(smeltTime);

        Debug.Log("Entred");
        isSmelting = false;

        Inventory.Singleton.SpawnInventoryItemAtPosition(smeltedSlot, _newItem);
        //Since an instanatiated item starts with a count of 1, we must call this to make it so the smeltedSlot item does not equal the item to be smelted about +1
        if (smeltedSlot.myItem.GetVirtualCount() == 0)
        {
            smeltedSlot.myItem.AddVirtualCount(1);
            smeltSlot.myItem.AddRemoveCount(-1);
        }
        else
        {
            Inventory.Singleton.ExchangeItemCount(smeltSlot, smeltedSlot, 1);
        }

    }

    private void UseEnergySource()
    {
        energySourceSlot.myItem.AddRemoveCount(-1);
        canSmelt = true;

        StartCoroutine(Energizing());
    }
    private IEnumerator Energizing()
    {
        float remainingTime = energySourceSlot.myItem.GetSmeltTime();
        while (remainingTime > 0f)
        {
            float timeLeftPerecentage = remainingTime / energySourceSlot.myItem.GetSmeltTime();
            if (timeLeftPerecentage <= 0.75f && timeLeftPerecentage > 0.50f)
            {
                spriteCurrentIndex = 0;
            }
            else if (timeLeftPerecentage <= 0.50f && timeLeftPerecentage > 0.35f)
            {
                spriteCurrentIndex = 1;

            }
            else if (timeLeftPerecentage <= 0.35f && timeLeftPerecentage > 0f)
            {
                spriteCurrentIndex = 2;
            }
            remainingTime -= Time.unscaledDeltaTime;
            Debug.Log(Mathf.Round(remainingTime));
            yield return null;
        }

        ChangeSprite(3);
        spriteCurrentIndex = -1;
        canSmelt = false; yield break;
    }

    private void ChangeSprite(int index)
    {
        if (index > -1 && index < sprites.Length)
        {
            fireImage.sprite = sprites[index];
        }
        else
        {
            Debug.Log("index size out of bounds");
        }
    }
}

