using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{

    private RaycastHit2D hit;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private int damage = 5;
    //[SerializeField] private LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(attackAction.action.WasPressedThisFrame())
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,1);
        foreach(Collider2D hitable in hits) 
        {
        
            Damageable damageable = hitable.GetComponent<Damageable>();

            if(damageable != null)
            {
                damageable.TakeDamage(damage);

            }
        
        
        }
        
    }    
}
