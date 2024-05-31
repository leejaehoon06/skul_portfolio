using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AttackType
{
    기본공격,
    스킬,
    대쉬,
    교대스킬,
    몬스터
}
public enum KnockbackType
{
    일방향,
    양방향,
    역방향
}
public class Damage : MonoBehaviour
{
    [SerializeField]
    DamageType damageType;
    [SerializeField]
    AttackType attackType;
    [SerializeField]
    HitType hitType;
    [SerializeField]
    KnockbackType knockbackType;
    [SerializeField]
    bool DonHitKnockback;
    [SerializeField]
    float _PlayerDamage;
    [SerializeField]
    float _MonsterDamage;
    [SerializeField]
    float PlayerKnockbackX;
    [SerializeField]
    float PlayerKnockbackY;
    [SerializeField]
    float MonsterKnockbackX;
    [SerializeField]
    float MonsterKnockbackY;
    public float PlayerDamageNum { get; set; } = 0;
    public float MonsterDamageNum { get; set; } = 0;
    public Player player {  get; set; }
    public Monster monster { get; set; }
    [SerializeField]
    GameObject HitObj;
    [SerializeField]
    GameObject CriHitObj;
    HitObj damageObj;
    Collider2D coll;
    
    private void Start()
    {
        if(_PlayerDamage > 0)
        {
            PlayerDamageNum = _PlayerDamage;
        }
        if(_MonsterDamage > 0)
        {
            MonsterDamageNum = _MonsterDamage;
        }
        damageObj = GetComponent<HitObj>();
        coll = GetComponent<Collider2D>();
        if (coll.enabled == true)
        {
            coll.enabled = false;
            coll.enabled = true;
        }
    }
    public Monster MonsterDamaged(Monster monster, Collider2D collision, bool CriInstan)
    {
        bool CriChance = false;
        float FinalDamage = 0;
        if (PlayerDamageNum > 0)
        {
            if(monster.GetComponent<MonsterRayFindTarget>() == null 
                && monster.targetParent == null && player != null)
            {
                monster.HitTarget(player.playerManager.gameObject);
            }
            CriChance = player.playerManager.playerStat.CriC >= Random.Range(1, 101);
            FinalDamage = MonsterStat.Damaged(monster.monsterStat, player.playerManager.playerStat, PlayerDamageNum, damageType, CriChance);
            if (FinalDamage < 1)
            {
                FinalDamage = 1;
            }
            monster.monsterStat.CurHp -= FinalDamage;
        }
        if (monster.runningCoroutine != null)
        {
            monster.StopCoroutine(monster.runningCoroutine);
        }
        monster.runningCoroutine = monster.StartCoroutine(monster.HpBarOn());
        Transform trans = monster.transform;
        if(damageObj != null)
        {
            if (CriInstan)
            {
                if (CriChance == false)
                {
                    if (hitType == HitType.근접)
                    {
                        trans = damageObj.MeeleeHitObjectInstan(collision, HitObj);
                    }
                    else
                    {
                        trans = damageObj.HitObejctInstan(collision, HitObj);
                    }
                }
                else
                {
                    if (hitType == HitType.근접)
                    {
                        trans = damageObj.MeeleeHitObjectInstan(collision, CriHitObj);
                    }
                    else
                    {
                        trans = damageObj.HitObejctInstan(collision, CriHitObj);
                    }
                }
            }
            else
            {
                if (hitType == HitType.근접)
                {
                    trans = damageObj.MeeleeHitObjectInstan(collision, HitObj);
                }
                else
                {
                    trans = damageObj.HitObejctInstan(collision, HitObj);
                }
            }
        }
        else
        {
            trans = transform;
        }
        if (PlayerDamageNum > 0)
        {
            MonsterDamageTextInstan(trans.position, damageType, FinalDamage, CriChance);
        }
        if (monster.monsterStat.CurHp > 0)
        {
            if (knockbackType == KnockbackType.일방향)
            {
                if (transform.rotation.eulerAngles.y == 0)
                {
                    monster.StartCoroutine(monster.HitKnockback(MonsterKnockbackX, MonsterKnockbackY));
                }
                else
                {
                    monster.StartCoroutine(monster.HitKnockback(-MonsterKnockbackX, MonsterKnockbackY));
                }
            }
            else if (knockbackType == KnockbackType.양방향)
            {
                int TargetRotate = 1;
                if (monster.transform.rotation.eulerAngles.y == 0)
                {
                    TargetRotate = 1;
                }
                else
                {
                    TargetRotate = -1;
                }
                int rotate = 1;
                if (transform.rotation.eulerAngles.y == 0)
                {
                    rotate = 1;
                }
                else
                {
                    rotate = -1;
                }
                float TargetPosX = monster.transform.position.x + monster.GetComponent<BoxCollider2D>().offset.x
                    * TargetRotate * monster.transform.localScale.x;
                float PosX = transform.position.x + GetComponent<BoxCollider2D>().offset.x * rotate * transform.localScale.x;
                Debug.DrawRay(new Vector2(TargetPosX, monster.transform.position.y),
                    new Vector2(PosX, transform.position.y), Color.red, 1f);
                if (TargetPosX < PosX)
                {
                    monster.StartCoroutine(monster.HitKnockback(-MonsterKnockbackX, MonsterKnockbackY));
                }
                else if (TargetPosX >= PosX)
                {
                    monster.StartCoroutine(monster.HitKnockback(MonsterKnockbackX, MonsterKnockbackY));
                }
            }
            if (monster.GetComponent<HitOn>() != null)
            {
                monster.StartCoroutine(monster.GetComponent<HitOn>().HitMaterailOn());
            }
            if (DonHitKnockback == false)
            {
                foreach (AnimatorControllerParameter param in monster.GetComponent<Animator>().parameters)
                {
                    if (param.name == "hit")
                    {
                        monster.GetComponent<Animator>().SetTrigger("hit");
                        monster.AnimHitDonAction(0);
                    }
                    else if (param.name == "hitmotion")
                    {
                        if (monster.GetComponent<Animator>().GetFloat("hitmotion") == 0)
                        {
                            monster.GetComponent<Animator>().SetFloat("hitmotion", 1f);
                        }
                        else
                        {
                            monster.GetComponent<Animator>().SetFloat("hitmotion", 0f);
                        }
                    }
                }
            }
        }
        else
        {
            MonsterDestroy(monster);
            monster.DestroyOn();
            return null;
        }
        return monster;
    }
    public virtual void MonsterDestroy(Monster monster)
    {

    }
    public Player PlayerDamaged(Player player, Collider2D collision)
    {
        if (player.DashTime == false)
        {
            float FinalDamage = 0f;
            if (MonsterDamageNum > 0)
            {
                FinalDamage = PlayerStat.Damaged(player.playerManager.playerStat, monster.monsterStat, MonsterDamageNum);
                if (FinalDamage < 1)
                {
                    FinalDamage = 1;
                }
                player.playerManager.playerStat.CurHp -= FinalDamage;
                UIManager.current.StartCoroutine(UIManager.current.HitScreen());
            }
            Transform trans = player.transform;
            if (knockbackType == KnockbackType.일방향)
            {
                if (transform.rotation.eulerAngles.y == 0)
                {
                    player.StartCoroutine(player.HitKnockback(PlayerKnockbackX, PlayerKnockbackY));
                }
                else
                {
                    player.StartCoroutine(player.HitKnockback(-PlayerKnockbackX, PlayerKnockbackY));
                }
            }
            else if (knockbackType == KnockbackType.양방향)
            {
                int TargetRotate = 1;
                if (player.transform.rotation.eulerAngles.y == 0)
                {
                    TargetRotate = 1;
                }
                else
                {
                    TargetRotate = -1;
                }
                int rotate = 1;
                if (transform.rotation.eulerAngles.y == 0)
                {
                    rotate = 1;
                }
                else
                {
                    rotate = -1;
                }
                float TargetPosX = player.transform.position.x + player.GetComponent<BoxCollider2D>().offset.x
                    * TargetRotate * player.transform.localScale.x;
                float PosX = transform.position.x + GetComponent<BoxCollider2D>().offset.x * rotate * transform.localScale.x;
                if (TargetPosX < PosX)
                {
                    player.StartCoroutine(player.HitKnockback(-PlayerKnockbackX, PlayerKnockbackY));
                }
                else if (TargetPosX >= PosX)
                {
                    player.StartCoroutine(player.HitKnockback(PlayerKnockbackX, PlayerKnockbackY));
                }
            }
            if (player.GetComponent<HitOn>() != null)
            {
                player.StartCoroutine(player.GetComponent<HitOn>().HitMaterailOn());
            }
            if (damageObj != null)
            {
                if (hitType == HitType.근접)
                {
                    trans = damageObj.MeeleeHitObjectInstan(collision, HitObj);
                }
                else
                {
                    trans = damageObj.HitObejctInstan(collision, HitObj);
                }
            }
            else
            {
                trans = transform;
            }
            if (MonsterDamageNum > 0)
            {
                PlayerDamageTextInstan(trans.position, FinalDamage);
            }
        }
        return player;
    }
    public void HitObjInstan(Vector3 pos, Quaternion rot)
    {
        Instantiate(HitObj, pos, rot);
    }
    void MonsterDamageTextInstan(Vector2 pos, DamageType damageType, float DamageNum, bool CriChance)
    {
        GameObject obj = Instantiate(UIManager.current.DamageText.gameObject, UIManager.current.DamageTextObjects.transform);
        UnityEngine.UI.Text text = obj.GetComponent<UnityEngine.UI.Text>();
        if(damageType == DamageType.Ad)
        {
            if (CriChance == false)
            {
                text.color = new Color32(255, 188, 0, 255);
            }
            else
            {
                text.color = new Color32(241, 255, 0, 255);
                text.fontSize = 15;
            }
        }
        else if(damageType == DamageType.Ap)
        {
            if (CriChance == false)
            {
                text.color = new Color32(85, 227, 255, 255);
            }
            else
            {
                text.color = new Color32(85, 255, 237, 255);
                text.fontSize = 15;
            }
        }
        DamageNum = Mathf.Floor(DamageNum);
        
        text.text = DamageNum.ToString("0");
        obj.GetComponent<DamageText>().SetWorldPoint(pos);
    }
    void PlayerDamageTextInstan(Vector2 pos, float DamageNum)
    {
        GameObject obj = Instantiate(UIManager.current.DamageText.gameObject, UIManager.current.DamageTextObjects.transform);
        UnityEngine.UI.Text text = obj.GetComponent<UnityEngine.UI.Text>();
        text.color = new Color32(255, 64, 42, 255);
        text.fontSize = 15;
        DamageNum = Mathf.Floor(DamageNum);

        text.text = DamageNum.ToString("0");
        obj.GetComponent<DamageText>().SetWorldPoint(pos);
    }
}
