using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private Dictionary<Vector2Int,Tile> tiles = new Dictionary<Vector2Int,Tile>();
    private LevelData levelData;

    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "leveldata.json");
    
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file not found!");
            return;
        }
    
        string json = File.ReadAllText(filePath);
        levelData = JsonUtility.FromJson<LevelData>(json);
        
        Debug.Log($"Level loaded: {filePath}, {levelData.tiles.Count}");
        
        InstantiateTiles();
    }
    
    private void InstantiateTiles()
    {
        foreach (var tile in levelData.tiles)
        {
            TileSettingsModel s = SettingsHolder.Instance.FindTileByType(tile.Value.tileType);
            Tile t = Instantiate(s.Prefab);
            t.Init(s);
            t.transform.position = new Vector3(tile.Key.x, tile.Key.y, 1);
            tiles.Add(tile.Key, t);
        }
    }
}
