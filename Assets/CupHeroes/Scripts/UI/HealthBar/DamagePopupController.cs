using UnityEngine;

public class DamagePopupController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPopupPosition;
    [SerializeField] private GameObject _damagePopupPrefab;

    public void ShowPopup(float damageValue)
    {
        Debug.Log("DamagePopupController ShowPopup");

        GameObject damagePopup = Instantiate(_damagePopupPrefab, _spawnPopupPosition.position, Quaternion.identity);
        damagePopup.GetComponent<DamagePopup>().Setup(-damageValue);
    }

}
