using System;
using System.IO;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;

    public string playerName;
    public static event Action OnPlayerNameChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class JsonData
    {
        public string playerName;

        public string[] itemIDs;
        public int[] itemCounts;
    }

    public void SavePlayer()
    {
        if (string.IsNullOrEmpty(playerName)) return;

        JsonData data = new JsonData();
        data.playerName = playerName;
        

        InventoryManager.instance.GetInventoryData(
            out data.itemIDs, 
            out data.itemCounts
            );
        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/save_" + playerName + ".json";
        File.WriteAllText(path, json);

        OnPlayerNameChanged?.Invoke();
        Debug.Log("Berhasil DiSave");
    }

    public void LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + "/save_" + name + ".json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        JsonData data = JsonUtility.FromJson<JsonData>(json);

        InventoryManager.instance.LoadInventoryData(
            data.itemIDs,
            data.itemCounts
            );

        playerName = data.playerName;
        OnPlayerNameChanged?.Invoke();
    }
}
