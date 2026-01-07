using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileType tileType;
    
    [SerializeField] private SpriteRenderer image;

    public virtual void Init(TileSettingsModel s)
    {
        tileType = s.Type;
        image.sprite = s.Image;
        image.color = s.Color;
    }
}
