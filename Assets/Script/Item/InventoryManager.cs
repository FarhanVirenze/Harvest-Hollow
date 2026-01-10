using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        changeSelectedSlot(0);
        Inventory.SetActive(false);
        foreach (Item item in startItems)
        {
            AddItem(item);
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
    public InvSlot[] invSlots;
    public GameObject inventoryItemPrefab;
    public int maxItemCount = 4;
    public GameObject Inventory;

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

    public bool AddItem(Item item)
    {
        // Check for existing stackable item
        for (int i = 0; i < invSlots.Length; i++)
        {
            InvSlot slot = invSlots[i];
            InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxItemCount && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.refreshCount();
                return true;
            }
        }

        // Find Empty Slot
        for (int i = 0; i < invSlots.Length; i++)
        {
            InvSlot slot = invSlots[i];
            InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
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
}
