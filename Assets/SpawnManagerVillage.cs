using UnityEngine;

public class SpawnManagerVillage : MonoBehaviour
{
    public Transform spawnVillage; // Spawn_Village

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player tidak ditemukan!");
            return;
        }

        string spawnFrom = PlayerPrefs.GetString("SpawnFrom", "");

        // Kalau masuk Village dari Farm
        if (spawnFrom == "Farm")
        {
            player.transform.position = spawnVillage.position;
            player.transform.rotation = spawnVillage.rotation;
        }
    }
}
