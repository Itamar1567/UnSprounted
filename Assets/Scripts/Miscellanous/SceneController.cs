using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Singleton;
    private int PrevLevIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
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

    //Function to load the last level the player was present in
    public void LoadLast_Level()
    {

    }
    public void LoadIntro_Level()
    {

    }
    //This function is created to easly debug newly added levels
    public void Load_Level(int index){
        PrevLevIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
    public void LoadSandBox_Level()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMain_Level()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSettings_Level()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadPrevLevel()
    {

    }

}
