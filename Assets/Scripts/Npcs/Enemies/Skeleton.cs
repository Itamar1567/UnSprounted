using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShootPointPosition();
        if (ai.reachedDestination && canShoot == true && runningShootAnimation == false)
        {
            runningShootAnimation = true;
            animator.SetTrigger("Attack");
        }
        MoveAnimations();
        Chase();
    }
    public override void PerformShoot()
    {
        base.Shoot();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
    }
    protected override void Shoot()
    {
        base.Shoot();
    }

    protected override IEnumerator CanShootTimer()
    {
        return base.CanShootTimer();
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

    protected override void ChangeShootPointPosition()
    {
        base.ChangeShootPointPosition();
    }
}
