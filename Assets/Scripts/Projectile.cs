using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float pushForce = 50;
    [SerializeField] private int damage = 15;
    [SerializeField] private int speed = 100;
    [SerializeField] private GameObject target;
    private GameObject shotBy;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //FlyToTarget();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FlyToTarget()
    {
        Debug.Log(GetDirection());
        Debug.LogWarning("Attack() was called!\n" + System.Environment.StackTrace);
        rb.linearVelocity = GetDirection() * speed;
    }
    public virtual void SetPushForce(float p) { pushForce = p; }
    public virtual void SetDirection(Vector2 d) { direction = d; }
    public virtual void SetDamage(int d) { damage = d; }
    public virtual void SetSpeed(int s) { speed = s; }
    public virtual void SetTarget(GameObject t) { target = t; }
    public virtual void SetShotBy(GameObject s) { shotBy = s; }

    public virtual Vector2 GetDirection() { return direction; }

    public virtual float GetPushForce() { return pushForce; }

    public virtual void PushTarget()
    {
        if (target == null && target.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D targetRigidBody = target.GetComponent<Rigidbody2D>();
            if (targetRigidBody == null) { Debug.Log("Returned null"); return; }
            Vector2 targetDirection = (target.transform.position - transform.position).normalized;
            targetRigidBody.AddForce(targetDirection * pushForce, ForceMode2D.Impulse);
        }
    }
    protected virtual void DamageTarget(Collider2D collidedWith)
    {
        foreach(var comp in collidedWith.GetComponents<MonoBehaviour>())
        {
            if(comp is Damageable damageable)
            {
                if(comp.CompareTag("Player"))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Cannot hit self or ground(Layer: 6)
        if(collision.gameObject != shotBy && collision.gameObject.layer != 6)
        {
            Debug.Log(collision.gameObject);
            PushTarget();
            DamageTarget(collision);
            Destroy(gameObject);
        }
    }


}
