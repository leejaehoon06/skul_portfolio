using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSubBar : MonoBehaviour
{
    [SerializeField]
    float FillNum;
    [SerializeField]
    GameObject WaveObj;
    public SubBar subBar { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            subBar.CurBarNum += FillNum;
            if(subBar.CurBarNum >= subBar.MaxBarNum)
            {
                if(WaveObj != null)
                {
                    GameObject obj = Instantiate(WaveObj, subBar.gameObject.transform.position,
                        subBar.gameObject.transform.rotation);
                    if (obj.GetComponentInChildren<Damage>() != null)
                    {
                        Damage playerDamage = obj.GetComponentInChildren<Damage>();
                        playerDamage.player = subBar.GetComponent<Player>();
                    }
                }
                subBar.CurBarNum = 0;
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.GetComponent<SubBar>() != null && 
                collision.gameObject.GetComponent<Player>()._MyImport.unitCode == PlayerCode.Water)
            {
                StopCoroutine("SpritsLakeBuff");
                StartCoroutine("SpritsLakeBuff");
            }
        }
    }
    IEnumerator SpritsLakeBuff()
    {
        while (true)
        {
            subBar.CurBarNum += (FillNum * Time.deltaTime);
            if (subBar.CurBarNum >= subBar.MaxBarNum)
            {
                if (WaveObj != null)
                {
                    GameObject obj = Instantiate(WaveObj, subBar.gameObject.transform.position,
                        subBar.gameObject.transform.rotation);
                    if (obj.GetComponentInChildren<Damage>() != null)
                    {
                        Damage playerDamage = obj.GetComponentInChildren<Damage>();
                        playerDamage.player = subBar.GetComponent<Player>();
                    }
                }
                subBar.CurBarNum = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopCoroutine("SpritsLakeBuff");
        }
    }
}
