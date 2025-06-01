using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public static UIControl Singleton;
    private bool isGamePaused = false;
    private int hotbarIncrementer = 0;
    [SerializeField] private GameObject hotbarSlotsHolder;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private Sprite hotbarDefaultSlotSprite;
    [SerializeField] private Sprite hotbarHighlightedSlotSprite;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Transform inventorySlots;
    [SerializeField] private InputActionReference scrollAction;
    [SerializeField] private InputActionReference hotbarAction;





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
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            OpenWidow(0);
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(IsWindowOpen())
            {
                CloseAllWindows();
            }
            else
            {
                //open pause menu
            }
        }
        if (scrollAction.action.ReadValue<Vector2>().y != 0)
        {
            hotbarIncrementer += (int)scrollAction.action.ReadValue<Vector2>().y;
            TraverseHotbarViaScrollWheel();
        }

        HightlightSelectedSlot(hotbarIncrementer);

    }

    //This function returns true of the game is paused and false if not
    public bool GetGameState()
    {
        //Debug.Log(isGamePaused);
        return isGamePaused;
    }

    //This function opens a UI window and closes all other open UI windows, if any
    public void OpenWidow(int windowId)
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

        ChangeInventorySlotsParent();


    }
    //This function closes all windows except the window that was just openend
    private void CloseAllOtherWindows(int windowId)
    {
        for (int i = 0; i < windows.Length; i++)
        {

            if (i == windowId)
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

        foreach (GameObject window in windows)
        {
            window.SetActive(false);

        }
        isGamePaused = false;
        Time.timeScale = 1.0f;

    }

    private void CloseActiveWindow(GameObject window)
    {
        isGamePaused = false;
        window.SetActive(false);
        Debug.Log(window.name);
    }

    //Returns true of any window is open and false if no windows are open
    private bool IsWindowOpen()
    {
        for(int i = 0; i < windows.Length;i++) 
        {

            if (windows[i].activeSelf)
            {
                return true;
            }

        }

        return false;
    }

    private void OnEnable()
    {
        hotbarAction.action.performed += OnHotBarPressed;
        hotbarAction.action.Enable();
    }
    private void OnDisable()
    {

        hotbarAction.action.performed -= OnHotBarPressed;
        hotbarAction.action.Disable();

    }

    #region Player Health

    public void UpdateHealthBar(int health)
    {
        healthBar.value = health;
    }
    public void UpdateHealthBarMaxValue(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }

    #endregion

    #region Crafting Table

    //This function moves the inventory slots to a different parent, so the player can see their items when crafting with any crafting system
    public void ChangeInventorySlotsParent()
    {
        if (windows[1].activeSelf)
        {
            inventorySlots.SetParent(windows[1].transform);
        }
        if (windows[0].activeSelf)
        {
            inventorySlots.SetParent(windows[0].transform);
        }
        else
        {
            return;
        }

    }

    #endregion

    #region Hotbar

    //Returns the Transform of player's selected hotbar slot
    public Transform SelectedHotbarSlot()
    {
        return hotbarSlotsHolder.transform.GetChild(hotbarIncrementer);
    }
    //Returns the InventoryItem of player's selected hotbar slot, if there is an item child attached to the slot
    public InventoryItem SelectedItemInHotbarSlot()
    {
        if (hotbarSlotsHolder.transform.GetChild(hotbarIncrementer).childCount > 0)
        {
            return hotbarSlotsHolder.transform.GetChild(hotbarIncrementer).GetChild(0).GetComponent<InventoryItem>();
        }

        return null;
    }
    private void TraverseHotbarViaScrollWheel()
    {
        if (hotbarIncrementer >= hotbarSlotsHolder.transform.childCount)
        {
            hotbarIncrementer = 0;
        }
        if (hotbarIncrementer < 0)
        {
            hotbarIncrementer = hotbarSlotsHolder.transform.childCount - 1;
        }
    }

    private void HightlightSelectedSlot(int number)
    {
        //Debug.Log("Called");
        //highlights the selected slot by assigning a new image
        hotbarSlotsHolder.transform.GetChild(number).GetComponent<Image>().sprite = hotbarHighlightedSlotSprite;
        //Traverses through the hot bar and dehighlights all slots that are not selected
        for (int i = 0; i < hotbarSlotsHolder.transform.childCount; i++)
        {
            if (i == number)
            {
                continue;
            }

            hotbarSlotsHolder.transform.GetChild(i).GetComponent<Image>().sprite = hotbarDefaultSlotSprite;
        }

    }

    private void OnHotBarPressed(InputAction.CallbackContext context)
    {
        var keyname = context.control.name;
        if (int.TryParse(keyname, out int number))
        {
            number -= 1;
            hotbarIncrementer = number;
        }

    }

    #endregion
}


