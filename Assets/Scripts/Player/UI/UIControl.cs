using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public static UIControl Singleton;
    private bool isGamePaused = false;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private Slider healthBar;




    void Awake()
    {

        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            OpenWidow(0);
        }

    }

    //This function returns true of the game is paused and false if not
    public bool GetGameState()
    {
        //Debug.Log(isGamePaused);
        return isGamePaused;
    }

    //This function opens a UI window and closes all other open UI windows, if any
    private void OpenWidow(int windowId)
    {

        Debug.Log("Opening...");
        if (windows[windowId].activeSelf)
        {
            GameObject window = windows[windowId];
            CloseActiveWindow(window);
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0f;
            isGamePaused = true;
            windows[windowId].SetActive(true);
            CloseAllOtherWindows(windowId);
        }
       
        
    }
    //This function closes all windows except the window that was just openend
    private void CloseAllOtherWindows(int windowId)
    {
        for(int i = 0; i < windows.Length; i++) 
        { 
            
            if(i == windowId)
            {
                continue;
            }

            windows[i].SetActive(false);
        
        }
    }
    
    private void CloseAllWindows()
    {

        Debug.Log("Closing...");

        isGamePaused = false;

        foreach(GameObject window in windows) 
        {
            window.SetActive(false);
        
        }
    }

    private void CloseActiveWindow(GameObject window)
    {
        isGamePaused = false;
        window.SetActive(false);
        Debug.Log(window.name);
    }

    #region Player Health

    public void UpdateHealthBar (int health)
    {
        healthBar.value = health;
    }
    public void UpdateHealthBarMaxValue(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }

    #endregion

}


