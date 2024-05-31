using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    근접,
    원거리,
    찍기
}
public class HitObj : MonoBehaviour
{    
    BoxCollider2D coll;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }
    public Transform MeeleeHitObjectInstan(Collider2D collision, GameObject HitObj)
    {
        if(coll == null) return null;
        BoxCollider2D MonsterColl = collision.gameObject.GetComponent<BoxCollider2D>();
        float PosX = 0;
        float PosY = 0;
        if (collision.gameObject.transform.rotation.eulerAngles.y == 0)
        {
            PosX = Random.Range(collision.gameObject.transform.position.x + MonsterColl.size.x
                * -0.5f * collision.gameObject.transform.localScale.x
                    + MonsterColl.offset.x * collision.gameObject.transform.localScale.x,
                    transform.position.x + coll.size.x * 0.5f * transform.localScale.x
                    + coll.offset.x * transform.localScale.x);
        }
        else
        {
            PosX = Random.Range(transform.position.x + coll.size.x * -0.5f * transform.localScale.x
                    + coll.offset.x * transform.localScale.x * -1,
                    collision.gameObject.transform.position.x + MonsterColl.size.x
                * 0.5f * collision.gameObject.transform.localScale.x
                    + MonsterColl.offset.x * collision.gameObject.transform.localScale.x * -1);
        }
        if(transform.position.y + coll.size.y * 0.5f * transform.localScale.y + coll.offset.y * transform.localScale.y >=
            collision.gameObject.transform.position.y + MonsterColl.size.y * 0.5f * collision.gameObject.transform.localScale.y 
            + MonsterColl.offset.y * collision.gameObject.transform.localScale.y)
        {
            if(transform.position.y + coll.size.y * -0.5f * transform.localScale.y + coll.offset.y * transform.localScale.y >=
            collision.gameObject.transform.position.y + MonsterColl.size.y * -0.5f * collision.gameObject.transform.localScale.y
            + MonsterColl.offset.y * collision.gameObject.transform.localScale.y)
            {
                PosY = Random.Range(transform.position.y + coll.size.y * -0.5f * transform.localScale.y 
                    + coll.offset.y * transform.localScale.y,
                    collision.gameObject.transform.position.y + MonsterColl.size.y * 0.5f * collision.gameObject.transform.localScale.y
            + MonsterColl.offset.y * collision.gameObject.transform.localScale.y);
            }
            else
            {
                PosY = Random.Range(collision.gameObject.transform.position.y + MonsterColl.size.y * -0.5f * collision.gameObject.transform.localScale.y
            + MonsterColl.offset.y * collision.gameObject.transform.localScale.y, 
                    collision.gameObject.transform.position.y + MonsterColl.size.y * 0.5f * collision.gameObject.transform.localScale.y
                    + MonsterColl.offset.y * collision.gameObject.transform.localScale.y);
            }
        }
        else
        {
            if (transform.position.y + coll.size.y * -0.5f * transform.localScale.y + coll.offset.y * transform.localScale.y >=
            collision.gameObject.transform.position.y + MonsterColl.size.y * -0.5f * collision.gameObject.transform.localScale.y
            + MonsterColl.offset.y * collision.gameObject.transform.localScale.y)
            {
                PosY = Random.Range(transform.position.y + coll.size.y * -0.5f * transform.localScale.y
                    + coll.offset.y * transform.localScale.y,
                    transform.position.y + coll.size.y * 0.5f * transform.localScale.y + coll.offset.y * transform.localScale.y);
            }
            else
            {
                PosY = Random.Range(collision.gameObject.transform.position.y + MonsterColl.size.y * -0.5f * collision.gameObject.transform.localScale.y
                    + MonsterColl.offset.y * collision.gameObject.transform.localScale.y,
                    transform.position.y + coll.size.y * 0.5f * transform.localScale.y + coll.offset.y * transform.localScale.y);
            }
        }
        Vector2 HitObjPos = new Vector2(Mathf.Clamp(PosX
            , collision.gameObject.transform.position.x + MonsterColl.size.x
                * -0.5f * collision.gameObject.transform.localScale.x
                    + MonsterColl.offset.x * collision.gameObject.transform.localScale.x,
            collision.gameObject.transform.position.x + MonsterColl.size.x
                * 0.5f * collision.gameObject.transform.localScale.x
                    + MonsterColl.offset.x * collision.gameObject.transform.localScale.x),
            Mathf.Clamp(PosY,
            collision.gameObject.transform.position.y + MonsterColl.size.y
                * -0.5f * collision.gameObject.transform.localScale.y
                    + MonsterColl.offset.y * collision.gameObject.transform.localScale.y,
            collision.gameObject.transform.position.y + MonsterColl.size.y
                * 0.5f * collision.gameObject.transform.localScale.y
                    + MonsterColl.offset.y * collision.gameObject.transform.localScale.y));
        GameObject obj = Instantiate(HitObj, HitObjPos, Quaternion.Euler(new Vector3(0,  
            transform.eulerAngles.y, Random.Range(-45f, 45))));
        return obj.transform;
    }
    public Transform HitObejctInstan(Collider2D collision, GameObject HitObj)
    {
        if (coll == null) return null;
        BoxCollider2D MonsterColl = collision.GetComponent<BoxCollider2D>();
        Vector2 HitObjPos = new Vector2(Mathf.Clamp(coll.bounds.center.x
            , collision.transform.position.x - MonsterColl.size.x / 2 + MonsterColl.offset.x,
            collision.transform.position.x + MonsterColl.size.x / 2 - MonsterColl.offset.x),
            Mathf.Clamp(coll.bounds.center.y,
            collision.transform.position.y - MonsterColl.size.y / 2 + MonsterColl.offset.y,
            collision.transform.position.y + MonsterColl.size.y / 2 - MonsterColl.offset.y));
        GameObject obj = Instantiate(HitObj, HitObjPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        return obj.transform;
    }
}
