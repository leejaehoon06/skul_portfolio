using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemReward : Interaction
{
    Inventory inventory;
    [SerializeField]
    Rare rare;
    [SerializeField]
    ItemBundle[] ItemBundles;
    ObjItem[] DropItems;
    List<Item> Items = new List<Item>();
    private void FixedUpdate()
    {
        if (UIObj != null)
        {
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
        }
    }
    private void Start()
    {
        if (interactionUI != null)
        {
            UIObj = Instantiate(interactionUI, UIManager.current.InteractionObj.transform);
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
            UIObj.SetActive(false);
        }
        inventory = FindObjectOfType<PlayerManager>().inventory;//모든 아이템으로 변경
        DropItems = FindObjectsOfType<ObjItem>();
        
        if (rare == Rare.노말)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand = UnityEngine.Random.Range(0, ItemBundles[0].items.Length);
                int index = 0;
                while (inventory.items.IndexOf(ItemBundles[0].items[rand]) != -1 
                    || Array.IndexOf(DropItems, ItemBundles[0].items[rand]) != -1
                    || Items.IndexOf(ItemBundles[0].items[rand]) != -1)
                {
                    rand = UnityEngine.Random.Range(0, ItemBundles[0].items.Length);
                    index++;
                    if (index == 100)
                    {
                        break;
                    }
                }
                if(index == 100)
                {
                    break;
                }
                else
                {
                    Items.Add(ItemBundles[0].items[rand]);
                }
            }
        }
        else if (rare == Rare.레어)
        {

        }
        else if (rare == Rare.유니크)
        {

        }
        else
        {

        }
    }
    List<GameObject> itemObjs = new List<GameObject>();
    public override void Interact()
    {
        base.Interact();
        GetComponent<Animator>().SetTrigger("Activate");
        for (int i = 0; i < Items.Count; i++)
        {
            itemObjs.Add(Instantiate(Items[i].ItemObj, transform));
            itemObjs[i].transform.position = transform.position + Vector3.down * 0.5f;
            itemObjs[i].GetComponent<ObjDoMove>().StartCoroutine(
            itemObjs[i].GetComponent<ObjDoMove>().ObjMove(itemObjs[i], i - 1));
        }
    }
    public void ItemSelect(Item item)
    {
        for(int i = 0; i < itemObjs.Count; i++)
        {
            if(item != Items[i])
            {
                Destroy(itemObjs[i]);
            }
        }
    }
}
