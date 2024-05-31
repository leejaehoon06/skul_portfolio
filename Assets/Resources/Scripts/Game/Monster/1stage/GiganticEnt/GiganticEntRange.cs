using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticEntRange : Damage
{
    public bool HitPlayer { get; set; }
    public float RotZ { get; set; }
    [SerializeField]
    float speed = 0.1f;
    void FixedUpdate()
    {
        transform.position = Quaternion.Euler(0, 0, RotZ) * Vector3.up * speed + transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer"))
        {
            if (HitPlayer == false)
            {
                GetComponentInParent<GiganticEntRangeParent>().HitRange(collision.gameObject);
                PlayerDamaged(collision.gameObject.GetComponent<Player>(), collision);
                Destroy(gameObject);
            }
        }
    }
}
