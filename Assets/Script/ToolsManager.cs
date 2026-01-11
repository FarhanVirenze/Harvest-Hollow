using UnityEngine;

public class ToolController : MonoBehaviour
{
    public LayerMask tileLayer;
    public float rayDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Tombol Q ditekan");

            // Ambil item yang sedang dipilih
            Item activeItem = InventoryManager.instance.GetSelectedItem(false);
            if (activeItem == null) return;

            if (activeItem is Hoe hoeTool)
            {
                UseTool(hoeTool.range, hoeTool);
                Debug.Log("Menggunakan cangkul");
            }
            else if (activeItem is WateringCan waterTool)
            {
                Debug.Log("Menggunakan penyiram");
                UseTool(waterTool.range, waterTool);
            }
            else if (activeItem is CornSeed cornSeed)
            {
                Debug.Log("Menggunakan benih jagung");
                UseTool(cornSeed.range, cornSeed);
            }
            else if(activeItem is Sickle sickle)
            {
                Debug.Log("Menggunakan Arit");
                UseTool(sickle.range, sickle);
            }
        }
    }

    void UseTool(float range, object tool)
    {
        if (Camera.main == null)
        {
            Debug.LogWarning("Camera.main is null");
            return;
        }
        ;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        Debug.DrawRay(ray.origin, ray.direction * range, Color.blue, 2f);
        Debug.Log("Melakukan raycast untuk tool dengan range: " + range);

        RaycastHit[] hits = Physics.RaycastAll(ray, range);
        Debug.Log("Jumlah hits dari RaycastAll: " + hits.Length);

        foreach (var hit in hits)
        {
            Debug.Log($"Hit collider: {hit.collider.name}, layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}, distance: {hit.distance}");

            if (((1 << hit.collider.gameObject.layer) & tileLayer) != 0)
            {
                TileGround tile = hit.collider.GetComponent<TileGround>();
                if (tile != null)
                {
                    bool used = false;
                    if (tool is Hoe hoe)
                    {
                        used = hoe.Use(hit);
                        Debug.Log("Menggunakan cangkul pada tile: " + tile.name);
                    }
                    else if (tool is WateringCan wateringCan)
                    {
                        used = wateringCan.Use(hit);
                        Debug.Log("Menggunakan penyiram pada tile: " + tile.name);
                    }
                    else if (tool is CornSeed corn)
                    {
                        used = corn.Use(hit);
                        Debug.Log("Tool berhasil digunakan pada tile: " + tile.name);
                        break;
                    } else if (tool is Sickle sickle)
                    {
                        used = sickle.Use(hit);
                        Debug.Log("Tool berhasil digunakan pada tile: " + tile.name);
                        break;
                    }

                }
            }
        }
    }
}
