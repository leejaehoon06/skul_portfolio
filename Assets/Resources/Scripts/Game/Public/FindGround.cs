using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class FindGround : MonoBehaviour
{
    public BoxCollider2D boxColl { get; set; } = null;
    int rotate = 1;
    void Start()
    {
        if(transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        if(boxColl == null)
        {
            boxColl = GetComponent<BoxCollider2D>();
        }
        RayHitGround(100f);
    }
    public bool RayHitGround(float scope)
    {
        Ray2D ray1 = new Ray2D(boxColl.bounds.center + new Vector3(-boxColl.bounds.extents.x * rotate
            , boxColl.bounds.extents.y * -0.9f, 0), Vector2.down);
        int layerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope,layerMask);
        Ray2D ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * rotate
            , boxColl.bounds.extents.y * -0.9f, 0), Vector2.down);
        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
        Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope), Color.gray, 1f);
        Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope), Color.gray, 1f);
        if (hit1.collider != null)
        {
            if(hit2.collider != null)
            {
                float DistanceHit1 = Vector2.Distance(transform.position, hit1.point);
                float DistanceHit2 = Vector2.Distance(transform.position, hit2.point);
                if(DistanceHit1 + 0.1f >= DistanceHit2 && DistanceHit1 - 0.1f <= DistanceHit2)
                {
                    transform.position = new Vector2((hit1.point.x + hit2.point.x) / 2, hit1.point.y - (boxColl.size.y * -0.5f * transform.localScale.y
                    + boxColl.offset.y * transform.localScale.y));
                }
                else if(DistanceHit1 > DistanceHit2)
                {
                    transform.position = new Vector2(hit2.point.x, hit2.point.y - (boxColl.size.y * -0.5f * transform.localScale.y
                    + boxColl.offset.y * transform.localScale.y));
                }
                else if (DistanceHit1 < DistanceHit2)
                {
                    transform.position = new Vector2(hit1.point.x, hit1.point.y - (boxColl.size.y * -0.5f * transform.localScale.y
                    + boxColl.offset.y * transform.localScale.y));
                }
                return true;
            }
            else
            {
                transform.position = hit1.point;
                return true;
            }
        }
        else
        {
            if (hit2.collider != null)
            {
                transform.position = hit2.point;
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
