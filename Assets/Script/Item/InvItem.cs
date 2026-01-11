using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Text countText;

    [HideInInspector]public Item item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Header("UI")]
    public Image img;

    void Awake()
    {
        img = GetComponent<Image>();
        countText.raycastTarget = false;
    }

    public void InitItem (Item newItem, int newCount = 1)
    {
        item = newItem;
        count = newCount;
        img.sprite = item.image;
        refreshCount();
    }
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void refreshCount ()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        img.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
