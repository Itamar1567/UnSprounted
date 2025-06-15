using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Melee : MonoBehaviour
{

    [SerializeField] private int handDamage = 1;
    [SerializeField] private int handWaitTime = 1;
    [SerializeField] private int handRange = 1;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private Animator anim;
    int playerLayer;
    int mask;
    private int damage;    
    private float range;
    private float waitTime;
    private bool canAttack = true;
    private Movement movementRef;
    private Dictionary<int, float> angleByDirection = new Dictionary<int, float>();
    private HashSet<Damageable> damagedEnemies = new HashSet<Damageable>();


    //[SerializeField] private LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Damageable");
        mask = 1 << playerLayer;
        movementRef = GetComponent<Movement>();
        angleByDirection[1] = -180f;
        angleByDirection[2] = 360f;
        angleByDirection[3] = -90f;
        angleByDirection[4] = 90f;

    }

    // Update is called once per frame
    void Update()
    {

        if (attackAction.action.WasPressedThisFrame() && canAttack)
        {
            canAttack = false;
            Attack();

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void Attack()
    {
        if(UIControl.Singleton.SelectedItemInHotbarSlot() == null)
        {
            damage = handDamage;
            waitTime = handWaitTime;
            range = handRange;
        }
        else 
        {
            damage = UIControl.Singleton.SelectedItemInHotbarSlot().GetDamageAmount();
            waitTime = UIControl.Singleton.SelectedItemInHotbarSlot().GetTimeBetweenAttacks();
            range = UIControl.Singleton.SelectedItemInHotbarSlot().GetAttackRange();

        }
        float directionAngle = angleByDirection[movementRef.GetMovementDirection()];
        //Creates multiple raycasts(3 parameter) and uses them to detect damagable objects
        HalfCircleRayCast(transform.position, range, 15, directionAngle);
        //Resets the player's ability to attack after a set time
        StartCoroutine(AttackTimer());
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }

    private void HalfCircleRayCast(Vector2 origin, float radius, int raycount, float directionAngleDegrees)
    {
        float halfAngle = 180f;
        float startAngle = directionAngleDegrees - halfAngle / 2f;
        for(int i = 0; i < raycount; i++)
        {
            float angle = startAngle + (halfAngle/(raycount-1)) * i;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, radius, mask);
            //Debug.Log(hit.collider);
            //This piece runs over all scripts with a MonoBehaviour compenent in an object and checks if they implement the Damageable interface
            if(hit.collider != null)
            {                
                foreach(var comp in hit.collider.GetComponents<MonoBehaviour>())
                {
                    if (comp is Damageable damageable && damagedEnemies.Contains(damageable) == false)
                    {
                        Debug.Log(damage + " hit");
                        damageable.TakeDamage(15);
                        damagedEnemies.Add(damageable);
                        break;
                    }    
                }
            }

            Debug.DrawRay(origin, dir * radius, Color.red, 0.1f);
        }

        damagedEnemies.Clear();




    }

}
