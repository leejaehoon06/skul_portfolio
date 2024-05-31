using System.Collections.Generic;
using UnityEngine;

public class MonsterFindTarget : MonoBehaviour
{
    Monster monster;
    public List<GameObject> PlayerObjs { get; set; } = new List<GameObject>();
    [SerializeField]
    bool DonTargetReset = false;
    
    void Start()
    {
        monster = GetComponentInParent<Monster>();
    }
    private void Update()
    {
        if (DonTargetReset == false)
        {
            if (PlayerObjs.Count == 0)
            {
                monster.targetParent = null;
            }
            else
            {
                if (PlayerObjs.IndexOf(monster.target) == -1 && monster.IsAction == true)
                {
                    monster.targetParent = null;
                }
                if (monster.targetParent == null)
                {
                    monster.targetParent = FindTarget();
                }
            }
        }
        else
        {
            if (monster.targetParent == null)
            {
                if (PlayerObjs.Count > 0)
                {
                    monster.targetParent = FindTarget();
                }
            }
        }
    }
    private GameObject FindTarget()
    {
        GameObject target = null;
        target = PlayerObjs[0];
        float shortDis = Vector3.Distance(gameObject.transform.position, PlayerObjs[0].transform.position);
        foreach (GameObject found in PlayerObjs)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                target = found;
                shortDis = Distance;
            }
        }
        return target.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && PlayerObjs.IndexOf(collision.gameObject) == -1)
        {
            PlayerObjs.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && PlayerObjs.IndexOf(collision.gameObject) != -1)
        {
            PlayerObjs.Remove(collision.gameObject);
        }
    }
}
