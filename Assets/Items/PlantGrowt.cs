using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [Header("Growth")]
    public GameObject[] growthStagePrefabs;
    public int currentStage = 0;

    [Header("Harvest")]
    public Item harvestItem;
    public int harvestAmount = 1;

    private GameObject currentVisual;

    void Start()
    {
        SpawnStage();
    }

    void SpawnStage()
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        currentVisual = Instantiate(
            growthStagePrefabs[currentStage],
            transform.position,
            Quaternion.identity,
            transform
        );
    }

    public void Grow()
    {
        if (currentStage >= growthStagePrefabs.Length - 1)
            return;

        currentStage++;
        SpawnStage();
    }

    public bool IsFullyGrown()
    {
        return currentStage == growthStagePrefabs.Length - 1;
    }

    public void Harvest()
    {
        if (!IsFullyGrown())
        {
            Debug.Log("Tanaman belum siap panen");
            return;
        }

        for (int i = 0; i < harvestAmount; i++)
        {
            InventoryManager.instance.AddItem(harvestItem);
        }

        TileGround tile = GetComponentInParent<TileGround>();
        if (tile != null)
        {
            tile.ResetToNormal();
        }

        Destroy(gameObject);

        Debug.Log("Panen berhasil");
    }
}
