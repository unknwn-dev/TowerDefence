
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableTileEntry
{
    public Vector2Int key;
    public SerializableTile value;

    public SerializableTileEntry(Vector2Int key, SerializableTile value)
    {
        this.key = key;
        this.value = value;
    }
}

[System.Serializable]
public class TilesDictionary : IEnumerable<KeyValuePair<Vector2Int, SerializableTile>>
{
    [SerializeField]
    private List<SerializableTileEntry> entries = new List<SerializableTileEntry>();

    public void Add(Vector2Int key, SerializableTile value)
    {
        entries.Add(new SerializableTileEntry(key, value));
    }

    public SerializableTile this[Vector2Int key]
    {
        get
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].key.Equals(key))
                {
                    return entries[i].value;
                }
            }
            throw new KeyNotFoundException($"Key {key} not found");
        }
        set
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].key.Equals(key))
                {
                    entries[i].value = value;
                    return;
                }
            }
            Add(key, value);
        }
    }

    public bool ContainsKey(Vector2Int key)
    {
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }

    public int Count => entries.Count;

    public IEnumerator<KeyValuePair<Vector2Int, SerializableTile>> GetEnumerator()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            yield return new KeyValuePair<Vector2Int, SerializableTile>(entries[i].key, entries[i].value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}