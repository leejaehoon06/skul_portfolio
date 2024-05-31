using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FlameWizard : MonoBehaviour
{
    [SerializeField]
    Transform attackTrans;
    Monster monster;
    MonsterAttackInfo attack;
    Animator anim;
    private void Start()
    {
        attack = GetComponent<NormalMonsterAttack>().AttackInfos[0];
        monster = GetComponent<Monster>();
        anim = GetComponent<Animator>();
    }
    IEnumerator Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(attack.DamageObj, attackTrans.position, attackTrans.rotation);
            obj.GetComponent<Damage>().MonsterDamageNum = attack.DamageNum;
            obj.GetComponent<Damage>().monster = monster;
            obj.GetComponent<FireBall>().target = monster.target;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("attackend");
    }
}
