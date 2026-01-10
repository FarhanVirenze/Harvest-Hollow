using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Sickle")]
public class Sickle : Item
{
    public float range = 8f;

    public override bool Use(RaycastHit hit)
    {
        PlantGrowth plant = hit.collider.GetComponentInParent<PlantGrowth>();
        if (plant == null) return false;

        if (!plant.IsFullyGrown())
        {
            Debug.Log("Belum bisa dipanen");
            return false;
        }

        plant.Harvest();
        return true;
    }
}
