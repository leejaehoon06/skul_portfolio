using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacialWizard : MonoBehaviour
{
    Monster monster;
    MonsterAttackInfo attack;
    private void Start()
    {
        attack = GetComponent<NormalMonsterAttack>().AttackInfos[0];
        monster = GetComponent<Monster>();
    }
    void Attack()
    {
        GameObject obj = Instantiate(attack.DamageObj, monster.target.transform.position + Vector3.up * 5
            , attack.DamageObj.transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = attack.DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
}
