using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
    Player player;
    [SerializeField]
    Attack[] attacks;
    [SerializeField]
    Switch switchSkill;
    [SerializeField]
    Transform LowTideTrans;
    Animator anim;
    private void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }
    void Attack1()
    {
        GameObject obj = Instantiate(attacks[0].attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, attacks[0].attackDamage);
        obj.GetComponent<WaterSubBar>().subBar = GetComponent<SubBar>();
    }
    void Attack2_1()
    {
        GameObject obj = Instantiate(attacks[1].attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, attacks[1].attackDamage);
        obj.GetComponent<WaterSubBar>().subBar = GetComponent<SubBar>();
    }
    void Attack2_2()
    {
        GameObject obj = Instantiate(attacks[2].attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, attacks[2].attackDamage);
        obj.GetComponent<WaterSubBar>().subBar = GetComponent<SubBar>();
    }
    void Switch()
    {
        GameObject obj = Instantiate(switchSkill.switchObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, switchSkill.switchDamage);
    }
    IEnumerator EnterTheWater()
    {
        player.playerManager.playerStat.Speed += 50f;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("EnterTheWater") 
            || anim.GetCurrentAnimatorStateInfo(0).IsName("EnterTheWaterLoop"))
        {
            player.DashTime = true;
            yield return new WaitForEndOfFrame();
        }
        player.DashTime = false;
        player.playerManager.playerStat.Speed -= 50f;
    }
    void WaterSpout()
    {
        player.LastInstanSkillObj.GetComponent<FindGround>().boxColl = GetComponent<BoxCollider2D>();
        if(anim.GetBool("fall") == false && player.LastInstanSkillObj.GetComponent<FindGround>().RayHitGround(0.1f) == false)
        {
            player.LastInstanSkillObj.transform.position = GetComponent<TurnHitWall>().HitGround().point;
            player.LastInstanSkillObj.GetComponent<FindGround>().enabled = false;
        }
        else if (anim.GetBool("fall") == false)
        {
            player.LastInstanSkillObj.transform.position = new Vector2(transform.position.x, transform.position.y);
            player.LastInstanSkillObj.GetComponent<FindGround>().enabled = false;
        }
    }
    void LowTide()
    {
        player.LastInstanSkillObj.transform.position = LowTideTrans.position;
    }
    void SpritsLake()
    {
        player.LastInstanSkillObj.GetComponentInChildren<WaterSubBar>().subBar = GetComponent<SubBar>();
        player.LastInstanSkillObj.GetComponent<FindGround>().boxColl = GetComponent<BoxCollider2D>();
        if (anim.GetBool("fall") == false && player.LastInstanSkillObj.GetComponent<FindGround>().RayHitGround(0.1f) == false)
        {
            player.LastInstanSkillObj.transform.position = GetComponent<TurnHitWall>().HitGround().point;
            player.LastInstanSkillObj.GetComponent<FindGround>().enabled = false;
        }
        else if (anim.GetBool("fall") == false)
        {
            player.LastInstanSkillObj.transform.position = new Vector2(transform.position.x, transform.position.y);
            player.LastInstanSkillObj.GetComponent<FindGround>().enabled = false;
        }
        BoxCollider2D skillCollider = player.LastInstanSkillObj.GetComponent<BoxCollider2D>();
        float SizeX = skillCollider.size.x;
        skillCollider.size = new Vector2(skillCollider.size.x * 1.5f, skillCollider.size.y);
        skillCollider.size = new Vector2(SizeX, skillCollider.size.y);
    }
}
