using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjItem : Interaction
{
    [SerializeField] Item item;
    public Item _item { get { return item; } }
    UnityEngine.UI.Image KeydownImage;
    [SerializeField]
    bool storeItem;
    SpriteRenderer sprite;
    private void Update()
    {
        if (KeydownImage != null)
        {
            KeydownImage.fillAmount = KeydownTimer;
        }
    }
    void Start()
    {
        if (interactionUI != null)
        {
            UIObj = Instantiate(interactionUI, UIManager.current.PopupTransChild[0].transform.parent);
            UIObj.GetComponent<Popup>().item = this;
            UIObj.GetComponent<Popup>().PopupOn();
            UIObj.SetActive(false);
        }
        sprite = GetComponent<SpriteRenderer>();
        sprite.material = Instantiate(sprite.material);
    }
    public override void Interact()
    {
        Inventory inventory = FindObjectOfType<PlayerManager>().inventory;
        if(inventory.items.Count < 9)
        {
            inventory.AddItem(item);
        }
        else
        {

        }
        if(transform.parent != null)
        {
            if(transform.parent.GetComponent<ItemReward>() != null)
            {
                transform.parent.GetComponent<ItemReward>().ItemSelect(item);
            }
        }
        Destroy(gameObject);
    }
    public override void KeydownInteract()
    {
        Destroy(gameObject);
    }
    public override void PlayerIn()
    {
        if (UIObj.activeSelf == false)
        {
            base.PlayerIn();
            if (storeItem == false)
            {
                sprite.material.SetFloat("_HsvSaturation", 1f);
            }
            Vector3 playerPos = FindObjectOfType<PlayerManager>().playerObjs[0].transform.position;
            if (transform.position.x < playerPos.x && transform.position.y > playerPos.y)
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[0].transform.position;
            }
            else if (transform.position.x >= playerPos.x && transform.position.y > playerPos.y)
            {
                UIObj.transform.position = UIManager.current.PopupTransChild[1].transform.position;
            }
            else if (transform.position.x < playerPos.x && transform.position.y <= playerPos.y)
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
        if (storeItem == false)
        {
            sprite.material.SetFloat("_HsvSaturation", 0.25f);
        }
    }
}
