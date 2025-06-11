using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    public void DebugLoad_Level(int index){
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

}
