using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorController : MonoBehaviour
{
    [SerializeField] private Tile Tile;
    
    private Dictionary<Vector2Int,Tile> tiles = new Dictionary<Vector2Int,Tile>();
    
    private TileSettingsModel currTile;
    
    
    void Start()
    {
        currTile = SettingsHolder.Instance.Tiles.TilesList[0];
    }

    void Update()
    {
        OnMouseDown();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (Input.GetMouseButton(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int tilePos = Vector2Int.RoundToInt(p);

            if (tiles.ContainsKey(tilePos) && tiles[tilePos] != null) return;
            
            Tile t = Instantiate(currTile.Prefab);
            
            t.transform.position = Vector3Int.RoundToInt(p)+ Vector3.forward;
            
            t.Init(currTile);
            
            tiles[tilePos] = t; 
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int tilePos = Vector2Int.RoundToInt(p);

            if (tiles.ContainsKey(tilePos) && tiles[tilePos] != null)
            {
                Tile t = tiles[tilePos];
                Destroy(t.gameObject);
                tiles.Remove(tilePos);
            }
        }
    }

    public void TileChange(int i)
    {
        currTile = SettingsHolder.Instance.Tiles.TilesList[i];
    }
}
