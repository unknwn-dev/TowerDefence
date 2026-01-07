using System;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    public static SettingsHolder Instance;
    
    public TilesSettings Tiles;

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
}
