using Pathfinding;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    [Header("Enemy Stats")]
    [SerializeField] protected int damage = 15;
    [SerializeField] protected int pushForce = 50;
    [SerializeField] protected float attackWaitTime = 1f;
    [SerializeField] protected float chaseDistance = 5f;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int _health;
    protected int health
    {
        get => _health;
        set
        {

            if (_health != value)
            {
                _health = value;
                Debug.Log("called");
                if (value < maxHealth / 2 && enteredHalfHealthPoint == false)
                { 
                    ReachedHalfHealth();
                }
            }

        }
    }



    [Header("If the enemy shoots")]
    [SerializeField] protected bool EnemyShoots = false;
    [SerializeField] protected GameObject shootPoint;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject quiver;
    [SerializeField] protected float shootWaitTime = 1f;
    [SerializeField] protected int projectilesCarried = 15;
    protected bool canShoot = true;
    private int currentProjectiles;
    protected bool runningShootAnimation = false;
    protected bool canAttack = true;

    [Header("Mini Boss Section")]
    private bool enteredHalfHealthPoint = false;


    //Components
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AIPath ai;
    protected AIDestinationSetter destinationSetter;

    

    


    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ai = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        if (spriteRenderer == null) { Debug.Log("Could not find 'Sprite Renderer Component'"); }
        if (animator == null) { Debug.Log("Could not find 'Animator Component'"); }
        if (ai == null) { Debug.Log("Could not find 'AIPath Component'"); }
        if (destinationSetter == null) { Debug.Log("Could not find 'AIDestinationSetter Component'"); }

        //Set shooting capabilities if enemy can shoot
        if (EnemyShoots) { SpawnProjectilePool(); currentProjectiles = projectilesCarried - 1; }

        SetTarget(GameObject.FindWithTag("Player").transform);
        health = maxHealth;
    }
    public bool hasTakenDamage { get; set; }
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
            if (animator != null)
            {
                Debug.Log("Died");
                animator.SetTrigger("Die");
            }
            else
            {
                Die();
            }
        }
    }

    #region Attack/Shoot
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
    public virtual void PerformAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            Attack();
        }
    }
    public virtual void PerformShoot()
    {
        if (canShoot)
        {
            runningShootAnimation = true;
            canShoot = false;
            Debug.Log(canShoot);
            Shoot();
        }
    }
    protected virtual void SpawnProjectilePool()
    {
        for (int i = 0; i < projectilesCarried; i++) 
        {
            Projectile shotProjectile = Instantiate(projectile, shootPoint.transform.position, Quaternion.identity, quiver.transform).GetComponent<Projectile>();
            shotProjectile.SetShotBy(gameObject);
            shotProjectile.SetResetPointParent(shootPoint.transform);

        }
    }

    protected virtual void Shoot()
    {
        Debug.LogWarning("Shoot() was called!\n" + System.Environment.StackTrace);
        StartCoroutine(CanShootTimer());
        Vector2 targetDir = (destinationSetter.target.position - ai.position).normalized;
        //Vector2 snappedDirection = new Vector2(Mathf.Round(targetDir.x), Mathf.Round(targetDir.y));
        //Debug.Log(targetDir);
        if(currentProjectiles >= 0)
        {
            //Debug.Log(currentProjectiles);
            Projectile shotProjectile = quiver.transform.GetChild(currentProjectiles).GetComponent<Projectile>();
            if (shotProjectile != null)
            {
                shotProjectile.SetTarget(destinationSetter.target.gameObject);
                shotProjectile.SetDirection(targetDir);
                shotProjectile.SetShotBy(gameObject);
                shotProjectile.FlyToTarget();
                currentProjectiles--;
            }
        }
        else if (currentProjectiles < 0)
        {
            currentProjectiles = projectilesCarried - 1;
            Projectile[] projectiles = quiver.GetComponentsInChildren<Projectile>();

            foreach (Projectile shot in projectiles)
            {
                shot.ResetProjectile();
            }

            Shoot();

        }

    }
    protected virtual void ChangeShootPointPosition()
    {
        Vector2 faceDirection = (destinationSetter.target.position - ai.position).normalized;
        if(Mathf.Abs(faceDirection.x) > Mathf.Abs(faceDirection.y))
        {
            faceDirection = new Vector2(Mathf.Round(faceDirection.x), 0);
        }
        else if(Mathf.Abs(faceDirection.y) > Mathf.Abs(faceDirection.x))
        {
            faceDirection = new Vector2(0, Mathf.Round(faceDirection.y));
        }
        Vector2 snappedDirection = new Vector2(Mathf.Round(faceDirection.x), Mathf.Round(faceDirection.y));
        //Debug.Log(snappedDirection);
        animator.SetFloat("facingX", faceDirection.x);
        animator.SetFloat("facingY", faceDirection.y);
        shootPoint.transform.localPosition = snappedDirection * 0.5f;
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
    protected virtual IEnumerator CanShootTimer()
    {
        yield return new WaitForSeconds(shootWaitTime);
        runningShootAnimation = false;
        canShoot = true;
    }

    #endregion
    #region Movement
    protected virtual void MoveAnimations()
    {
        //Debug.Log(ai.velocity);
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
    #endregion 
    #region Setters/Getters
    public virtual void SetHealth(int h) { health = h; }
    public virtual void SetTarget(Transform t) { destinationSetter.target = t; }
    public virtual int GetHealth() { return health; }

    public virtual Transform GetTarget() { return destinationSetter.target;}
    #endregion
    #region Mini Bosses
    protected virtual void ReachedHalfHealth()
    {
        enteredHalfHealthPoint = true;
    }
    #endregion


}
