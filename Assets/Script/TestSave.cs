using TMPro;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    public TMP_Text namaInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        if (string.IsNullOrEmpty(namaInput.text))
        {
            Debug.LogWarning("Nama belum diisi!");
            return;
        }

        GManager.instance.playerName = namaInput.text;
        GManager.instance.SavePlayer();
    }
}
