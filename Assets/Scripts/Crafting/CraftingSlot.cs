using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler
{
    private GameObject[] slots;
    private Transform parentOfSlots;
    //Checks if there has been a change in the slot
    private bool _hasSlotChanged = false;
    //when readig or writing to has hasSlotChanged this code will trigger(i.e get/read or set/write)
    private bool hasSlotChanged
    {
        get => _hasSlotChanged;

        set
        {
            if (_hasSlotChanged != value)
            {
                _hasSlotChanged = value;
                if(value == false)
                {
                    CallInspectSlots();
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hasSlotChanged = transform.childCount > 0;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Creates an array with size of slot amount that is given by a mouse click
        slots = new GameObject[eventData.pointerCurrentRaycast.gameObject.transform.parent.childCount];
        parentOfSlots = eventData.pointerCurrentRaycast.gameObject.transform.parent;
        CallInspectSlots();
    }

    private void CallInspectSlots()
    {
        for (int i = 0; i < parentOfSlots.childCount; i++)
        {
            slots[i] = parentOfSlots.GetChild(i).gameObject;
        }

        //This takes the populated slots array in order to inspect what items are in each slot and slots length in order to determine what type of "crafting system" it is, i.e crafting table or inventory crafting
        Crafting.Singleton.InspectCraftingSlots(slots, slots.Length);
    }

}
