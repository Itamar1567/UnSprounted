using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, Damageable
{
    public bool hasTakenDamage { get; set; }
    [SerializeField] private int attackTime = 1;
    [SerializeField] private int damage = 15;
    [SerializeField] private int health = 100;
    private bool canAttack = true;
    private Animator animator;
    private AIDestinationSetter destinationSetter;
    private GameObject playerRef;
    private IAstarAI ai;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = GetComponent<Animator>();
        destinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        playerRef = destinationSetter.target.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(ai.reachedDestination && canAttack)
        {
            Attack();
        }
        if(!ai.reachedDestination)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public void Die()
    {
        animator.SetBool("Die", true);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Attack()
    {
        canAttack = false;
        Debug.Log("Attacking");
        playerRef.GetComponent<Health>().TakeDamage(damage);
        StartCoroutine(AttackTimer());

    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }

}
