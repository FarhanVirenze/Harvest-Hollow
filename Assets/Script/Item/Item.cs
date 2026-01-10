using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public abstract class Item : ScriptableObject
{
    [Header("Item Property")]
    public string itemName;
    public Sprite image;
    public bool stackable = true;

    public abstract bool Use(RaycastHit hit);
}

public enum ItemType
{
    Plant,
    Tool
}

public enum ActionType
{
    Dig,
    Mine
}
