[System.Serializable]
public class SerializableTile
{
    public TileType tileType;
    
    public SerializableTile(Tile tile)
    {
        this.tileType = tile.tileType;
    }
    
    public SerializableTile(TileType tileType)
    {
        this.tileType = tileType;
    }
}
