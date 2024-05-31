using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpritsLake : MonoBehaviour
{
    BoxCollider2D boxColl;
    bool ClampIndex1 = false;
    bool ClampIndex2 = false;
    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
        Vector3 scale = transform.localScale;
        ScaleRayGround();
        int rotate = 1;
        if(transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        Vector3 center = new Vector3(transform.position.x
                    + boxColl.offset.x * rotate * transform.localScale.x,
                    transform.position.y
                    + boxColl.offset.y * transform.localScale.y, 0);
        Vector3 extents = new Vector3(boxColl.bounds.extents.x * (transform.localScale.x / scale.x), 
            boxColl.bounds.extents.y * (transform.localScale.y / scale.y), 0);
        ScaleRayWall(center ,extents);
    }

    void ScaleRayWall(Vector3 center, Vector3 extents)
    {
        float scope = 0.1f;
        int layerMask = LayerMask.GetMask("Ground");
        int index1 = 0;
        int index2 = 0;

        Ray2D ray1 = new Ray2D();
        ray1 = new Ray2D(center + new Vector3(extents.x * (-0.01f * index1)
            , -extents.y + 0.1f, 0), Vector2.left);
        Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x * scope, ray1.direction.y), Color.yellow, 1f);

        RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);

        Ray2D ray2 = new Ray2D();
        ray2 = new Ray2D(center + new Vector3(extents.x * (0.01f * index2)
            , -extents.y + 0.1f, 0), Vector2.right);

        Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x * scope, ray2.direction.y), Color.blue, 1f);

        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
        while (hit1.collider == null)
        {
            index1++;
            ray1 = new Ray2D(center + new Vector3(extents.x * (-0.01f * index1)
            , -extents.y + 0.1f, 0), Vector2.left);
            Debug.DrawRay(ray1.origin, new Vector2(ray1.direction.x * scope, ray1.direction.y), Color.yellow, 1f);

            hit1 = Physics2D.Raycast(ray1.origin, ray1.direction, scope, layerMask);
            if (ClampIndex1 == false)
            {
                if (index1 >= 200)
                {
                    break;
                }
            }
            else
            {
                if (index1 >= 100)
                {
                    break;
                }
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
            ray2 = new Ray2D(center + new Vector3(extents.x * (0.01f * index2)
            , -extents.y + 0.1f, 0), Vector2.right);

            Debug.DrawRay(ray2.origin, new Vector2(ray2.direction.x * -scope, ray2.direction.y), Color.blue, 1f);

            hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, scope, layerMask);
            if (ClampIndex2 == false)
            {
                if (index2 >= 200 - minusIndex2)
                {
                    break;
                }
            }
            else
            {
                if (index2 >= 100)
                {
                    break;
                }
            }
        }

        float minusIndex1 = 200 - index2;
        if (index1 < minusIndex1)
        {
            minusIndex1 = index1;
        }
        transform.position = new Vector2(((center +
            new Vector3(extents.x * (-0.01f * minusIndex1),
            -extents.y + 0.1f, 0)).x
            + (center + new Vector3(extents.x * (0.01f * index2)
            , -extents.y + 0.1f, 0)).x) / 2, transform.position.y);
        transform.localScale = new Vector2((extents.x * (0.01f * index2)
            - extents.x * (-0.01f * minusIndex1))
            / (extents.x * 2) * transform.localScale.x, transform.localScale.y);
    }
    void ScaleRayGround()
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
            ClampIndex1 = true;
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
            ClampIndex2 = true;
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
        transform.localScale = new Vector2((boxColl.bounds.extents.x * (0.01f * index2)
            - boxColl.bounds.extents.x * (-0.01f * minusIndex1))
            / (boxColl.bounds.extents.x * 2) * transform.localScale.x, transform.localScale.y);
    }
}
