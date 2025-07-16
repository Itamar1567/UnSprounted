using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference inputRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputRef.action.WasPressedThisFrame())
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        UIControl.Singleton.OpenWidow(3);
    }
    public void QuitGame()
    {
        GameManager.Singleton.QuitGame();
    }

    public void Settings()
    {

    }
}
