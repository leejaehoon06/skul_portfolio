using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHitDamage : Damage
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterDamaged(collision.GetComponent<Monster>(), collision, true);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer"))
        {
            PlayerDamaged(collision.GetComponent<Player>(), collision);
        }
    }
}
