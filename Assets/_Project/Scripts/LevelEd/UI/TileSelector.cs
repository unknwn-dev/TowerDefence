using UnityEngine;
using TMPro;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    
    void Start()
    {
        string[] elem = System.Enum.GetNames(typeof(TileType));

        foreach (var e in elem)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(e));
        }
    }
}
