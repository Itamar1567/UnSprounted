using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{

    [SerializeField] private int damage = 5;
    [SerializeField] private int handDamage = 1;
    [SerializeField] private float attackWaitTime = 5;
    [SerializeField] private float handAttackTime = 1;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private Animator anim;
    private bool canAttack = true;
    private RaycastHit2D hit;
    private InventoryItem selectedItemRef;


    //[SerializeField] private LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (attackAction.action.WasPressedThisFrame() && canAttack)
        {

            Attack();

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void Attack()
    {

        canAttack = false;
        //anim.SetTrigger("Attack");
        if (UIControl.Singleton.SelectedItemInHotbarSlot() != null)
        {
            selectedItemRef = UIControl.Singleton.SelectedItemInHotbarSlot();
            damage = selectedItemRef.GetDamageAmount();
            attackWaitTime = selectedItemRef.GetTimeBetweenAttacks();

        }
        else
        {
            damage = handDamage;
            attackWaitTime = handAttackTime;
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D hitable in hits)
        {
            Damageable damageable = hitable.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        //Resets the player's ability to talk after a set time
        StartCoroutine(AttackTimer());
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;
    }

}
