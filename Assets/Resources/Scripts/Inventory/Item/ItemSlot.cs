using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler
{
    Inventory inventory;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] Sprite DeActivedImage;
    [SerializeField] GameObject selectImage;
    [SerializeField] GameObject[] OtherImage;
    Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.SetNativeSize();
            }
            else
            {
                image.sprite = DeActivedImage;
                image.SetNativeSize();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    public void Select()
    {
        selectImage.SetActive(true);
        otherSelectImageOff();
        if (_item != null)
        {
            inventory.ItemOn(this);
        }
        else
        {
            inventory.NoneOn();
        }
    }
    void otherSelectImageOff()
    {
        for (int i = 0; i < OtherImage.Length; i++)
        {
            OtherImage[i].SetActive(false);
        }
    }
    private void OnDisable()
    {
        selectImage.SetActive(false);
    }
}
