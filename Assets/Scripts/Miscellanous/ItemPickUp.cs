using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] private int itemId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Picked Up");
            Inventory.Singleton.SpawnInventoryItem(itemId);
            Destroy(gameObject);
        }
    }
}
