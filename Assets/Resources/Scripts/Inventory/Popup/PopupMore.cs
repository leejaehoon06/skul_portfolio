using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopupMore : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField]
    Image image;
    [SerializeField]
    Text text;
    [SerializeField]
    Text Num;
    [SerializeField]
    InscriptionInfo PopupInfo;
    public void OnMoreSkulPopup(Skill skill)
    {
        rectTransform = GetComponent<RectTransform>();
        image.sprite = skill.skillImage;
        text.text = skill.skillName;
        Num.text = skill.skillCool.ToString();
        GameObject obj = Instantiate(PopupInfo.gameObject, transform);
        obj.GetComponent<InscriptionInfo>().textInfo.text = skill.skillInfo;
    }
    public void OnMoreItemPopup(Inscription inscription)
    {
        rectTransform = GetComponent<RectTransform>();
        image.sprite = inscription.inscripImage;
        text.text = inscription.inscripName;
        Inventory inventory = FindObjectOfType<PlayerManager>().inventory;
        if (inventory.inscriptions.IndexOf(inscription) != -1)
        {
            int num = inventory._InscriptionSlots[inventory.inscriptions.IndexOf(inscription)].inscriptionSkill.InscripNum;
            Num.text = num.ToString() + "/" + inscription.inscripNum[inscription.inscripNum.Length - 1].ToString();
        }
        else
        {
            Num.text = "0/" + inscription.inscripNum[inscription.inscripNum.Length - 1].ToString();
        }
        List<InscriptionInfo> obj = new List<InscriptionInfo>();
        float posY = 0;
        for (int i = 0; i < inscription.inscripInfo.Length; i++)
        {
            obj.Add(Instantiate(PopupInfo.gameObject, transform).GetComponent<InscriptionInfo>());
            obj[i].textNum.text = inscription.inscripNum[i].ToString() + "  <sprite=1>  ";
            obj[i].textInfo.text = inscription.inscripInfo[i].ToString();
            
            //자동 줄바꿈까지 생각한 코드로!
            obj[i].textInfo.ForceMeshUpdate(true);
            Debug.Log(obj[i].textInfo.textInfo.lineCount);
            int num = obj[i].textInfo.textInfo.lineCount - 2;
            if(num < 0)
            {
                num = 0;
            }
            if (i > 0)
            {
                posY += 34.5f;
            }
            obj[i].gameObject.GetComponent<RectTransform>().anchoredPosition += Vector2.down * posY;
            posY += num * 11.5f;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, posY + 101);
        }
    }
}
