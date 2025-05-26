using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class WorldInteraction : MonoBehaviour
{

    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject Indicator;
    private string layerName;
    private Vector2 mousePos;
    private Tilemap tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Indicator.transform.position = mousePos;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
        {
            layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);

            

            if (LevelManager.Singleton.GetTileMapByLayerName(layerName) != null)
            {
                tilemap = LevelManager.Singleton.GetTileMapByLayerName(layerName);
                Vector3Int cellPos = tilemap.WorldToCell(mousePos);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPos);
                Indicator.transform.position = cellCenter;
                //Indicator.transform.localScale = gameObject.GetComponent<Grid>().cellSize;
                Indicator.SetActive(true);
                Debug.Log(tilemap);
            }
            else
            {
                Indicator.SetActive(false);
            }

        }
        
    }
}
