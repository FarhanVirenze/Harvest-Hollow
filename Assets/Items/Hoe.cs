using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Hoe")]
public class Hoe : Item
{
    public float range = 8f;

    public override bool Use(RaycastHit hit)
    {
        TileGround tile = hit.collider.GetComponent<TileGround>();
        if (tile == null) return false;

        tile.Hoe();
        return true;
    }
}
