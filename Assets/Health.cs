using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIControl.Singleton.UpdateHealthBarMaxValue(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.qKey.wasPressedThisFrame)
        {
            TakeDamage(15);
        }    
    }


    public void Die()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int h)
    {
        health = h;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void SetMaxHealth(int h)
    {
        maxHealth = h;
        UIControl.Singleton.UpdateHealthBarMaxValue(maxHealth);
    }
    public void IncreaseHealth(int amount)
    {
        if (health == maxHealth)
        {
            return;
        }
        if (health + amount >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
        UIControl.Singleton.UpdateHealthBar(health);

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        UIControl.Singleton.UpdateHealthBar(health);
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }
}
