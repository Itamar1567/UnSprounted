using UnityEngine;

public class Bed : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAsPlayerSpawn()
    {
        GameManager.Singleton.SetPlayerSpawnPoint(transform);
    }

    public override void PerformAction()
    {
        if (DayCycle.Singleton.isDay == false)
        {
            SetAsPlayerSpawn();
            GameManager.Singleton.ResetDay();
        }
        else
        {
            SetAsPlayerSpawn();
            Debug.Log("Can only sleep at night");
        }
    }
}
