using System;
using UnityEngine;

public class TileGround : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject normalGroundPrefab;
    public GameObject tilledGroundPrefab;
    public GameObject wateredGroundPrefab;
    private GameObject currentGround;
    public GameObject plantPrefab;
    public Item cornseed;

    public Transform plantPoint;
    private enum GroundState { Normal, Tilled, Watered, Planted }
    private GroundState currentState = GroundState.Normal;
    private void Start()
    {
        
    }
    public void Hoe()
    {
        if (currentState == GroundState.Normal)
        {
            Destroy(currentGround);
            Debug.Log("Mengganti tanah menjadi digarap");
            currentGround = Instantiate(tilledGroundPrefab, transform.position, Quaternion.identity, transform);
            currentState = GroundState.Tilled;
        }
    }
    public void Water()
    {
        if (currentState == GroundState.Tilled)
        {
            Destroy(currentGround);
            currentGround = Instantiate(wateredGroundPrefab, transform.position, Quaternion.identity, transform);
            currentState = GroundState.Watered;
            Debug.Log(currentState);
            Debug.Log(CanPlant());
        }
    }

    public bool CanPlant()
    {
        return currentState == GroundState.Watered;
    }

    public void Plant()
    {
        
        if (!CanPlant()) return;

        Instantiate(
            plantPrefab,
            plantPoint.position,
            Quaternion.identity,
            transform
        );
        Item selectedItem = InventoryManager.instance.GetSelectedItem(true);

        currentState = GroundState.Planted;
        Debug.Log("Tanaman ditanam");
    }
    public void ResetToNormal()
    {
        if (currentState == GroundState.Planted)
        {
            Destroy(currentGround);
            currentState = GroundState.Normal;
            Debug.Log(currentState);
            Debug.Log(CanPlant());
        }
    }
    public bool Harvest()
    {
        if (currentState != GroundState.Planted) return false;

        PlantGrowth plant = GetComponentInChildren<PlantGrowth>();
        if (plant == null) return false;

        if (plant.currentStage != 2)
        {
            Debug.Log("Tanaman belum siap panen");
            return false;
        }

        Destroy(plant.gameObject);
        ResetToNormal();
        for (int i = 0; i < 3; i++)
        {
            InventoryManager.instance.AddItem(cornseed);
        }
       
        Debug.Log("Panen berhasil, tanah kembali normal");
        return true;
    }
}
