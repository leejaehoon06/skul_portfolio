using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    Image PopupImage;
    [SerializeField]
    Popup_Frame1 Frame_1;
    [SerializeField]
    Popup_Frame2 Frame_2;
    [SerializeField]
    Text Name;
    [SerializeField]
    Text Rare;
    [SerializeField]
    Text Type;
    [SerializeField]
    Transform ExpTrans;
    [SerializeField]
    Image ExpImage;
    [SerializeField]
    Image DestroyBackground;
    [SerializeField]
    TextMeshProUGUI DestroyText;
    [SerializeField]
    Image KeydownImage;
    [SerializeField]
    PopupMore SkulPopupMore;
    [SerializeField]
    PopupMore ItemPopupMore;
    public ObjSkul skul { get; set; }
    public ObjItem item { get; set; }
    List<PopupMore> popupMores = new List<PopupMore>();
    public void PopupOn()
    {
        PopupImage = GetComponent<Image>();
        if(skul != null)
        {
            Type.gameObject.SetActive(true);
            if(skul.skills.Count == 1)
            {
                Frame_1.gameObject.SetActive(true);
                Frame_2.gameObject.SetActive(false);
                Frame_1.FrameOn(skul);
                popupMores.Add(Instantiate(SkulPopupMore.gameObject, Frame_1.trans).GetComponent<PopupMore>());
                popupMores[0].OnMoreSkulPopup(skul.skills[0]);
                popupMores[0].gameObject.SetActive(false);
            }
            else
            {
                Frame_1.gameObject.SetActive(false);
                Frame_2.gameObject.SetActive(true);
                Frame_2.SkulFrameOn(skul);
                popupMores.Add(Instantiate(SkulPopupMore.gameObject, Frame_2.trans1).GetComponent<PopupMore>());
                popupMores[0].OnMoreSkulPopup(skul.skills[0]);
                popupMores[0].gameObject.SetActive(false);
                popupMores.Add(Instantiate(SkulPopupMore.gameObject, Frame_2.trans2).GetComponent<PopupMore>());
                popupMores[1].OnMoreSkulPopup(skul.skills[1]);
                popupMores[1].gameObject.SetActive(false);
            }
            Name.text = skul._skul.skulName;
            Rare.text = skul._skul.skulRare.ToString();
            Type.text = skul._skul.skulType.ToString();
            if(skul._skul.skulExpl.Length > 0)
            {
                Image ExpObj = Instantiate(ExpImage.gameObject, ExpTrans).GetComponent<Image>();
                TextMeshProUGUI ExpText = ExpObj.GetComponentInChildren<TextMeshProUGUI>();
                ExpText.text = skul._skul.skulInfo;
                ExpText.ForceMeshUpdate(true);
                int LineNum = ExpText.textInfo.lineCount - 1;
                PopupImage.rectTransform.sizeDelta += Vector2.up * 22;
                PopupImage.rectTransform.sizeDelta += Vector2.up * 11.5f * LineNum;
                ExpObj.rectTransform.offsetMin += Vector2.down * 11.5f * LineNum;
            }
            if (skul._skul.unitCode == PlayerCode.LittleBone)
            {
                DestroyBackground.gameObject.SetActive(false);
            }
            DestroyText.text += "(<sprite=1>   " + skul.bone.ToString() + ")";
            DestroyBackground.rectTransform.sizeDelta += Vector2.right * 25;
            DestroyBackground.rectTransform.sizeDelta += Vector2.right * 5 * skul.bone.ToString().Length;
        }
        else if(item != null)
        {
            Type.gameObject.SetActive(false);
            Frame_1.gameObject.SetActive(false);
            Frame_2.gameObject.SetActive(true);
            Frame_2.ItemFrameOn(item._item);
            popupMores.Add(Instantiate(ItemPopupMore.gameObject, Frame_2.trans1).GetComponent<PopupMore>());
            popupMores[0].OnMoreItemPopup(item._item.itemInscriptions[0]);
            popupMores[0].gameObject.SetActive(false);
            popupMores.Add(Instantiate(ItemPopupMore.gameObject, Frame_2.trans2).GetComponent<PopupMore>());
            popupMores[1].OnMoreItemPopup(item._item.itemInscriptions[1]);
            popupMores[1].gameObject.SetActive(false);
            Name.text = item._item.itemName;
            Rare.text = item._item.itemRare.ToString();
            Type.gameObject.SetActive(false);
            if (item._item.itemExpl.Length > 0)
            {
                Image ExpObj = Instantiate(ExpImage.gameObject, ExpTrans).GetComponent<Image>();
                TextMeshProUGUI ExpText = ExpObj.GetComponentInChildren<TextMeshProUGUI>();
                ExpText.text = item._item.itemInfo;
                ExpText.ForceMeshUpdate(true);
                int LineNum = ExpText.textInfo.lineCount - 1;
                PopupImage.rectTransform.sizeDelta += Vector2.up * 22;
                PopupImage.rectTransform.sizeDelta += Vector2.up * 11.5f * LineNum;
                ExpObj.rectTransform.offsetMin += Vector2.down * 11.5f * LineNum;
            }
        }
    }

    void Update()
    {
        if (skul != null)
        {
            if(skul.KeydownTimer == 0)
            {
                KeydownImage.fillAmount = 1f;
            }
            else
            {
                KeydownImage.fillAmount = skul.KeydownTimer;
            }
            
        }
        else if(item != null)
        {
            if (item.KeydownTimer == 0)
            {
                KeydownImage.fillAmount = 1f;
            }
            else
            {
                KeydownImage.fillAmount = item.KeydownTimer;
            }
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            for(int i = 0; i < popupMores.Count; i++)
            {
                popupMores[i].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            for (int i = 0; i < popupMores.Count; i++)
            {
                popupMores[i].gameObject.SetActive(false);
            }
        }
    }
}
