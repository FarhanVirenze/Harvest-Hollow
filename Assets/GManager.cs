using System;
using System.IO;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;

    public string playerName;   // NAMA PLAYER

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadJSONData();
    }

    // =========================
    // JSON DATA
    // =========================
    [Serializable]
    class JsonData
    {
        public string jsonPlayerName;
    }

    // =========================
    // SAVE
    // =========================
    public void SaveJSONData()
    {
        JsonData data = new JsonData
        {
            jsonPlayerName = playerName
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/savefile.json", json);
    }

    // =========================
    // LOAD
    // =========================
    public void LoadJSONData()
    {
        string path = Application.dataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonData data = JsonUtility.FromJson<JsonData>(json);

            playerName = data.jsonPlayerName;
        }
    }
}
