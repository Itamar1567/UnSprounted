using Pathfinding;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 15;
    [SerializeField] private int pushForce = 50;
    [SerializeField] private float attackWaitTime = 1f;
    [SerializeField] private float chaseDistance = 5f;

    private bool canAttack = true;
    private Animator animator;
    private AIPath ai;
    private AIDestinationSetter destinationSetter;

    protected virtual void Start()
    {
        if (GetComponent<Animator>() != null) { animator = GetComponent<Animator>(); } else { Debug.Log("Could not find 'Animator Component'"); }
        if (GetComponent<AIPath>() != null) { ai = GetComponent<AIPath>(); } else { Debug.Log("Could not find 'AIPath Component'"); }
        if (GetComponent<AIDestinationSetter>() != null) { destinationSetter = GetComponent<AIDestinationSetter>(); } else { Debug.Log("Could not find 'AIDestinationSetter Component'"); }
        health = maxHealth;
    }
    private void FixedUpdate()
    {
        if (ai.reachedDestination)
        {
            Attack();
        }
    }
    private void Update()
    {
        MoveAnimations();
        Chase();
    }
    public bool hasTakenDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log("Took damage");
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    protected virtual void Attack()
    {
        if (canAttack)
        {
            DamageTarget();
            PushTarget();
            canAttack = false;
            StartCoroutine(CanAttackTimer());
            Debug.Log("Attacked");
        }
    }

    protected virtual void PushTarget()
    {
        Rigidbody2D targetRigidBody = destinationSetter.target.GetComponent<Rigidbody2D>();
        if (targetRigidBody == null) { Debug.Log("Returned null"); return; }
        Vector2 targetDirection = (destinationSetter.target.position - ai.position).normalized;
        targetRigidBody.AddForce(targetDirection * pushForce, ForceMode2D.Impulse);
    }
    protected virtual void DamageTarget()
    {
        if (destinationSetter.target.GetComponent<Health>() != null)
        {
            destinationSetter.target.GetComponent<Health>().TakeDamage(damage);
        }
    }

    protected virtual IEnumerator CanAttackTimer()
    {
        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;

    }
    protected virtual void MoveAnimations()
    {
        Debug.Log(ai.velocity);
        if (ai.canMove)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    protected virtual void Chase()
    {
        if (destinationSetter.target != null)
        {
            float distance = Vector2.Distance(destinationSetter.target.position, transform.position);
            if (distance < chaseDistance)
            {
                ai.canMove = true;
            }
            else
            {
                ai.canMove = false;
            }
        }
    }
}
