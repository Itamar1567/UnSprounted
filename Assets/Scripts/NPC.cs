using Pathfinding;
using UnityEngine;

public class NPC : MonoBehaviour, Damageable
{

    protected int maxHealth = 100;
    protected int health = 100;

    public bool hasTakenDamage { get; set; }

    protected GameObject[] ItemsToDrop;

    protected Animator animator;

    protected AIPath ai;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<Animator>() != null) { animator = GetComponent<Animator>(); } else { Debug.Log("No animator attached"); };
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Inititalize()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {

        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }

    }

    public void Die()
    {
        if(animator != null)
        {
            animator.SetBool("Die", true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void MoveAnimations()
    {
        if(ai.canMove)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    protected void DropItems()
    {
        
    }
}
