using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHit : Damage
{
    [SerializeField]
    GameObject destroyEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterDamaged(collision.GetComponent<Monster>(), collision, false);
        }
    }
    private void OnDisable()
    {
        Instantiate(destroyEffect, transform.position, transform.parent.rotation);
    }
}
