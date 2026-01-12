using UnityEngine;
using System.Collections.Generic;

public class TurretSelector : MonoBehaviour
{
    [SerializeField] private TurretSelectorItem ItemPrefab;
    [SerializeField] private List<TurretSelectorItem> Items = new List<TurretSelectorItem>();
    [SerializeField] private Transform ItemsHolder;
    
    void Start()
    {
        foreach (var t in SettingsHolder.Instance.Turrets.Turrets)
        {
            TurretSelectorItem item = Instantiate(ItemPrefab, ItemsHolder);
            item.Init(t);
            Items.Add(item);
        }
    }
}
