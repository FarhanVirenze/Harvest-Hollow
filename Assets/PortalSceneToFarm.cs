using UnityEngine;

public class PortalSceneToFarm : MonoBehaviour
{
    public string sceneTujuan = "Farm";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TANDA: player balik dari Village
            PlayerPrefs.SetString("SpawnFrom", "Village");

            SceneLoader.Load(sceneTujuan);
        }
    }
}
