using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Singleton;
    private Vector2 playerSpawnPoint;
   
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
        if(Keyboard.current.mKey.wasPressedThisFrame)
        {
            DayCycle.Singleton.SetNight();
        }
    }

    public void ResetDay()
    {
        Debug.Log("Day Reset");
        DayCycle.Singleton.SetDay();
    }
    public void SetPlayerSpawnPoint(Transform position)
    {
        playerSpawnPoint = position.position;
    }

    public Vector2 GetPlayerSpawnPoint()
    {
        Debug.Log(playerSpawnPoint);
        return playerSpawnPoint;
    }
}
