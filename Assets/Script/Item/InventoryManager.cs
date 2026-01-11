using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlotData
    {
        public string itemID;
        public int count;
    }
    public InventorySlotData[] inventoryData;

    public static InventoryManager instance;
    public Item[] startItems;
    public bool isLoadedFromSave = false;
    public InvSlot[] invSlots;
    public GameObject inventoryItemPrefab;
    public int maxItemCount = 4;
    public GameObject Inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        changeSelectedSlot(0);
        Inventory.SetActive(false);
        Debug.Log(Inventory);

    }

    public void InitNewGame()
    {
        ClearInventory();

        foreach (Item item in startItems)
        {
            AddItem(item);
        }
    }
    void ClearInventory()
    {
        foreach (InvSlot slot in invSlots)
        {
            InvItem item = slot.GetComponentInChildren<InvItem>();
            if (item != null)
                Destroy(item.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeSelectedSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeSelectedSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeSelectedSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeSelectedSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            changeSelectedSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            changeSelectedSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            changeSelectedSlot(6);
        }
    }

    //


    int selectedSlot = -1;

    void changeSelectedSlot(int newIndex)
    {
        if (selectedSlot >= 0)
        {
            invSlots[selectedSlot].Deselect();
        }
        
        invSlots[newIndex].Select();
        selectedSlot = newIndex;
    }

    public InvItem AddItem(Item item)
    {
        SyncUIToData();

        // Stack dulu
        foreach (InvSlot slot in invSlots)
        {
            InvItem existing = slot.GetComponentInChildren<InvItem>();
            if (existing != null &&
                existing.item.id == item.id &&
                existing.item.stackable &&
                existing.count < maxItemCount)
            {
                existing.count++;
                existing.refreshCount();
                return existing;
            }
        }

        // Slot kosong
        foreach (InvSlot slot in invSlots)
        {
            if (slot.GetComponentInChildren<InvItem>() == null)
            {
                GameObject obj = Instantiate(inventoryItemPrefab, slot.transform);
                InvItem invItem = obj.GetComponent<InvItem>();
                invItem.InitItem(item, 1);
                return invItem;
            }
        }

        return null;
    }


    void SpawnNewItem(Item item, InvSlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InvItem invItem = newItemGo.GetComponent<InvItem>();
        invItem.InitItem(item);
    }
    public Item GetSelectedItem(bool use)
    {
        InvSlot slot = invSlots[selectedSlot];
        InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            
            if (use == true)
            {
                SyncUIToData();
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else {
                    itemInSlot.refreshCount();
                }
            }
            return item;
        }
        return null;
    }
    public void GetInventoryData(out string[] itemIDs, out int[] itemCounts)
    {
        itemIDs = new string[invSlots.Length];
        itemCounts = new int[invSlots.Length];

        for (int i = 0; i < invSlots.Length; i++)
        {
            InvItem invItem = invSlots[i].GetComponentInChildren<InvItem>();
            if (invItem != null)
            {
                itemIDs[i] = invItem.item.id;
                itemCounts[i] = invItem.count;
            }
            else
            {
                itemIDs[i] = "";
                itemCounts[i] = 0;
            }
        }
    }
    public void LoadInventoryData(string[] itemIDs, int[] itemCounts)
    {
        isLoadedFromSave = true;
        if (invSlots == null || invSlots.Length == 0)
        {
            Debug.LogError("invSlots belum siap!");
            return;
        }

        if (inventoryItemPrefab == null)
        {
            Debug.LogError("inventoryItemPrefab belum di-assign!");
            return;
        }

        if (itemIDs == null || itemCounts == null)
        {
            Debug.LogWarning("Data inventory kosong");
            return;
        }

        // Bersihkan slot dulu
        foreach (InvSlot slot in invSlots)
        {
            if (slot == null) continue;

            InvItem existing = slot.GetComponentInChildren<InvItem>();
            if (existing != null)
                Destroy(existing.gameObject);
        }

        // Load item
        for (int i = 0; i < itemIDs.Length; i++)
        {
            if (string.IsNullOrEmpty(itemIDs[i])) continue;

            Item item = ItemDatabase.instance.GetItem(itemIDs[i]);
            if (item == null)
            {
                Debug.LogWarning("Item ID tidak ditemukan: " + itemIDs[i]);
                continue;
            }

            if (!AddItem(item))
            {
                Debug.LogWarning("Inventory penuh, item dilewati: " + item.name);
                break;
            }

            // Set count (AddItem cuma +1)
            InvItem invItem = invSlots[i].GetComponentInChildren<InvItem>();
            if (invItem != null)
            {
                invItem.count = itemCounts[i];
                invItem.refreshCount();
            }
        }
        SyncUIToData();

    }
    public void SyncUIToData()
    {
        if (invSlots == null || invSlots.Length == 0)
        {
            Debug.LogWarning("Sync gagal: invSlots null");
            return;
        }

        inventoryData = new InventorySlotData[invSlots.Length];

        for (int i = 0; i < invSlots.Length; i++)
        {
            InvItem item = invSlots[i].GetComponentInChildren<InvItem>();

            if (item != null)
            {
                inventoryData[i] = new InventorySlotData
                {
                    itemID = item.item.id,
                    count = item.count
                };
            }
            else
            {
                inventoryData[i] = new InventorySlotData
                {
                    itemID = "",
                    count = 0
                };
            }
        }

        Debug.Log("Inventory DATA tersimpan");
    }
    public void RebuildUIFromData()
    {
        if (inventoryData == null)
        {
            Debug.LogWarning("Tidak ada inventory data untuk dibangun");
            return;
        }

        ClearInventory();

        for (int i = 0; i < inventoryData.Length; i++)
        {
            if (string.IsNullOrEmpty(inventoryData[i].itemID)) continue;

            Item item = ItemDatabase.instance.GetItem(inventoryData[i].itemID);
            if (item == null) continue;

            GameObject obj = Instantiate(inventoryItemPrefab, invSlots[i].transform);
            InvItem invItem = obj.GetComponent<InvItem>();
            invItem.InitItem(item, inventoryData[i].count);
        }

        Debug.Log("Inventory UI dibangun ulang dari DATA");
    }

}
