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
    //This function is created to easly debug newly added levels
    
    public void SetPrevSceneIndex(int index)
    {
        PrevLevIndex = index;
    }
    public int GetPrevSceneIndex()
    {
        return PrevLevIndex;
    }
   


}
