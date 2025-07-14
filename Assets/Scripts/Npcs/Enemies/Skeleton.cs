using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShootPointPosition();
        if (ai.reachedDestination && canAttack == true)
        {
            animator.SetTrigger("Attack");
        }
        MoveAnimations();
        Chase();
    }
    public void PerformAttack()
    {
        if(canAttack)
        {
            canAttack = false;
            Attack();
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
    }
    protected override void Attack()
    {
        Debug.LogWarning("Attack() was called!\n" + System.Environment.StackTrace);
        StartCoroutine(CanAttackTimer());
        Vector2 targetDir = (destinationSetter.target.position - ai.position).normalized;
        Vector2 snappedDirection = new Vector2(Mathf.Round(targetDir.x), Mathf.Round(targetDir.y));
        GameObject arrow = Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);
        //Debug.Log(targetDir);
        arrow.GetComponent<Projectile>().SetTarget(destinationSetter.target.gameObject);
        arrow.GetComponent<Projectile>().SetDirection(snappedDirection);
        arrow.GetComponent<Projectile>().SetShotBy(gameObject);
        arrow.GetComponent<Projectile>().FlyToTarget();

    }

    protected override IEnumerator CanAttackTimer()
    {
        return base.CanAttackTimer();
    }

    protected override void MoveAnimations()
    {
        base.MoveAnimations();
    }

    protected override void Chase()
    {
        base.Chase();
    }
    protected override void PushTarget()
    {
        base.PushTarget();
    }

    private void ChangeShootPointPosition()
    {
        Vector2 faceDirection = (destinationSetter.target.position - ai.position).normalized;
        Vector2 snappedDirection = new Vector2(Mathf.Round(faceDirection.x), Mathf.Round(faceDirection.y));
        //Debug.Log(snappedDirection);
        animator.SetFloat("facingX", snappedDirection.x);
        animator.SetFloat("facingY", snappedDirection.y);
        shootPoint.transform.localPosition = snappedDirection * 0.5f;
    }
}
