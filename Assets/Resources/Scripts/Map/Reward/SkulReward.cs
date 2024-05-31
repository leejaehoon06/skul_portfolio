using System.Collections.Generic;
using UnityEngine;

public class SkulReward : Interaction
{
    Inventory inventory;
    [SerializeField]
    Rare rare;
    [SerializeField]
    SkulBundle[] SkulBundles;
    List<Skul> skuls = new List<Skul>();
    private void Start()
    {
        if (interactionUI != null)
        {
            UIObj = Instantiate(interactionUI, UIManager.current.InteractionObj.transform);
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
            UIObj.SetActive(false);
        }
        inventory = FindObjectOfType<Inventory>();
        if(rare == Rare.노말)
        {

        }
        else if(rare == Rare.레어)
        {
            skuls.Add(SkulBundles[0].skuls[0]);
        }
        else if(rare == Rare.유니크)
        {

        }
        else
        {

        }
    }
    private void FixedUpdate()
    {
        if (UIObj != null)
        {
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
        }
    }
    List<GameObject> skulObjs = new List<GameObject>();
    public override void Interact()
    {
        base.Interact();
        GetComponent<Animator>().SetTrigger("Activate");
        for (int i = 0; i < skuls.Count; i++)
        {
            skulObjs.Add(Instantiate(skuls[0].skulObj, transform));
            skulObjs[i].transform.position = transform.position + Vector3.down * 0.5f;
            skulObjs[i].GetComponent<ObjDoMove>().StartCoroutine(skulObjs[i].
                GetComponent<ObjDoMove>().ObjMove(skulObjs[i]));
        }
    }
    public void SkulSelect(Skul skul)
    {
        for (int i = 0; i < skulObjs.Count; i++)
        {
            if (skul != skuls[i])
            {
                Destroy(skulObjs[i]);
            }
        }
        Destroy(gameObject);
    }
}
