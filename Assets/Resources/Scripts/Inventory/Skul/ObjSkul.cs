using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjSkul : Interaction
{
    [SerializeField] Skul skul;
    public Skul _skul {  get { return skul; } }
    UnityEngine.UI.Image KeydownImage;
    public List<Skill> skills { get; set; } = new List<Skill>();
    public Player player { get; set; }
    SpriteRenderer sprite;
    public int bone { get; set; }
    private void Update()
    {
        if (KeydownImage != null)
        {
            KeydownImage.fillAmount = KeydownTimer;
        }
    }
    void Start()
    {
        if (skul.skulRare == Rare.노말)
        {
            bone = 5;
        }
        else if (skul.skulRare == Rare.레어)
        {
            bone = 11;
        }
        else if (skul.skulRare == Rare.유니크)
        {
            bone = 23;
        }
        else
        {
            bone = 44;
        }
        sprite = GetComponent<SpriteRenderer>();
        sprite.material = Instantiate(sprite.material);
        if (player == null)
        {//추후에 확정 스킬들은 따로 코드 작성
            if (skul.skulRare <= Rare.레어)
            {
                int rand = UnityEngine.Random.Range(0, skul.skulSkills.Length);
                skills.Add(skul.skulSkills[rand]);
            }
            else
            {
                for(int i = 0; i < 2; i++)
                {
                    int rand = UnityEngine.Random.Range(0, skul.skulSkills.Length);
                    if (i == 1) {
                        while (skul.skulSkills[rand] == skills[0])
                        {
                            rand = UnityEngine.Random.Range(0, skul.skulSkills.Length);
                        }
                    }
                    skills.Add(skul.skulSkills[rand]);
                }
            }
        }
        else
        {
            skills = player.skills;
        }
        if (interactionUI != null)
        {
            UIObj = Instantiate(interactionUI, UIManager.current.PopupTransChild[0].transform.parent);
            UIObj.GetComponent<Popup>().skul = this;
            UIObj.GetComponent<Popup>().PopupOn();
            UIObj.SetActive(false);
        }
    }
    public override void Interact()
    {
        Inventory inventory = FindObjectOfType<PlayerManager>().inventory;
        inventory.AddSkul(skul, this);
        Destroy(gameObject);
    }
    public override void KeydownInteract()
    {
        GameManager.current.bone += bone;
        Destroy(gameObject);
    }
    public override void PlayerIn()
    {
        if (UIObj.activeSelf == false)
        {
            base.PlayerIn();
            Vector3 playerPos = FindObjectOfType<PlayerManager>().playerObjs[0].transform.position;
            Vector3 Pos = transform.position + Vector3.down;
            if (Pos.x < playerPos.x && Pos.y > playerPos.y)
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[0].transform.position;
            }
            else if (Pos.x >= playerPos.x && Pos.y > playerPos.y)
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[1].transform.position;
            }
            else if (Pos.x < playerPos.x && Pos.y <= playerPos.y)
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[2].transform.position;
            }
            else
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[3].transform.position;
            }
        }
    }
    public override void PlayerOut()
    {
        base.PlayerOut();
    }
}
