using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class WorldInteraction : MonoBehaviour
{
    public class Block
    {
        public int itemId;
        public string blockName;
        public GameObject block;

    }

    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject Indicator;
    [SerializeField] private GameObject[] blockObjects;
    [SerializeField] private List<Block> BlocksList = new List<Block>();
    [SerializeField] private InputActionReference interactAction;
    private string layerName;
    private Vector2 mousePos;
    private InventoryItem inventoryItemRef;
    private Tilemap tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlocksList.Add(new Block { itemId = 2, blockName = "CraftingTable", block = blockObjects[0] });
        BlocksList.Add(new Block { itemId = 4, blockName = "Smelter", block = blockObjects[1] });

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Indicator.transform.position = mousePos;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);



            if (LevelManager.Singleton.GetTileMapByLayerName(layerName) != null)
            {
                tilemap = LevelManager.Singleton.GetTileMapByLayerName(layerName);
                Vector3Int cellPos = tilemap.WorldToCell(mousePos);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPos);
                Indicator.transform.position = cellCenter;
                Indicator.SetActive(true);
                //Debug.Log(tilemap);
            }
            else
            {
                Indicator.SetActive(false);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PlaceBlock();
            }

            if (layerName == "Interactable")
            {
                //Crafting table
                if (hit.collider.GetComponent<BlockIdentifier>().GetItemId() == 2)
                {
                    if(interactAction.action.WasPressedThisFrame())
                    {
                        Debug.Log("Fafaff");
                        UIControl.Singleton.OpenWidow(1);
                    }
                }
                //Smelter
                if (hit.collider.GetComponent<BlockIdentifier>().GetItemId() == 4)
                {
                    if (interactAction.action.WasPressedThisFrame())
                    {
                        UIControl.Singleton.OpenWidow(2);
                    }
                }
            }
        }

        
    }



    #region Block Placement

    private void PlaceBlock()
    {
        if (UIControl.Singleton.SelectedItemInHotbarSlot() != null && layerName == "Ground")
        {
            inventoryItemRef = UIControl.Singleton.SelectedItemInHotbarSlot();
            if (inventoryItemRef.isBlock())
            {
                foreach (Block block in BlocksList)
                {
                    if (block.itemId == inventoryItemRef.GetItemID())
                    {
                        Debug.Log(block.blockName);
                        GameObject PlacedObject = Instantiate(block.block, Indicator.transform.position, Quaternion.identity);
                        PlacedObject.GetComponent<BlockIdentifier>().SetItemId(block.itemId);
                        Destroy(inventoryItemRef.gameObject);
                    }
                }
            }
        }

        

    }

    #endregion
}
