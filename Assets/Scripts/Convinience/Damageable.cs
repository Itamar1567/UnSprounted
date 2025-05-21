using UnityEngine;

public interface Damageable
{
    public void TakeDamage(int damage);

    public bool hasTakenDamage { get; set; }

    public void Die();

}
