using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Seed/CornSeed")]
public class CornSeed : Item
{
    public GameObject cornPlantPrefab;
    public float range = 12f;
    public override bool Use(RaycastHit hit)
    {
        Debug.Log("Menggunakan CornSeed pada tile: " + hit.collider.name);
        TileGround tile = hit.collider.GetComponent<TileGround>();
        if (tile == null) return false;

        tile.Plant(cornPlantPrefab);
        return true;
    }
}
