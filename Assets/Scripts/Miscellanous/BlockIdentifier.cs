using UnityEngine;

public class BlockIdentifier : MonoBehaviour
{

    private int itemId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetItemId()
    {
        return itemId;
    }
    public void SetItemId(int id)
    {
        itemId = id;
    }
}
