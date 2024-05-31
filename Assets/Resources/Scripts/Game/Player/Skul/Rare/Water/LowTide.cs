using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowTide : MonoBehaviour
{
    List<GameObject> monsterObjs = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            monsterObjs.Add(collision.gameObject);
            monsterObjs[monsterObjs.Count - 1].transform.parent = transform;
        }
    }
    void DestroyBefore()
    {
        for(int i=0;i<monsterObjs.Count; i++)
        {
            monsterObjs[i].transform.parent = null;
        }
    }
}
