using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLook : MonoBehaviour
{
    Monster monster;
    [SerializeField]
    bool IsTurnBack;
    private void Start()
    {
         monster = GetComponent<Monster>();
    }
    void LookAtTarget()
    {
        if (monster.targetParent != null && monster.target != null)
        {
            Quaternion rotation = transform.rotation;
            if(transform.position.x <= monster.target.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if(IsTurnBack)
            {
                StartCoroutine(TurnBack(rotation));
            }
        }
    }
    IEnumerator TurnBack(Quaternion rotation)
    {
        while(monster.IsAction == false)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = rotation;
    }
}
