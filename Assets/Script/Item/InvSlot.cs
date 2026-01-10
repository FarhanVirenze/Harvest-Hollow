using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour, IDropHandler
{
    public Image img;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        img = GetComponent<Image>();
        Deselect();
    }
    public void Select ()
    {
        img.color = selectedColor;
    }

    public void Deselect ()
    {
        img.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0)
        {
            InvItem invItem = eventData.pointerDrag.GetComponent<InvItem>();
            invItem.parentAfterDrag = transform;
            Debug.Log("OnDrop itemslot");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
