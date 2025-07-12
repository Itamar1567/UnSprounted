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
        
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    protected override void Attack()
    {
        //Shoot arrow
    }
}
