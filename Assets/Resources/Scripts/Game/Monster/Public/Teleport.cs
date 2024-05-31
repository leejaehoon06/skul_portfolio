using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeleportType
{
    Wizard,
    Assassin
}
public class Teleport : MonoBehaviour
{
    [SerializeField]
    GameObject TeleportEffect;
    [SerializeField]
    TeleportType type;
    SpriteRenderer sprite;
    Monster monster;
    Rigidbody2D rigid;
    BoxCollider2D boxColl;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        monster = GetComponent<Monster>();
        rigid = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(type == TeleportType.Wizard)
        {
            if(GetComponentInChildren<MonsterFindTarget>().PlayerObjs.Count == 0 && monster.IsAction == true)
            {
                GetComponent<Animator>().Play("Teleport");
            }
        }
    }
    void TeleportStart()
    {
        Instantiate(TeleportEffect, transform.position, transform.rotation);
        sprite.color = new Color(1, 1, 1, 0);
        rigid.simulated = false;
        boxColl.enabled = false;
    }
    void TeleportEnd()
    {
        Instantiate(TeleportEffect, transform.position, transform.rotation);
        sprite.color = new Color(1, 1, 1, 1);
        rigid.simulated = true;
        boxColl.enabled = true;
    }
    void WizardTeleport()
    {
        if (monster.target != null)
        {
            Vector3 beforeTrans = transform.position;
            float PosX = UnityEngine.Random.Range(-5f, 5f);
            float PosY = UnityEngine.Random.Range(-5f, 5f);
            transform.position = new Vector2(monster.target.transform.position.x + PosX,
                monster.target.transform.position.y + PosY);
            int layerMask = LayerMask.GetMask("Ground");
            float scope1 = PosY + 3f;
            int rotate = 1;
            if(transform.rotation.eulerAngles.y == 0)
            {
                rotate = 1;
            }
            else
            {
                rotate = -1;
            }
            Ray2D ray1 = new Ray2D(new Vector2(transform.position.x + boxColl.offset.x * rotate, 
                transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.down);
            Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope1), Color.yellow, 1f);
            RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope1, layerMask);

            float scope2 = boxColl.bounds.extents.y * 2;
            Ray2D ray2 = new Ray2D(new Vector2(transform.position.x + boxColl.size.x * -0.5f
                + boxColl.offset.x * rotate,
                transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.up);
            Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope2), Color.yellow, 1f);
            RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope2, layerMask);
            Ray2D ray3 = new Ray2D(new Vector2(transform.position.x + boxColl.size.x * 0.5f
                + boxColl.offset.x * rotate,
                transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.up);
            Debug.DrawRay(ray3.origin, new Vector2(ray3.direction.x, ray3.direction.y * scope2), Color.yellow, 1f);
            RaycastHit2D hit3 = Physics2D.Raycast(ray3.origin, ray3.direction, scope2, layerMask);
            int i = 0;
            while (hit1.collider == null || (hit1.collider != null && (hit2.collider != null || hit3.collider != null)))
            {
                i++;
                if (i >= 100)
                {
                    transform.position = beforeTrans;
                    return;
                }
                PosX = UnityEngine.Random.Range(-3f, 3f);
                PosY = UnityEngine.Random.Range(-3f, 3f);
                transform.position = new Vector2(monster.target.transform.position.x + PosX,
                    monster.target.transform.position.y + PosY);

                scope1 = PosY + 3f;
                ray1 = new Ray2D(new Vector2(transform.position.x + boxColl.offset.x * rotate,
                transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.down);
                Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope1), Color.yellow, 1f);
                hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope1, layerMask);

                ray2 = new Ray2D(new Vector2(transform.position.x + boxColl.size.x * -0.5f
                    + boxColl.offset.x * rotate,
                    transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.up);
                Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope2), Color.yellow, 1f);
                hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope2, layerMask);
                ray3 = new Ray2D(new Vector2(transform.position.x + boxColl.size.x * 0.5f
                    + boxColl.offset.x * rotate,
                    transform.position.y + boxColl.size.y * -0.5f + boxColl.offset.y), Vector2.up);
                Debug.DrawRay(ray3.origin, new Vector2(ray3.direction.x, ray3.direction.y * scope2), Color.yellow, 1f);
                hit3 = Physics2D.Raycast(ray3.origin, ray3.direction, scope2, layerMask);
            }
            transform.position = hit1.point;
        }
    }
}
