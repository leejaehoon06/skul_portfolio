using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStop : MonoBehaviour
{
    BoxCollider2D boxColl;
    CircleCollider2D circleColl;
    void Start()
    {
        if(GetComponent<BoxCollider2D>() != null)
        {
            boxColl = GetComponent<BoxCollider2D>();
        }
        else
        {
            circleColl = GetComponent<CircleCollider2D>();
        }
    }
    private void FixedUpdate()
    {
        CheckFallStop();
    }
    void CheckFallStop()
    {
        float scope = 1f;
        int layerMask = LayerMask.GetMask("Ground");

        List<Ray2D> rays2D = new List<Ray2D>();
        if (boxColl != null)
        {
            rays2D.Add(new Ray2D(transform.position + Vector3.right * boxColl.size.x * -0.4f, Vector2.up));
            rays2D.Add(new Ray2D(transform.position, Vector2.up));
            rays2D.Add(new Ray2D(transform.position + Vector3.right * boxColl.size.x * 0.4f, Vector2.up));
        }
        else
        {
            rays2D.Add(new Ray2D(transform.position + Vector3.right * circleColl.radius * -0.4f, Vector2.up));
            rays2D.Add(new Ray2D(transform.position, Vector2.up));
            rays2D.Add(new Ray2D(transform.position + Vector3.right * circleColl.radius * 0.4f, Vector2.up));
        }

        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x, ray.direction.y * scope), Color.red);
        }

        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null)
            {
                transform.position = new Vector2(transform.position.x, hit.point.y);
            }
        }
    }
}
