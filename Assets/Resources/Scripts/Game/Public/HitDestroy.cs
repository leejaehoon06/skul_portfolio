using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("Monster") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer"))
        {
            Destroy(gameObject);
        }
    }
}
