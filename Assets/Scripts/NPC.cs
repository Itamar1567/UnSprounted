using Pathfinding;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour, Damageable
{

    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int health = 100;
    [SerializeField] protected float idleTime = 3f;
    [SerializeField] protected float allowableWalkRadius = 3f;

    [Header("Dropped Items Section")]

    [SerializeField] bool dropItems;
    [SerializeField] int startAmount = 0;
    [SerializeField] int endAmount = 5;
    [SerializeField] protected GameObject[] ItemsToDrop;


    protected bool isChangePosCoroutineRunning = false;
    public bool hasTakenDamage { get; set; }

    protected Animator animator;

    protected AIPath ai;

    protected AIDestinationSetter destinationSetter;

    //This must be initialized before calling MoveRandom as it is the reference point for the sheep's target
    private GameObject randomTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (GetComponent<Animator>() != null) { animator = GetComponent<Animator>(); } else { Debug.Log("No animator attached"); };
        if (GetComponent<AIDestinationSetter>() != null) { destinationSetter = GetComponent<AIDestinationSetter>(); } else { Debug.Log("No AIDestinationSetter attached"); };
        if (GetComponent<AIPath>() != null) { ai = GetComponent<AIPath>(); } else { Debug.Log("No AIPath attached"); };
        Inititalize();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


    protected virtual void Inititalize()
    {
        health = maxHealth;
        randomTarget = new GameObject();
        RandomMove();

    }

    public virtual void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            PerformDeath();
        }

    }

    public void PerformDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        if (dropItems == true)
        {
            DropItems();
        }
        Destroy(randomTarget);
        Destroy(gameObject);
    }
    protected virtual void MoveAnimations()
    {
        if (ai.canMove)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    protected virtual void DropItems()
    {
        int differentItemIterator = Random.Range(0, ItemsToDrop.Length);
        int randNum = Random.Range(startAmount, endAmount);
        for (int i = 0; i <= differentItemIterator; i++)
        {
            for (int j = 0; j <= randNum; j++)
            {
                Instantiate(ItemsToDrop[i], transform.position, Quaternion.identity);
            }
        }
    }

    protected virtual void RandomMove()
    {
        //could be null
        Vector2? newPostion;
        newPostion = FindPointOnWalkable();

        if (newPostion.HasValue && randomTarget != null)
        {
            randomTarget.transform.position = newPostion.Value;
            destinationSetter.target = randomTarget.transform;
        }

    }

    protected virtual IEnumerator MovePosition()
    {
        randomTarget.SetActive(false);
        isChangePosCoroutineRunning = true;
        yield return new WaitForSeconds(idleTime);
        EndMoveCoroutine();
    }

    protected void EndMoveCoroutine()
    {
        randomTarget.SetActive(true);
        RandomMove();
        isChangePosCoroutineRunning = false;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, allowableWalkRadius);
    }

    //Function to retrieve a walkable point of the npc, can return null if not found
    protected virtual Vector2? FindPointOnWalkable()
    {
        int maxAttempts = 30;

        for (int i = 0; i < maxAttempts; i++)
        {

            //retireves a random point within a circle around the gameObject
            Vector2 randomOffset = Random.insideUnitCircle * allowableWalkRadius;
            Vector2 randomPoint = (Vector2)transform.position + randomOffset;
            //Checks wether randomPoint is inside a walkable collider(ground)
            Collider2D hit = Physics2D.OverlapPoint(randomPoint, groundLayer);

            if (hit != null)
            {
                Debug.Log("Walkable point at: " + randomPoint);
                return randomPoint;
            }
        }
        Debug.Log("Could not find a walkable point");
        return null;
    }
}
