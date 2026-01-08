using UnityEngine;

public class PortalSceneToHouse : MonoBehaviour
{
    public string sceneTujuan = "House";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tandai datang dari Farm
            PlayerPrefs.SetString("SpawnFrom", "Farm");

            SceneLoader.Load(sceneTujuan);
        }
    }
}
