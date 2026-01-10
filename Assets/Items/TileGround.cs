using UnityEngine;

public class TileGround : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject normalGroundPrefab;
    public GameObject tilledGroundPrefab;
    public GameObject wateredGroundPrefab;
    private GameObject currentGround;

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

    public void Plant(GameObject plantPrefab)
    {
        if (!CanPlant()) return;

        Instantiate(
            plantPrefab,
            plantPoint.position,
            Quaternion.identity,
            transform
        );

        currentState = GroundState.Planted;
        Debug.Log("Tanaman ditanam");
    }
    public void ResetToNormal()
    {
        if (currentGround != null)
            Destroy(currentGround);

        currentGround = Instantiate(
            normalGroundPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        currentState = GroundState.Normal;
    }

}
