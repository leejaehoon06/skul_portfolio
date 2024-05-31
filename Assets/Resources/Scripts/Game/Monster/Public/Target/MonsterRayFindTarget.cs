using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterRayFindTarget : MonoBehaviour
{
    Monster monster;
    BoxCollider2D boxColl;
    void Start()
    {
        monster = GetComponent<Monster>();
        boxColl = GetComponent<BoxCollider2D>();
        RayFind();
    }
    public void RayFind()
    {
        int layerMask = LayerMask.GetMask("Player") + LayerMask.GetMask("PlatformPlayer") + LayerMask.GetMask("Ground");
        float scope = 100f;
        List<Ray2D> rays2D = new List<Ray2D>();
        rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(0
            , -boxColl.bounds.extents.y + 0.3f, 0), Vector2.right));
        rays2D.Add(new Ray2D(boxColl.bounds.center + new Vector3(0
            , -boxColl.bounds.extents.y + 0.3f, 0), Vector2.left));
        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x * scope, ray.direction.y), Color.red);
        }
        List<GameObject> objs = new List<GameObject>();
        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                objs.Add(hit.collider.gameObject);
            }
        }
        if (objs.Count > 0)
        {
            GameObject target = objs[0];
            float shortDis = Vector3.Distance(gameObject.transform.position, objs[0].transform.position);
            foreach (GameObject found in objs)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    target = found;
                    shortDis = Distance;
                }
            }
            monster.targetParent = target.transform.parent.gameObject;
        }
        else
        {
            if(monster.targetParent != null)
            {
                StartCoroutine(GetComponent<MonsterMove>().MoveStopTurn());
            }
            monster.targetParent = null;
        }
    }
}
