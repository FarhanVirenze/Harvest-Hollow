using UnityEngine;

public class PortalSceneLoader : MonoBehaviour
{
    public string sceneTujuan = "Farm";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tandai datang dari House
            PlayerPrefs.SetString("SpawnFrom", "House");

            SceneLoader.Load(sceneTujuan);
        }
    }
}