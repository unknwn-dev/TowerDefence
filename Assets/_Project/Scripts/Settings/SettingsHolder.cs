using System;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    public static SettingsHolder Instance;
    
    public TilesSettings Tiles;
    public TurretsSettings Turrets;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public TileSettingsModel FindTileByType(TileType type)
    {
        foreach (var t in Tiles.TilesList)
        {
            if(t.Type == type) return t;
        }
        Debug.LogWarning($"[SettingsHolder][FindTileByType] Couldnt find tile {type}");
        return null;
    }
}
