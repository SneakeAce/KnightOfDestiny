using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _popupText;

    public void Setup(float value)
    {
        Debug.Log("DamagePopup Setup");

        _popupText.text = value.ToString();

    }

}
