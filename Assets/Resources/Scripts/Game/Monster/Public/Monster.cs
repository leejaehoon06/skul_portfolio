using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Animator anim;
    List<string> animParamNames = new List<string>();
    Rigidbody2D rigid;
    [SerializeField]
    GameObject DeathEffect;
    public MonsterStat monsterStat { get; set; }
    [SerializeField]
    MonsterCode monsterCode;
    [SerializeField]
    GameObject Hpbar;
    GameObject HpBarObj;
    [SerializeField]
    bool _SuperArmor;
    public bool SuperArmor { get { return _SuperArmor; } set { _SuperArmor = value; } }
    public GameObject targetParent { get; set; }
    public GameObject target { get; private set; }
    public bool IsAction { get; set; } = true;
    public Coroutine runningCoroutine { get; set; } = null;
    private void Start()
    {
        GetComponent<SpriteRenderer>().material = Instantiate(GetComponent<SpriteRenderer>().material);
        monsterStat = MonsterStat.SetMonsterStats(monsterCode);
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            animParamNames.Add(param.name);
        }
        if (Hpbar != null && Hpbar.GetComponent<MonsterHpBar>() != null)
        {
            HpBarObj = Instantiate(Hpbar, UIManager.current.MonsterHpBarObjs.transform);
            MonsterHpBar hpBar = HpBarObj.GetComponent<MonsterHpBar>();
            hpBar.monster = this;
        }
    }
    private void Update()
    {
        if (targetParent != null)
        {
            target = targetParent.GetComponent<PlayerManager>().playerObjs[0];
        }
        else
        {
            target = null;
        }
        if (animParamNames.IndexOf("hit") != -1)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && SuperArmor == false)
            {
                IsAction = false;
            }
            else
            {
                IsAction = true;
            }
        }
    }
    public IEnumerator HpBarOn()
    {
        if (HpBarObj != null)
        {
            float timer = 0f;
            HpBarObj.SetActive(true);
            while (timer <= 2f)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            HpBarObj.SetActive(false);
        }
    }
    public void DestroyOn()
    {
        if (DeathEffect != null)
        {
            Instantiate(DeathEffect, new Vector2(transform.position.x, transform.position.y
                + GetComponent<BoxCollider2D>().size.y * 0f * transform.localScale.y
                + GetComponent<BoxCollider2D>().offset.y * transform.localScale.y)
                , transform.rotation);
            Destroy(HpBarObj);
            Destroy(gameObject);
        }
    }
    public IEnumerator RandHitDonAction(float Num)
    {
        float Rand = Random.Range(Num * 0.8f, Num * 1.2f);
        float Timer = 0;
        while (Timer <= Rand)
        {
            IsAction = false;
            Timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        IsAction = true;
    }
    public IEnumerator AnimHitDonAction(float Num)
    {
        IsAction = false;
        yield return new WaitForEndOfFrame();
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            IsAction = false;
            yield return new WaitForEndOfFrame();
        }
        IsAction = false;
        StartCoroutine(RandHitDonAction(Num));
    }
    public IEnumerator HitKnockback(float KnockbackX, float KnockbackY)
    {
        float timer = 0.1f;
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        while (timer >= 0)
        {
            rigid.AddForce(new Vector2(KnockbackX, KnockbackY));
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void HitTarget(GameObject target)
    {
        targetParent = target;
    }
}
