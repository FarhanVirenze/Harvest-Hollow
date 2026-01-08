using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnFromHouse;
    public Transform spawnFromVillage;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string spawnFrom = PlayerPrefs.GetString("SpawnFrom", "House");

        if (spawnFrom == "Village")
        {
            player.transform.position = spawnFromVillage.position;
        }
        else
        {
            player.transform.position = spawnFromHouse.position;
        }
    }
}
