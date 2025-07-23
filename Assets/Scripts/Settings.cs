using UnityEngine;

public class Settings : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Controlls()
    {
        UIControl.Singleton.OpenWidow(5);
    }
    public void Sound()
    {
        UIControl.Singleton.OpenWidow(1);
    }
    public void Return()
    {
        UIControl.Singleton.OpenWidow(3);
    }
    public void Recipes()
    {
        UIControl.Singleton.OpenWidow(6);
    }
}

