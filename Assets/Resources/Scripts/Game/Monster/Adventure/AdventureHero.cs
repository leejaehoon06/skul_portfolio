using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureHero : Adventure
{
    [SerializeField]
    BossAttackInfo[] MeleeAttack;
    [SerializeField]
    BossAttackInfo[] RangeAttack;
    [SerializeField]
    BossAttackInfo CastingAttack;
    [SerializeField]
    float DashSpeed;
    [SerializeField]
    Transform EnergyBallTrans;
    [SerializeField]
    GameObject[] AttackObjs;
    List<float> MeleeAttackCoolTime = new List<float>();
    List<float> RangeAttackCoolTime = new List<float>();
    Monster monster;
    Animator anim;
    Rigidbody2D rigid;
    void Start()
    {
        monster = GetComponent<Monster>();
        monster.targetParent = FindObjectOfType<PlayerManager>().gameObject;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        for(int i = 0; i < MeleeAttack.Length; i++)
        {
            MeleeAttackCoolTime.Add(0);
        }
        for(int i=0;i<RangeAttack.Length; i++)
        {
            RangeAttackCoolTime.Add(0);
        }
    }

    void Update()
    {
        for (int i = 0; i < MeleeAttackCoolTime.Count; i++)
        {
            MeleeAttackCoolTime[i] -= Time.deltaTime;
        }
        for (int i = 0; i < RangeAttackCoolTime.Count; i++)
        {
            RangeAttackCoolTime[i] -= Time.deltaTime;
        }
        if(CastingAttack.HpCondition >= monster.monsterStat.CurHp / monster.monsterStat.MaxHp)
        {
            anim.Play(CastingAttack.name);
            StartCoroutine(Casting());
            StartCoroutine(monster.AnimHitDonAction(3f));
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Intro") == false 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Casting") == false 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Groggy") == false && monster.IsAction)
        {
            int rand = Random.Range(0, MeleeAttack.Length + RangeAttack.Length + 1);
            if (rand < MeleeAttack.Length && MeleeAttackCoolTime[rand] <= 0f)
            {
                if (Mathf.Abs(transform.position.x - monster.target.transform.position.x) >= 4f)
                {
                    anim.ResetTrigger("dashEnd");
                    anim.Play("Dash");
                    StartCoroutine(Dash(rand));
                    StartCoroutine(monster.AnimHitDonAction(0));
                }
                else
                {
                    anim.Play(MeleeAttack[rand].name);
                    StartCoroutine(monster.AnimHitDonAction(Random.Range(1f, 3f)));
                }
            }
            else if (rand >= MeleeAttack.Length && rand < MeleeAttack.Length + RangeAttack.Length && RangeAttackCoolTime[rand - MeleeAttack.Length] <= 0f)
            {
                if (Mathf.Abs(transform.position.x - monster.target.transform.position.x) <= 1f)
                {
                    anim.Play("BackDash");
                    StartCoroutine(BackDash(rand - MeleeAttack.Length));
                    StartCoroutine(monster.AnimHitDonAction(0));
                }
                else
                {
                    anim.Play(RangeAttack[rand - MeleeAttack.Length].name);
                    StartCoroutine(monster.AnimHitDonAction(Random.Range(1f, 3f)));
                }
            }
            else if (rand >= MeleeAttack.Length + RangeAttack.Length)
            {
                anim.Play("Potion");
                StartCoroutine(GetComponent<Adventure>().PotionEnd());
                StartCoroutine(monster.AnimHitDonAction(Random.Range(1f, 3f)));
            }
        }
    }
    IEnumerator Dash(int index)
    {
        float targetX = monster.target.transform.position.x;
        if (targetX < transform.position.x)
        {
            while(targetX < transform.position.x)
            {
                rigid.velocity = new Vector2(-DashSpeed, rigid.velocity.y);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (targetX >= transform.position.x)
            {
                rigid.velocity = new Vector2(DashSpeed, rigid.velocity.y);
                yield return new WaitForFixedUpdate();
            }
        }
        rigid.velocity = Vector2.zero;
        anim.SetTrigger("dashEnd");
        anim.Play(MeleeAttack[index].name);
        StartCoroutine(monster.AnimHitDonAction(Random.Range(1f, 3f)));
    }
    IEnumerator BackDash(int index)
    {
        float targetX = monster.target.transform.position.x;
        float timer = 24f / 60f;
        if (targetX < transform.position.x)
        {
            while (timer >= 0f)
            {
                rigid.velocity = new Vector2(DashSpeed, rigid.velocity.y);
                timer -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (timer >= 0f)
            {
                rigid.velocity = new Vector2(-DashSpeed, rigid.velocity.y);
                timer -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        rigid.velocity = Vector2.zero;
        anim.Play(RangeAttack[index].name);
        StartCoroutine(monster.AnimHitDonAction(Random.Range(1f, 3f)));
    }
    void Attack1Instan()
    {
        GameObject obj = Instantiate(AttackObjs[0], transform.position, transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = MeleeAttack[0].DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
    void Attack2Instan()
    {
        GameObject obj = Instantiate(AttackObjs[1], transform.position, transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = MeleeAttack[0].DamageNum;
        obj.GetComponent<Damage>().monster = monster;

    }
    void Attack3Instan()
    {
        GameObject obj = Instantiate(AttackObjs[2], transform.position, transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = MeleeAttack[0].DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
    void ExplosionInstan()
    {
        GameObject obj = Instantiate(MeleeAttack[1].DamageObj, transform.position, transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = MeleeAttack[1].DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
    [SerializeField]
    GameObject EnergyBallEnableEffect;
    void EnergyBallInstan()
    {
        GameObject obj = Instantiate(EnergyBallEnableEffect, EnergyBallTrans.position, transform.rotation);
        StartCoroutine(EnergyBallAttackInstan(obj.transform));
    }
    IEnumerator EnergyBallAttackInstan(Transform tras)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject obj = Instantiate(RangeAttack[0].DamageObj, tras.position, RangeAttack[0].DamageObj.transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = RangeAttack[0].DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
    void CastingAttackInstan()
    {
        GameObject obj = Instantiate(CastingAttack.DamageObj, EnergyBallTrans.position, CastingAttack.DamageObj.transform.rotation);
        obj.GetComponent<Damage>().MonsterDamageNum = CastingAttack.DamageNum;
        obj.GetComponent<Damage>().monster = monster;
    }
}
