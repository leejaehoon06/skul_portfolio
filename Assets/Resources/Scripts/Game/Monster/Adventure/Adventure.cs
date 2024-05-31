using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure : MonoBehaviour
{
    [SerializeField]
    GameObject HpFrame;
    Monster monster;
    Animator anim;
    int castingNum;
    void Start()
    {
        monster = GetComponent<Monster>();
        anim = GetComponent<Animator>();
        GameObject obj = Instantiate(HpFrame, UIManager.current.MonsterHpBarObjs.transform);
        obj.GetComponent<AdventureHpBar>().monster = GetComponent<Monster>();
    }

    void Update()
    {
        
    }
    public IEnumerator Casting()
    {
        float GroggyHp = monster.monsterStat.CurHp / monster.monsterStat.MaxHp - 0.2f;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("CastingAttack") != false)
        {
            if (monster.monsterStat.CurHp / monster.monsterStat.MaxHp <= GroggyHp)
            {
                anim.SetTrigger("groggy");
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void Potion()
    {
        GetComponent<MonsterHeal>().heal(2);
    }
    public IEnumerator PotionEnd()
    {
        float rand = Random.Range(1f, 3f);
        while(rand <= 0)
        {
            rand -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        anim.SetTrigger("loopend");
    }
}
