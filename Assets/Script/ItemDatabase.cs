using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public Item[] allItems;
    private Dictionary<string, Item> itemDict;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        BuildDatabase();
    }

    void BuildDatabase()
    {
        itemDict = new Dictionary<string, Item>();
        foreach (var item in allItems) {
            if (string.IsNullOrEmpty(item.id))
            {
                Debug.LogWarning("Item Tanpa ID: " + item.name);
                continue;
            }

            if (!itemDict.ContainsKey(item.id)) { 
                itemDict.Add(item.id, item);
            }
            else
            {
                Debug.LogWarning("ID item duplikat: " + item.id);
            }
        }
    }

    public Item GetItem(string id) {
        if (itemDict.TryGetValue(id, out Item item)) return item;

        Debug.Log("Item tidak ditemukan");
        return null;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
