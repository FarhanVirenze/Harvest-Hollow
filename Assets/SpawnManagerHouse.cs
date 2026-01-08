using UnityEngine;

public class SpawnManagerHouse : MonoBehaviour
{
    public Transform spawnFromFarm;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player tidak ditemukan!");
            return;
        }

        string spawnFrom = PlayerPrefs.GetString("SpawnFrom", "");

        if (spawnFrom == "Farm")
        {
            player.transform.position = spawnFromFarm.position;
            player.transform.rotation = spawnFromFarm.rotation;
        }
    }
}
