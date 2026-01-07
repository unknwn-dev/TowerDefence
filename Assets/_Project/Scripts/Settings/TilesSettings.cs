using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesSettings", menuName = "Scriptable Objects/TilesSettings")]
public class TilesSettings : ScriptableObject
{
    public List<TileSettingsModel> TilesList;
}
