using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton;
    private Tilemap[] tilemaps;

    private void Awake()
    {
        if(Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;

        tilemaps = new Tilemap[transform.childCount];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            tilemaps[i] = transform.GetChild(i).GetComponent<Tilemap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Returns a tile map based on its layer's name
    public Tilemap GetTileMapByLayerName(string layerName)
    {
        foreach(Tilemap tile in tilemaps)
        {
            if(LayerMask.LayerToName(tile.gameObject.layer) == layerName)
            {
                return tile;
            }
        }

        return null;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode load)
    {

    }



}
