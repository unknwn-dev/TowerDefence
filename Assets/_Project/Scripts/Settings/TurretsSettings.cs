using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TurretsSettings", menuName = "Scriptable Objects/TurretsSettings")]
public class TurretsSettings : ScriptableObject
{
    public List<TurretsSettingsModel> Turrets;
}
