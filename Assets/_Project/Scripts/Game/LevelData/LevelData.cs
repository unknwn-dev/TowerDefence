using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    private Dictionary<Vector2Int, Tile> tiles;
    private List<Vector2Int> mobsPath;
}
