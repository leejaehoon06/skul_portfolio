using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InscriptionSlot : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] Image InscriptionFrame;
    [SerializeField] Image InscriptionImage;
    public Image _inscripImage
    {
        get
        {
            return InscriptionImage;
        }
        set
        {
            InscriptionImage = value;
        }
    }
    [SerializeField] Text InscriptionName;
    [SerializeField] Text InscriptionNum1;
    public Text _inscripNum1
    {
        get
        {
            return InscriptionNum1;
        }
        set
        {
            InscriptionNum1 = value;
        }
    }
    [SerializeField] TextMeshProUGUI InscriptionNum2;
    public TextMeshProUGUI _inscripNum2
    {
        get
        {
            return InscriptionNum2;
        }
        set
        {
            InscriptionNum2 = value;
        }
    }
    public List<bool> InscripColor { get; set; } = new List<bool>();
    Inscription _inscription;
    public InscriptionSkill inscriptionSkill { get; set; }
    public Inscription inscription
    {
        get { return _inscription; }
        set
        {
            _inscription = value;
            SetSlot();
        }
    }
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    void SetSlot()
    {
        if (_inscription != null)
        {
            InscripColor = new List<bool>();
            InscriptionFrame.color = new Color(1, 1, 1, 1);
            InscriptionImage.gameObject.SetActive(true);
            InscriptionName.gameObject.SetActive(true);
            InscriptionNum1.gameObject.SetActive(true);
            InscriptionNum2.gameObject.SetActive(true);
            InscriptionName.text = _inscription.inscripName;
            InscriptionNum1.text = inscriptionSkill.InscripNum.ToString();
            InscriptionNum2.text = "";
            inscriptionSkill.InscripDefaltNum = new List<int>();
            for (int i = 0; i < _inscription.inscripNum.Length; i++)
            {
                inscriptionSkill.InscripDefaltNum.Add(_inscription.inscripNum[i]);
                if (_inscription.inscripNum.Length == 1)
                {
                    if (inscriptionSkill.InscripNum < _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripDeactivedImage;
                        InscripColor.Add(false);
                    }
                    else if (inscriptionSkill.InscripNum >= _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripFullActivedImage;
                        InscripColor.Add(true);
                    }
                }
                else if (i == _inscription.inscripNum.Length - 1 && _inscription.inscripNum.Length > 1)
                {
                    if (inscriptionSkill.InscripNum >= _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripFullActivedImage;
                        InscripColor.Add(true);
                    }
                    else
                    {
                        InscripColor.Add(false);
                    }
                }
                else if (i == 0 && _inscription.inscripNum.Length > 1)
                {
                    if (inscriptionSkill.InscripNum < _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripDeactivedImage;
                        InscripColor.Add(false);
                    }
                    else if (inscriptionSkill.InscripNum >= _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripImage;
                        InscripColor.Add(true);
                    }
                }
                else
                {
                    if (inscriptionSkill.InscripNum >= _inscription.inscripNum[i])
                    {
                        InscriptionImage.sprite = _inscription.inscripImage;
                        InscripColor.Add(true);
                    }
                    else
                    {
                        InscripColor.Add(false);
                    }
                }
            }
            if (InscripColor[InscripColor.Count - 1] == true)
            {
                for (int i = 0; i < _inscription.inscripNum.Length; i++)
                {
                    InscriptionNum1.color = new Color32(251, 213, 61, 255);
                    if (i < InscripColor.Count - 1)
                    {
                        InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                        InscriptionNum2.text += "  <sprite=0>  ";
                        InscripColor[i] = false;
                    }
                    else
                    {
                        InscriptionNum2.text += "<#FFB00B>";
                        InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                        InscriptionNum2.text += "</color>";
                        InscripColor[i] = true;
                    }
                }
            }
            else if (InscripColor[0] == false)
            {
                for (int i = 0; i < _inscription.inscripNum.Length; i++)
                {
                    InscriptionNum1.color = new Color32(156, 129, 96, 255);
                    InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                    if (i < InscripColor.Count - 1)
                    {
                        InscriptionNum2.text += "  <sprite=0>  ";
                    }
                }
            }
            else
            {
                for (int i = 0; i < _inscription.inscripNum.Length; i++)
                {
                    InscriptionNum1.color = new Color32(215, 192, 170, 255);
                    if (i < InscripColor.Count - 1)
                    {
                        if (InscripColor[i + 1] != true)
                        {
                            InscriptionNum2.text += "<#C89664>";
                            InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                            InscriptionNum2.text += "</color>";
                        }
                        else if (InscripColor[i + 1] == true || InscripColor[i] == false)
                        {
                            InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                            InscripColor[i] = false;
                        }
                        InscriptionNum2.text += "  <sprite=0>  ";
                    }
                    else
                    {
                        InscriptionNum2.text += _inscription.inscripNum[i].ToString();
                    }
                }
            }
        }
        else
        {
            InscriptionFrame.color = new Color(1, 1, 1, 0);
            InscriptionImage.gameObject.SetActive(false);
            InscriptionName.gameObject.SetActive(false);
            InscriptionNum1.gameObject.SetActive(false);
            InscriptionNum2.gameObject.SetActive(false);
        }
    }

}
