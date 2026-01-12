using System.Collections.Generic;
using System.IO;
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

    public void Save()
    {
        LevelData save = new LevelData();

        save.tiles = new TilesDictionary();

        foreach (var t in tiles)
        {
            save.tiles.Add(t.Key, new SerializableTile(t.Value));
        }

        save.mobsPath = CalculateMobsPath();
        
        string json = JsonUtility.ToJson(save, true);
        string filePath = Path.Combine(Application.persistentDataPath, "leveldata.json");
    
        File.WriteAllText(filePath, json);
        
        Debug.Log($"Level saved to: {filePath}, {save.tiles.Count}");
    }

    private List<Vector2Int> CalculateMobsPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        
        Tile startTile = null;
        Vector2Int startTilePos = Vector2Int.zero;
        
        foreach (var t in tiles)
        {
            if (t.Value != null && t.Value.tileType == TileType.Start)
            {
                startTile = t.Value;
                startTilePos = t.Key;
                break;
            }
        }

        if (startTile == null)
        {
            Debug.LogError("[EditorCanvas][CalculateMobsPath] No start tile finded");
            return null;
        }
        
        Tile curTile = startTile;
        Vector2Int curTilePos = startTilePos;
        visited.Add(curTilePos);
        path.Add(curTilePos);
        
        Vector2Int dir = Vector2Int.zero;

        while (curTile.tileType != TileType.End)
        {
            bool isFinded = false;
            
            //Horizontal check
            for (int i = -1; i <= 1; i += 2)
            {
                Vector2Int curDir = new Vector2Int(i, 0);
                Vector2Int pos = curTilePos + curDir;
                    
                if (!tiles.ContainsKey(pos)||
                    visited.Contains(pos)) continue;
                    
                Tile t = tiles[pos];
                if (t != null && (t.tileType == TileType.Path || t.tileType == TileType.End))
                {
                    curTilePos = pos;
                    curTile = t;
                    visited.Add(pos);
                    isFinded = true;
                    if (dir != curDir)
                    {
                        path.Add(pos);
                        dir = curDir;
                    }
                    break;
                }
            }
                
            //Vertical check
            if (!isFinded)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2Int curDir = new Vector2Int(0, i);
                    Vector2Int pos = curTilePos + curDir;
                    
                    if (!tiles.ContainsKey(pos)||
                        visited.Contains(pos)) continue;
                    
                    Tile t = tiles[pos];
                    if (t != null && (t.tileType == TileType.Path || t.tileType == TileType.End))
                    {
                        curTilePos = pos;
                        curTile = t;
                        visited.Add(pos);
                        isFinded = true;
                        if (dir != curDir)
                        {
                            path.Add(pos);
                            dir = curDir;
                        }
                        break;
                    }
                }
            }

            if (!isFinded)
            {
                Debug.LogError("[EditorCanvas][CalculateMobsPath] No path finded from position " + curTilePos);
                break;
            }
        }
        
        return path;
    }
}
