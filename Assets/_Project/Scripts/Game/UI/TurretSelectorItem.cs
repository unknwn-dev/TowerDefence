using UnityEngine;
using UnityEngine.UI;

public class TurretSelectorItem : MonoBehaviour
{
    [SerializeField] private Image turretPreview;

    public void Init(TurretsSettingsModel turret)
    {
        turretPreview.sprite = turret.Prefab.GetComponent<SpriteRenderer>().sprite;
    }
}
