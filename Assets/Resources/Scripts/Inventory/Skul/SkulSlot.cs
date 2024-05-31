using UnityEngine;
using UnityEngine.EventSystems;

public class SkulSlot : MonoBehaviour, IPointerEnterHandler
{
    Inventory inventory;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] Sprite DeActivedImage;
    [SerializeField] GameObject selectImage;
    [SerializeField] GameObject[] OtherImage;
    Skul _skul;
    public Skul skul
    {
        get { return _skul; }
        set
        {
            _skul = value;
            if (_skul != null)
            {
                image.sprite = skul.skulImage;
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
        if (_skul != null)
        {
            if ((skul.skulRare == Rare.노말|| skul.skulRare == Rare.레어) && skul.unitCode != PlayerCode.LittleBone)
            {
                inventory.SkulSkill1On(this);
            }
            else
            {
                inventory.SkulSkill2On(this);
            }
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
