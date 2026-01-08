using UnityEngine;

public class PortalSceneToVillage : MonoBehaviour
{
    public string sceneTujuan = "Village";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player datang dari Farm
            PlayerPrefs.SetString("SpawnFrom", "Farm");

            SceneLoader.Load(sceneTujuan);
        }
    }
}
