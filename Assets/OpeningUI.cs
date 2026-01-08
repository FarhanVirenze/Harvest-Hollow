using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningUI : MonoBehaviour
{
    public TMP_InputField namaInput;

    void Start()
    {
        // Kalau sudah pernah save, langsung isi input field
        if (GManager.instance != null)
        {
            namaInput.text = GManager.instance.playerName;
        }
    }

    // =========================
    // AMBIL NAMA DARI INPUT
    // =========================
    public void IsiNama()
    {
        GManager.instance.playerName = namaInput.text;
    }

    // =========================
    // SAVE GAME
    // =========================
    public void SaveGame()
    {
        IsiNama(); // pastikan nama tersimpan
        GManager.instance.SaveJSONData();
    }

    // =========================
    // LOAD GAME
    // =========================
    public void LoadGame()
    {
        GManager.instance.LoadJSONData();
        namaInput.text = GManager.instance.playerName;
    }

    // =========================
    // PLAY GAME
    // =========================
    public void PlayGame()
    {
        IsiNama();
        GManager.instance.SaveJSONData(); // optional tapi bagus
        SceneManager.LoadScene(1);
    }
}
