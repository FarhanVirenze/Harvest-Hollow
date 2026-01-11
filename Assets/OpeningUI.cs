using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningUI : MonoBehaviour
{
    public TMP_InputField namaInput;
    public LoadGameDropdown loadDropdown;

    void Start()
    {
        namaInput.text = "";
        InventoryManager.instance.allowInventoryInput = false;
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

        // 🔥 REFRESH DROPDOWN BIAR REALTIME
        loadDropdown.ShowDropdown();

        Debug.Log("Player disimpan & aktif: " + GManager.instance.playerName);
    }

    public void LoadGame()
    {
        loadDropdown.ShowDropdown();
    }

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(GManager.instance.playerName))
        {
            Debug.Log("PLAY AS: " + GManager.instance.playerName);
            InventoryManager.instance.allowInventoryInput = true;
            SceneManager.LoadScene(1);
            return;
        }

        Debug.LogWarning("Belum ada player aktif. Silakan Save Player atau Load Player terlebih dahulu.");
    }


}
