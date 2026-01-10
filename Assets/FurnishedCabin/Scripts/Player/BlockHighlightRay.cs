using UnityEngine;

public class BlockHighlightRay : MonoBehaviour
{
    [Header("Ray Settings")]
    public float rayDistance = 8f;
    public LayerMask blockLayer;
    public Material highlightMaterial;

    private GameObject currentBlock;
    private Material originalMaterial;

    void Update()
    {
        if (Camera.main == null) return;

        Vector3 rayOrigin = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        Ray ray = new Ray(rayOrigin, Camera.main.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        if (hits.Length == 0)
        {
            ClearHighlight();
            return;
        }

        GameObject hitBlock = null;
        float closestDist = Mathf.Infinity;

        // Cari hit terdekat yang termasuk blockLayer
        foreach (var hit in hits)
        {

            if (((1 << hit.collider.gameObject.layer) & blockLayer) != 0)
            {
                if (hit.distance < closestDist)
                {
                    closestDist = hit.distance;
                    hitBlock = hit.collider.gameObject;
                }
            }
        }

        // Highlight hanya setelah loop selesai
        if (hitBlock != null)
        {
            if (currentBlock != hitBlock)
            {
                ClearHighlight();

                currentBlock = hitBlock;
                Renderer r = currentBlock.GetComponentInChildren<Renderer>();
                if (r != null)
                {
                    originalMaterial = r.material;
                    r.material = highlightMaterial;
                }
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (currentBlock != null)
        {
            Renderer r = currentBlock.GetComponentInChildren<Renderer>();
            if (r != null)
                r.material = originalMaterial;

            currentBlock = null;
        }
    }
}
