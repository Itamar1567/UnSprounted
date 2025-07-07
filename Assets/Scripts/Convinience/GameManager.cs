using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Singleton;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
