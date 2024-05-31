using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{
    BoxCollider2D boxColl;
    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
        RayHitGround();
        RayHitWall();
    }
    void RayHitGround()
    {
        float scope = 0.1f;
        int layerMask = LayerMask.GetMask("Ground");
        int index1 = 0;
        int index2 = 0;

        Ray2D ray1 = new Ray2D();
        ray1 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (-0.01f * index1)
            , -boxColl.bounds.extents.y, 0), Vector2.down);
        Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope), Color.yellow, 1f);

        RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);

        Ray2D ray2 = new Ray2D();
        ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y, 0), Vector2.down);

        Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope), Color.blue, 1f);

        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
        while (hit1.collider != null)
        {
            index1++;
            ray1 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (-0.01f * index1)
            , -boxColl.bounds.extents.y, 0), Vector2.down);
            Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope), Color.yellow, 1f);
            hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);
            if (index1 >= 200)
            {
                break;
            }
        }
        if (hit1.collider == null)
        {
            index1--;
            ray1 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (-0.01f * index1)
            , -boxColl.bounds.extents.y, 0), Vector2.down);
            Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x, ray1.direction.y * scope), Color.yellow, 1f);
            hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);
        }
        float minusIndex2 = index1;
        if (minusIndex2 > 100)
        {
            minusIndex2 = 100;
        }
        while (hit2.collider != null)
        {
            index2++;
            ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y, 0), Vector2.down);
            Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope), Color.blue, 1f);
            hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
            if (index2 >= 200 - minusIndex2)
            {
                break;
            }
        }
        if (hit2.collider == null)
        {
            index2--;
            ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y, 0), Vector2.down);
            Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x, ray2.direction.y * scope), Color.blue, 1f);
            hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
        }
        float minusIndex1 = 200 - index2;
        if (index1 < minusIndex1)
        {
            minusIndex1 = index1;
        }
        transform.position = new Vector2(((boxColl.bounds.center +
            new Vector3(boxColl.bounds.extents.x * (-0.01f * minusIndex1),
            -boxColl.bounds.extents.y, 0)).x
            + (boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y, 0)).x) / 2, transform.position.y);
    }
    void RayHitWall()
    {
        float scope = 0.1f;
        int layerMask = LayerMask.GetMask("Ground");
        int index1 = 0;
        int index2 = 0;

        Ray2D ray1 = new Ray2D();
        ray1 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (-0.01f * index1)
            , -boxColl.bounds.extents.y + 0.1f, 0), Vector2.left);
        Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x * scope, ray1.direction.y), Color.yellow, 1f);

        RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);

        Ray2D ray2 = new Ray2D();
        ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y + 0.1f, 0), Vector2.right);

        Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x * scope, ray2.direction.y), Color.blue, 1f);

        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
        while (hit1.collider == null)
        {
            index1++;
            ray1 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (-0.01f * index1)
            , -boxColl.bounds.extents.y + 0.1f, 0), Vector2.left);
            Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x * scope, ray1.direction.y), Color.yellow, 1f);

            hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);
            if (index1 >= 200)
            {
                break;
            }
        }
        float minusIndex2 = index1;
        if (minusIndex2 > 100)
        {
            minusIndex2 = 100;
        }
        while (hit2.collider == null)
        {
            index2++;
            ray2 = new Ray2D(boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y + 0.1f, 0), Vector2.right);

            Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x * -scope, ray2.direction.y), Color.blue, 1f);

            hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
            if (index2 >= 200 - minusIndex2)
            {
                break;
            }
        }

        float minusIndex1 = 200 - index2;
        if (index1 < minusIndex1)
        {
            minusIndex1 = index1;
        }
        transform.position = new Vector2(((boxColl.bounds.center +
            new Vector3(boxColl.bounds.extents.x * (-0.01f * minusIndex1),
            -boxColl.bounds.extents.y + 0.1f, 0)).x
            + (boxColl.bounds.center + new Vector3(boxColl.bounds.extents.x * (0.01f * index2)
            , -boxColl.bounds.extents.y + 0.1f, 0)).x) / 2, transform.position.y);
    }
}
