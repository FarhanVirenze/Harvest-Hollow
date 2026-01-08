using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviour
{
    public TMP_Text nameText;

    void OnEnable()
    {
        UpdateName();
        GManager.OnPlayerNameChanged += UpdateName;
    }

    void OnDisable()
    {
        GManager.OnPlayerNameChanged -= UpdateName;
    }

    void UpdateName()
    {
        if (GManager.instance != null)
            nameText.text = GManager.instance.playerName;
    }

    void LateUpdate()
    {
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;
    }
}
