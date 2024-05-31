using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnHitWall : MonoBehaviour
{
    [SerializeField]
    bool TurnOn;
    BoxCollider2D boxColl;
    CircleCollider2D circleColl;
    Player player = null;
    int rotate = 1;
    bool IsGround = false;
    MonsterRayFindTarget[] monsters;
    private void Start()
    {
        if (GetComponent<Player>() != null)
        {
            player = GetComponent<Player>();
        }
        monsters = null;
        if (transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        if (GetComponent<BoxCollider2D>() != null)
        {
            boxColl = GetComponent<BoxCollider2D>();
            if (player == null)
            {
                OnEnableCheckWall();
            }
        }
        else
        {
            circleColl = GetComponent<CircleCollider2D>();
        }
    }
    private void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        if (TurnOn == false)
        {
            if (player != null)
            {
                player.CheckingWall = CheckHitWall();
                GetComponent<Animator>().SetBool("fall", !CheckHitGround());
            }
            else if (GetComponent<MonsterMove>() == null)
            {
                if (CheckHitWall())
                {
                    TurnObj();
                }
                else if (CheckHitGround() == false && IsGround == true)
                {
                    TurnObj();
                }
            }
            else
            {
                if (CheckHitWall())
                {
                    if (GetComponent<MonsterOnlyMove>() != null || GetComponent<Monster>().targetParent == null)
                    {
                        StartCoroutine(GetComponent<MonsterMove>().MoveStopTurn());
                    }
                }
                else if (CheckHitGround() == false && IsGround == true)
                {
                    if (GetComponent<MonsterOnlyMove>() != null || GetComponent<Monster>().targetParent == null)
                    {
                        StartCoroutine(GetComponent<MonsterMove>().MoveStopTurn());
                    }
                }
            }
        }
    }
    void OnEnableCheckWall()
    {
        float scope = boxColl.size.x;
        int layerMask = LayerMask.GetMask("Ground");
        // 플레이어의 머리, 가슴, 발 총 3군데에서 ray를 쏜다.
        List<Ray2D> rays2D = new List<Ray2D>();
        if (GetComponent<Monster>() == null)
        {
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , boxColl.bounds.extents.y, 0), Vector2.right * rotate));
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , 0, 0), Vector2.right * rotate));
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , boxColl.bounds.extents.y * -0.9f, 0), Vector2.right * rotate));
        }
        else
        {
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            + 0.7f * rotate, boxColl.bounds.extents.y, 0), Vector2.right * rotate));
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            + 0.7f * rotate, 0, 0), Vector2.right * rotate));
            rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            + 0.7f * rotate, boxColl.bounds.extents.y * -0.9f, 0), Vector2.right * rotate));
        }
        // 디버깅을 위해 ray를 화면에 그린다.
        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x * scope, ray.direction.y), Color.red, 1f);
        }

        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null)
            {
                TurnObj();
            }
        }
    }
    public bool CheckHitWall()
    {
        // 움직임에 대한 로컬 벡터를 월드 벡터로 변환해준다.
        // scope로 ray 충돌을 확인할 범위를 지정할 수 있다.
        float scope = 0.05f;
        int layerMask = LayerMask.GetMask("Ground");
        // 플레이어의 머리, 가슴, 발 총 3군데에서 ray를 쏜다.
        List<Ray2D> rays2D = new List<Ray2D>();
        if (boxColl != null)
        {
            if (GetComponent<Monster>() == null)
            {
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                , boxColl.bounds.extents.y, 0), Vector2.right * rotate));
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                , 0, 0), Vector2.right * rotate));
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                , boxColl.bounds.extents.y * -0.9f, 0), Vector2.right * rotate));
            }
            else
            {
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                + 0.7f * rotate, boxColl.bounds.extents.y, 0), Vector2.right * rotate));
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                + 0.7f * rotate, 0, 0), Vector2.right * rotate));
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
                + 0.7f * rotate, boxColl.bounds.extents.y * -0.9f, 0), Vector2.right * rotate));
            }
        }
        else
        {
            rays2D.Add(new Ray2D(transform.position + Vector3.up * circleColl.radius * 0.1f, Vector2.right * rotate));
            rays2D.Add(new Ray2D(transform.position + Vector3.up * circleColl.radius * 0.5f, Vector2.right * rotate));
            rays2D.Add(new Ray2D(transform.position + Vector3.up * circleColl.radius, Vector2.right * rotate));
        }

        // 디버깅을 위해 ray를 화면에 그린다.
        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x * scope, ray.direction.y), Color.red);
        }

        // ray와 벽의 충돌을 확인한다.
        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<PlatformEffector2D>().useOneWay == false)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckHitGround()
    {
        float scope = 0.05f;
        int layerMask = LayerMask.GetMask("Ground");

        List<Ray2D> rays2D = new List<Ray2D>();
        if (boxColl != null)
        {
            if (player != null)
            {
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , -boxColl.bounds.extents.y, 0), Vector2.down));
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
            , -boxColl.bounds.extents.y, 0), Vector2.down));
            }
            else if (GetComponent<Monster>() == null)
            {
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * 1.1f * rotate
            , -boxColl.bounds.extents.y, 0), Vector2.down));
            }
            else
            {
                rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
            + 0.7f * rotate, -boxColl.bounds.extents.y, 0), Vector2.down));
            }
        }
        else
        {
            rays2D.Add(new Ray2D(transform.position + Vector3.right * circleColl.radius * -0.5f, Vector2.down));
            rays2D.Add(new Ray2D(transform.position + Vector3.right * circleColl.radius * 0.5f, Vector2.down));
        }

        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x, ray.direction.y * scope), Color.red);
        }
        if (player != null)
        {
            foreach (Ray2D ray in rays2D)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
                if (hit.collider != null)
                {
                    if (GetComponent<Rigidbody2D>().velocity.y <= 0)
                    {
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump") == false)
                        {
                            player.playerManager.JumpValue = 0;
                        }
                        if (monsters == null)
                        {
                            monsters = FindObjectsOfType<MonsterRayFindTarget>();
                            for (int i = 0; i < monsters.Length; i++)
                            {
                                monsters[i].RayFind();
                            }
                        }
                        return true;
                    }
                }
            }
            monsters = null;
            return false;
        }
        else
        {
            foreach (Ray2D ray in rays2D)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
                if (hit.collider == null)
                {
                    return false;
                }
            }
            IsGround = true;
            return true;
        }
    }
    public RaycastHit2D HitGround()
    {
        float scope = 0.1f;
        int layerMask = LayerMask.GetMask("Ground");

        List<Ray2D> rays2D = new List<Ray2D>();
        rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , -boxColl.bounds.extents.y, 0), Vector2.down));
        rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
            , -boxColl.bounds.extents.y, 0), Vector2.down));

        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x, ray.direction.y * scope), Color.red);
        }
        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null)
            {
                if (GetComponent<Rigidbody2D>().velocity.y <= 0)
                {
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump") == false)
                    {
                        player.playerManager.JumpValue = 0;
                    }
                    return hit;
                }
            }
        }
        return new RaycastHit2D();
    }
    public void TurnObj()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (GetComponentInChildren<Damage>() != null)
        {
            GetComponentInChildren<Damage>().GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<Damage>().GetComponent<Collider2D>().enabled = true;
        }
    }
}
