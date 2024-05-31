using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Frame2 : MonoBehaviour
{
    [SerializeField]
    Image image1;
    [SerializeField]
    Text text1;
    [SerializeField]
    Image image2;
    [SerializeField]
    Text text2;
    [SerializeField]
    Transform _trans1;
    public Transform trans1 { get { return _trans1; } }
    [SerializeField]
    Transform _trans2;
    public Transform trans2 { get { return _trans2; } }
    public void SkulFrameOn(ObjSkul skul)   
    {
        image1.sprite = skul.skills[0].skillImage;
        text1.text = skul.skills[0].skillName;
        image2.sprite = skul.skills[1].skillImage;
        text2.text = skul.skills[1].skillName;
    }
    public void ItemFrameOn(Item item)
    {
        image1.sprite = item.itemInscriptions[0].inscripImage;
        text1.text = item.itemInscriptions[0].inscripName;
        image2.sprite = item.itemInscriptions[1].inscripImage;
        text2.text = item.itemInscriptions[1].inscripName;
    }
}
