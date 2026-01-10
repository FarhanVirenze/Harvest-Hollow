using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    private void Awake()
    {
        instance = this;
    }

    void sleep()
    {
        PlantGrowth[] plants = FindObjectsOfType<PlantGrowth>();
        foreach (var item in plants)
        {
            item.Grow();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sleep();
        }
    }
}
