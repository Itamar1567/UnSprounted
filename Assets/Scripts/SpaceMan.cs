using System.Collections;
using UnityEngine;

public class SpaceMan : Enemy
{

    public void Update()
    {
        MoveAnimations();
        Chase();

        if (ai.reachedDestination && canShoot)
        {
            animator.SetTrigger("Attack");
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
}
