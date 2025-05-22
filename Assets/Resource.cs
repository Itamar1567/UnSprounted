using UnityEngine;

public class Resource : MonoBehaviour, Damageable
{

    private Animator animator;
    [SerializeField] private int health = 100;
    [SerializeField] private Vector2 dropAmountRange;
    [SerializeField] GameObject[] droppedItems;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasTakenDamage { get; set; }

    public void Die()
    {
        DropItems();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        Debug.Log("Hit");
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void DropItems()
    {
        //an integer to switch between dropped items
        int j = 0;
        int randomiser = Random.Range((int)dropAmountRange.x,(int)dropAmountRange.y);
        for(int i = 0; i <= randomiser; i++)
        {
            GameObject item_1 = Instantiate(droppedItems[j],transform.position, Quaternion.identity);
            if (droppedItems.Length > 1 && i == 3)
            {
                j++;
                GameObject item_2 = Instantiate(droppedItems[j], transform.position, Quaternion.identity);
            }
            if(j > droppedItems.Length)
            {
                j--;
            }
        }
    }
}
