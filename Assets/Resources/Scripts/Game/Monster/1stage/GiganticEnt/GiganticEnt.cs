using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticEnt : MonoBehaviour
{
    const int rangeAttack = 1;
    [SerializeField]
    Transform RangeAttackTrans;
    [SerializeField]
    GameObject ParentObj;
    MonsterAttackInfo attack;
    Monster monster;

    private void Start()
    {
        monster = GetComponent<Monster>();
        attack = GetComponent<NormalMonsterAttack>().AttackInfos[rangeAttack];
    }
    void RangeInstan()
    {
        GameObject parentObj = Instantiate(ParentObj, RangeAttackTrans.position, Quaternion.identity);
        GiganticEntRangeParent parentObjScript = parentObj.GetComponent<GiganticEntRangeParent>();
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(attack.DamageObj, parentObj.transform);
            obj.GetComponent<Damage>().MonsterDamageNum = attack.DamageNum;
            obj.GetComponent<Damage>().monster = monster;
            GiganticEntRange objScript = obj.GetComponent<GiganticEntRange>();
            objScript.RotZ = i * 60f;
            parentObjScript.giganticEntRanges.Add(objScript);
        }
    }
}
