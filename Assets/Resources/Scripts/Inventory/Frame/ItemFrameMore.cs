using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemFrameMore : MonoBehaviour
{
    Item item;
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
    GameObject[] InscriptionInfos;
    [SerializeField]
    InscriptionSlot[] InscriptionSlots;
    [SerializeField]
    GameObject InscriptionInfo;
    [SerializeField]
    GameObject InscriptionDescription;
    List<GameObject> obj = new List<GameObject>();
    bool IsOn = true;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void IsOnTrue()
    {
        if (obj.Count != 0)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                Destroy(obj[i]);
            }
            obj = new List<GameObject>();
        }
        IsOn = true;
    }
    private void OnDisable()
    {
        IsOnTrue();
    }
    public void FrameOn(Item _item)
    {
        if (IsOn == true)
        {
            item = _item;
            ItemExplain.gameObject.SetActive(false);
            Information.gameObject.SetActive(false);
            Detail.SetActive(false);
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
                InscriptionNums[i].text = InscriptionSlots[index[i]]._inscripNum1.text;
                for (int j = 0; j < item.itemInscriptions[i].inscripInfo.Length; j++)
                {
                    obj.Add(Instantiate(InscriptionInfo, InscriptionInfos[i].transform));
                    InscriptionInfo objInfo = obj[obj.Count - 1].GetComponent<InscriptionInfo>();
                    if (item.itemInscriptions[i].inscripInfo.Length == 3)
                    {
                        RectTransform rect = obj[obj.Count - 1].GetComponent<RectTransform>();
                        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 55f);
                    }
                    if (InscriptionSlots[index[i]].InscripColor.IndexOf(true) == -1)
                    {
                        objInfo.textNum.text = "<#7E5D3D>" + item.itemInscriptions[i].inscripNum[j].ToString() + "</color>" + "  <sprite=1>  ";
                        objInfo.textInfo.text = "<#7E5D3D>" + item.itemInscriptions[i].inscripInfo[j].ToString() + "</color>";
                    }
                    else
                    {
                        if (InscriptionSlots[index[i]].InscripColor.IndexOf(true) >= j)
                        {
                            objInfo.textNum.text = item.itemInscriptions[i].inscripNum[j].ToString() + "  <sprite=1>  ";
                            objInfo.textInfo.text = item.itemInscriptions[i].inscripInfo[j].ToString();
                        }
                        else
                        {
                            objInfo.textNum.text = "<#7E5D3D>" + item.itemInscriptions[i].inscripNum[j].ToString() + "</color>" + "  <sprite=1>  ";
                            objInfo.textInfo.text = "<#7E5D3D>" + item.itemInscriptions[i].inscripInfo[j].ToString() + "</color>";
                        }
                    }
                    if (j < item.itemInscriptions[i].inscripInfo.Length - 1)
                    {
                        obj.Add(Instantiate(InscriptionDescription, InscriptionInfos[i].transform));
                    }
                }
            }
            IsOn = false;
        }
    }
}
