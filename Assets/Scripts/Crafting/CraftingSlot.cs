using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler
{
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        //Creates an array with size of slot amount that is given by a mouse click
        GameObject[] slots = new GameObject[eventData.pointerCurrentRaycast.gameObject.transform.parent.childCount];
        Transform parentOfSlots = eventData.pointerCurrentRaycast.gameObject.transform.parent;

        for(int i = 0; i < parentOfSlots.childCount; i++)
        {
            slots[i] = parentOfSlots.GetChild(i).gameObject;
        }

        //This takes the populated slots array in order to inspect what items are in each slot and slots length in order to determine what type of "crafting system" it is, i.e crafting table or inventory crafting
        Crafting.Singleton.InspectCraftingSlots(slots, slots.Length);


    }

}
