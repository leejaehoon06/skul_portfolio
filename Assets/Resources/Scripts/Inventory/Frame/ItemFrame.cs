using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    [SerializeField]
    ItemFrameMore itemFrameMore;
    public Item item { get; private set; }
    [SerializeField]
    Image ItemIcon;
    [SerializeField]
    Text ItemName;
    [SerializeField]
    Text ItemRare;
    [SerializeField]
    Text ItemExplain;
    [SerializeField]
    TextMeshProUGUI Information;
    [SerializeField]
    GameObject Detail;
    [SerializeField]
    Image[] InscriptionImages;
    [SerializeField]
    Text[] InscriptionNames;
    [SerializeField]
    TextMeshProUGUI[] InscriptionNums;
    [SerializeField]
    InscriptionSlot[] InscriptionSlots;
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            itemFrameMore.gameObject.SetActive(true);
            itemFrameMore.FrameOn(item);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            itemFrameMore.gameObject.SetActive(false);
            FrameOn(item);
        }
    }
    private void OnDisable()
    {
        itemFrameMore.gameObject.SetActive(false);
    }
    public void FrameOn(Item _item)
    {
        itemFrameMore.IsOnTrue();
        item = _item;
        ItemIcon.sprite = item.itemInvenIcon;
        ItemIcon.SetNativeSize();
        ItemIcon.rectTransform.pivot = new Vector2(item.itemInvenIcon.pivot.x / ItemIcon.rectTransform.sizeDelta.x,
            item.itemInvenIcon.pivot.y / ItemIcon.rectTransform.sizeDelta.y);
        ItemName.text = item.itemName;
        ItemRare.text = item.itemRare.ToString();
        ItemExplain.gameObject.SetActive(true);
        Information.gameObject.SetActive(true);
        ItemExplain.text = item.itemExpl;
        Information.text = item.itemInfo;
        Detail.SetActive(true);
        int[] index = new int[2];
        for (int i = 0; i < InscriptionImages.Length; i++)
        {
            for (int j = 0; i < InscriptionSlots.Length; j++)
            {
                if (InscriptionSlots[j].inscriptionSkill != null)
                {
                    if (item.itemInscriptions[i] == InscriptionSlots[j].inscription)
                    {
                        index[i] = j;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            InscriptionImages[i].sprite = InscriptionSlots[index[i]]._inscripImage.sprite;
            InscriptionNames[i].text = item.itemInscriptions[i].inscripName;
            InscriptionNums[i].text = "";
            if (InscriptionSlots[index[i]].InscripColor.IndexOf(true) == -1)
            {
                for (int j = 0; j < InscriptionSlots[index[i]].inscription.inscripNum.Length; j++)
                {
                    InscriptionNums[i].text += InscriptionSlots[index[i]].inscription.inscripNum[j].ToString();
                    if (j < InscriptionSlots[index[i]].InscripColor.Count - 1)
                    {
                        InscriptionNums[i].text += "  <sprite=0>  ";
                    }
                }
            }
            else
            {
                for (int j = 0; j < InscriptionSlots[index[i]].inscription.inscripNum.Length; j++)
                {
                    if (InscriptionSlots[index[i]].InscripColor[j] == true)
                    {
                        InscriptionNums[i].text += "<#C89664>";
                        InscriptionNums[i].text += InscriptionSlots[index[i]].inscription.inscripNum[j].ToString();
                        InscriptionNums[i].text += "</color>";
                    }
                    else
                    {
                        InscriptionNums[i].text += InscriptionSlots[index[i]].inscription.inscripNum[j].ToString();
                    }
                    if (j < InscriptionSlots[index[i]].InscripColor.Count - 1)
                    {
                        InscriptionNums[i].text += "  <sprite=0>  ";
                    }

                }
            }
        }
    }
}
