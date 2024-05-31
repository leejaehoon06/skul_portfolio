using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    float MoveSpeed;
    private void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
    }
    private void FixedUpdate()
    {
        if (rigid != null)
        {
            if (transform.rotation.eulerAngles.y == 0)
            {
                rigid.velocity = new Vector2(MoveSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(-MoveSpeed, rigid.velocity.y);
            }
        }
        else
        {
            transform.Translate(new Vector2(MoveSpeed * Time.fixedDeltaTime, 0));
        }
    }
}
