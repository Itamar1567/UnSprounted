using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{

    protected virtual void GivePower()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GivePower();
    }
}
