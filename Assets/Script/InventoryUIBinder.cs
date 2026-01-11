using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUIBinder : MonoBehaviour
{
    public InvSlot[] invSlots;
    public GameObject inventoryPanel;

    void Start()
    {
        InventoryManager inv = InventoryManager.instance;

        inv.invSlots = invSlots;
        inv.Inventory = inventoryPanel;

        inv.RebuildUIFromData();

        Debug.Log("Inventory UI berhasil di-bind ulang");
    }
    public void saveGame()
    {
        GManager.instance.SavePlayer();
    }
    public void exitGame()
    {
        SceneManager.LoadScene(0);
    }
}
