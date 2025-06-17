using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Resource : MonoBehaviour, Damageable
{
    [SerializeField] private string resourceRequiredTool = "axe";
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Vector2 dropAmountRange;
    [SerializeField] GameObject[] droppedItems;
    [SerializeField] Sprite[] damageSprites;
    private int health;
    private SpriteRenderer spriteRenderer;
    private Animator animator;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObject.GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        health = maxHealth;
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
        if(animator)
        {
            animator.SetTrigger("Hit");
        }
        Debug.Log("Hit");
        health -= damage;
        DisplayDamage();
        if (health <= 0)
        {
            Die();
        }
    }
    public string GetResourceRequiredTool()
    {
        return resourceRequiredTool;
    }
    private void DropItems()
    {
        //an integer to switch between dropped items
        int j = 0;
        int randomiser = Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y);
        for (int i = 0; i <= randomiser; i++)
        {
            GameObject item_1 = Instantiate(droppedItems[j], transform.position, Quaternion.identity);
            if (droppedItems.Length > 1 && i == 3)
            {
                j++;
                GameObject item_2 = Instantiate(droppedItems[j], transform.position, Quaternion.identity);
            }
            if (j > droppedItems.Length)
            {
                j--;
            }
        }
    }

    //This function displayes the damage an object sustained by switching its sprite
    private void DisplayDamage()
    {
        float healthPercent = (float)health / maxHealth;
        Debug.Log(healthPercent);

        if (damageSprites.Length >= 2)
        {
            if (healthPercent <= 0.5f && healthPercent > 0.3f)
            {
                spriteRenderer.sprite = damageSprites[0];
            }
            else if (healthPercent <= 0.3f)
            {
                spriteRenderer.sprite = damageSprites[1];
            }
        }
    }
}
