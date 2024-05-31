using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackColl : MonoBehaviour
{
    public bool targetIn { get; set; }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetIn = false;
        }
    }
}
