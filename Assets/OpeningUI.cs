using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningUI : MonoBehaviour
{
    public TMP_InputField namaInput;
    public LoadGameDropdown loadDropdown;
    public GameObject go;

    void Start()
    {
        namaInput.text = "";
        InventoryManager.instance.allowInventoryInput = false;
        go.SetActive(false);
    }

    public void SaveGame()
    {
        if (string.IsNullOrEmpty(namaInput.text))
        {
            Debug.LogWarning("Nama belum diisi!");
            return;
        }

        GManager.instance.playerName = namaInput.text;

        InventoryManager.instance.InitNewGame();
        GManager.instance.SavePlayer();

        
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
