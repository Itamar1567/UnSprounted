using System.Collections;
using UnityEngine;

public class Zombie : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ai.reachedDestination)
        {
            Attack();
        }
    }

    private void Update()
    {
        MoveAnimations();
        Chase();
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
        base.Attack();  
    }
    protected override void Chase()
    {
        base.Chase();
    }
    protected override void MoveAnimations()
    {
        base.MoveAnimations();
    }

    protected override void PushTarget()
    {
        base.PushTarget();
    }

    protected override void DamageTarget()
    {
        base.DamageTarget();
    }
    protected override IEnumerator CanAttackTimer()
    {
        return base.CanAttackTimer();
    }
}
