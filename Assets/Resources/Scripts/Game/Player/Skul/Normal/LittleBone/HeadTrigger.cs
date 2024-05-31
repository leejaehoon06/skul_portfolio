using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    Head head;
    void Start()
    {
        head = GetComponentInParent<Head>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        head.GetHead(collision);
    }
}
