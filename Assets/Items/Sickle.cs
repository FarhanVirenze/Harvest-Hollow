using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Sickle")]
public class Sickle : Item
{
    public float range = 8f;

    public override bool Use(RaycastHit hit)
    {
        TileGround tile = hit.collider.GetComponent<TileGround>();
        if (tile == null) return false;

        tile.Harvest();
        return true;
    }
}
