using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Frame1 : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Text text;
    [SerializeField]
    Transform _trans;
    public Transform trans { get { return _trans; } }
    public void FrameOn(ObjSkul skul)
    {
        image.sprite = skul.skills[0].skillImage;
        text.text = skul.skills[0].skillName;
    }
}
