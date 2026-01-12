using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TurretSelector TurretSelector;

    public void OpenTurretSelector()
    {
        TurretSelector.gameObject.SetActive(true);
    }
}
