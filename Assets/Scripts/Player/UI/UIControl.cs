using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public static UIControl Singleton;
    private bool isGamePaused = false;
    private int hotbarIncrementer
    {
        get => _hotbarIncrementer;

        set
        {
            if (_hotbarIncrementer != value)
            {
                _hotbarIncrementer = value;
                ShowcaseSelectedHeldItemInText();
            }
        }
    }
    [SerializeField] private TMP_Text hotbarText;
    [SerializeField] private GameObject hotbarSlotsHolder;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private Sprite hotbarDefaultSlotSprite;
    [SerializeField] private Sprite hotbarHighlightedSlotSprite;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Transform inventorySlots;
    [SerializeField] private InputActionReference scrollAction;
    [SerializeField] private InputActionReference hotbarAction;
    private int _hotbarIncrementer = 0;
    private Coroutine textFadeCoroutineRef;






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
            if (IsWindowOpen())
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
            int tempIncrementer = hotbarIncrementer;
            tempIncrementer += (int)scrollAction.action.ReadValue<Vector2>().y;
            TraverseHotbarViaScrollWheel(tempIncrementer);
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
        if (windowId == 0 || windowId == 1 || windowId == 2)
        {
            ChangeInventorySlotsParent(windowId);
        }



    }

    //Returns true of any window is open and false if no windows are open
    public bool IsWindowOpen()
    {
        for (int i = 0; i < windows.Length; i++)
        {

            if (windows[i].activeSelf)
            {
                return true;
            }

        }

        return false;
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
    public void ChangeInventorySlotsParent(int id)
    {
        if (windows[id].activeSelf)
        {
            inventorySlots.SetParent(windows[id].transform);
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
    private void TraverseHotbarViaScrollWheel(int tempHotbarIncrementer)
    {
        Debug.Log(hotbarIncrementer);
        int count = hotbarSlotsHolder.transform.childCount;
        if (count == 0)
        {
            return;
        }
        else
        {
            hotbarIncrementer = ((tempHotbarIncrementer % count) + count) % count;
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

    private void ShowcaseSelectedHeldItemInText()
    {
        if (SelectedItemInHotbarSlot() != null)
        {
            hotbarText.text = SelectedItemInHotbarSlot().GetItemName();
            if (textFadeCoroutineRef != null)
            {
                StopCoroutine(textFadeCoroutineRef);
            }

            textFadeCoroutineRef = StartCoroutine(TextFade(1f, hotbarText));

        }
    }

    private IEnumerator TextFade(float fadeTime, TMP_Text textToFade)
    {

        textToFade.alpha = 1;
        float elapsedTime = 0f;
        float startAlpha = textToFade.alpha;
        //Calls this while loop until elapsed time which is increased in seconds is less than fadeTime
        while (elapsedTime < fadeTime)
        {
            Debug.Log(elapsedTime);
            //Lerp returns a value between startAlpha and 0 by the percentage given in elapsedTime/fadeTime or smallNum/total or the percent of the way from startAlpha to 0f i.e 100% = 0f
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeTime);
            textToFade.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;

        }
    }

    #endregion

    #region Smelter

    #endregion
}


