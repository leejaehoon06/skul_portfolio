using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterAttack : MonoBehaviour
{
    [SerializeField]
    MonsterAttackInfo[] _AttackInfos;
    public MonsterAttackInfo[] AttackInfos { get { return _AttackInfos; } set { _AttackInfos = value; } }
    List<GameObject> AttackColl = new List<GameObject>();
    List<float> AttackCoolTime = new List<float>();
    Monster monster;
    Animator anim;
    Rigidbody2D rigid;
    void Start()
    {
        monster = GetComponent<Monster>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        for(int i = 0; i < _AttackInfos.Length; i++)
        {
            if (_AttackInfos[i].AttackColl != null)
            {
                AttackColl.Add(Instantiate(_AttackInfos[i].AttackColl, transform));
            }
            AttackCoolTime.Add(0);
        }
    }
    private void Update()
    {
        if (monster.IsAction == true)
        {
            for (int i = 0; i < AttackColl.Count; i++)
            {
                if ((AttackColl[i].GetComponent<MonsterAttackColl>() != null && 
                    AttackColl[i].GetComponent<MonsterAttackColl>().targetIn == true && AttackCoolTime[i] <= 0)
                    || (AttackColl[i].GetComponent<MonsterAttackColl>() == null && monster.targetParent != null 
                    && AttackCoolTime[i] <= 0))
                {
                    anim.Play(_AttackInfos[i].name);
                    rigid.velocity = Vector2.zero;
                    StartCoroutine(monster.AnimHitDonAction(_AttackInfos[i].DonActionTime));
                    StartCoroutine(AttackCoolDown(i));
                    break;
                }
            }
        }
        for (int i = 0; i < AttackCoolTime.Count; i++)
        {
            if (AttackCoolTime[i] >= 0)
            {
                AttackCoolTime[i] -= Time.deltaTime;
            }
        }
    }
    void ParentAttackInstan()
    {
        string animName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        for (int i = 0; i < _AttackInfos.Length; i++)
        {
            if (_AttackInfos[i].name == animName)
            {
                GameObject obj = Instantiate(_AttackInfos[i].DamageObj, transform);
                obj.GetComponent<Damage>().MonsterDamageNum = _AttackInfos[i].DamageNum;
                obj.GetComponent<Damage>().monster = monster;
            }
        }
    }
    void AttackInstan()
    {
        string animName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        for (int i = 0; i < _AttackInfos.Length; i++)
        {
            if (_AttackInfos[i].name == animName)
            {
                GameObject obj = Instantiate(_AttackInfos[i].DamageObj, transform.position, transform.rotation);
                obj.GetComponent<Damage>().MonsterDamageNum = _AttackInfos[i].DamageNum;
                obj.GetComponent<Damage>().monster = monster;
            }
        }
    }
    void TargetAttackInstan()
    {
        string animName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        for (int i = 0; i < _AttackInfos.Length; i++)
        {
            if (_AttackInfos[i].name == animName)
            {
                if (monster.targetParent != null)
                {
                    GameObject obj = Instantiate(_AttackInfos[i].DamageObj, monster.target.transform.position, transform.rotation);
                    obj.GetComponent<Damage>().MonsterDamageNum = _AttackInfos[i].DamageNum;
                    obj.GetComponent<Damage>().monster = monster;
                    if(obj.GetComponent<FindGround>() != null)
                    {
                        obj.GetComponent<FindGround>().boxColl = GetComponent<BoxCollider2D>();
                    }
                }
            }
        }
    }
    IEnumerator AttackCoolDown(int index)
    {
        AttackCoolTime[index] = 100f;
        yield return new WaitForEndOfFrame();
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            yield return new WaitForEndOfFrame();
        }
        AttackCoolTime[index] = _AttackInfos[index].CoolTime;
    }
}
