using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void Load_LevelByIndex(int index)
    {
        SceneController.Singleton.SetPrevSceneIndex(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(index);
    }
    public void LoadSandBox_Level()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadControl_Level()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadRecpie_Level()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadMain_Level()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadSettings_Level()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneController.Singleton.GetPrevSceneIndex());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
