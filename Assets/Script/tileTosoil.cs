using UnityEngine;

public class tileTosoil : MonoBehaviour
{
    public GameObject tilledPrefab;
    private bool isTilled = false;

    public void Hoe()
    {
        if (isTilled) return;

        isTilled = true;

        Instantiate(tilledPrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
