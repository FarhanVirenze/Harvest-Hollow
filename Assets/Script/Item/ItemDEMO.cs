using UnityEngine;

public class ItemDEMO : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InventoryManager inventoryManager;
    public Item[] ItemToPickup;

    public void pickupItem(int index)
    {
        if (index < 0 || index >= ItemToPickup.Length) return;
         bool result = inventoryManager.AddItem(ItemToPickup[index]);
        if (result)
        {
            Debug.Log("Picked up: " + ItemToPickup[index]);
        }
        else
        {
            Debug.Log("Inventory Full! Could not pick up: " + ItemToPickup[index]);
        }
    }
}
