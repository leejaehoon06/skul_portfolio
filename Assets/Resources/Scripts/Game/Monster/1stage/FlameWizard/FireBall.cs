using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    float Speed;
    public GameObject target { get; set; }
    Vector3 dir;
    Collider2D coll;
    void Start()
    {
        BoxCollider2D targetBoxColl = target.GetComponent<BoxCollider2D>();
        dir = new Vector3(target.transform.position.x, 
            target.transform.position.y + targetBoxColl.size.y * -0.5f +
            targetBoxColl.offset.y + 1f, 
            0) - transform.position;
        dir.Normalize();
        coll = GetComponent<Collider2D>();
    }
    float timer = 0f;
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            if(coll.enabled == false)
            {
                coll.enabled = true;
            }
            transform.position += dir * Speed * Time.fixedDeltaTime;
            transform.Rotate(0, 0, -3 * Time.fixedDeltaTime * 180f);
        }
    }
}
