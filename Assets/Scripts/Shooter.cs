using UnityEngine;
using System.Collections;


public class Shooter : MonoBehaviour
{
    [SerializeField] protected int id;
    [SerializeField] protected float timeToHide = 1f;
    [SerializeField] protected GameObject projectile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetId() { return id; }
    public virtual void Shoot(Vector3 target, GameObject shotBy)
    {
        if (projectile != null)
        {
            Vector2 targetDir = (target - transform.position).normalized;
            if (projectile.GetComponent<Projectile>())
            {
                StartCoroutine(HideShooter());
                Projectile shot = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                shot.SetArbitraryRotation(transform.rotation);
                shot.SetDirection(targetDir);
                shot.SetShotBy(shotBy);
                shot.FlyToTarget();
            }

        }
    }

    protected virtual IEnumerator HideShooter()
    {
        yield return new WaitForSeconds(timeToHide);
        gameObject.SetActive(false);
    }
}
