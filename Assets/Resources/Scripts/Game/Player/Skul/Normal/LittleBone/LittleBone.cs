using System.Collections;
using UnityEngine;

public class LittleBone : MonoBehaviour
{
    [SerializeField]
    Attack[] attacks;
    [SerializeField]
    Attack jumpAttack;
    [SerializeField]
    Switch switchSkill;
    [SerializeField]
    Transform HeadMuzzle;
    GameObject HeadObj;
    Player player;
    Animator anim;
    Rigidbody2D rigid;
    private void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Attack1()
    {
        GameObject obj = Instantiate(attacks[0].attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, attacks[0].attackDamage);
    }
    void Attack2()
    {
        GameObject obj = Instantiate(attacks[1].attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, attacks[1].attackDamage);
    }
    void JumpAttack()
    {
        GameObject obj = Instantiate(jumpAttack.attackObj, transform.position, transform.rotation);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, jumpAttack.attackDamage);
    }
    GameObject SwitchObj;
    void Switch()
    {
        SwitchObj = Instantiate(switchSkill.switchObj, transform);
        SwitchObj = player.playerManager.PlayerDamageObjManage(player, SwitchObj, switchSkill.switchDamage);
    }
    float SwitchValue = 125f;
    IEnumerator SwitchMove()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            while (anim.GetCurrentAnimatorStateInfo(0).IsName("Switch"))
            {
                if (player.CheckingWall == false)
                {
                    rigid.AddForce(new Vector2(SwitchValue, rigid.velocity.y));
                }
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (anim.GetCurrentAnimatorStateInfo(0).IsName("Switch"))
            {
                if (player.CheckingWall == false)
                {
                    rigid.AddForce(new Vector2(-SwitchValue, rigid.velocity.y));
                }
                yield return new WaitForFixedUpdate();
            }
        }
        Destroy(SwitchObj);
    }
    void AnimSkill1()
    {
        HeadObj = Instantiate(player.skills[0].skillObj, HeadMuzzle.position, transform.rotation);
        HeadObj.GetComponent<Head>().player = player;
        HeadObj = player.playerManager.PlayerDamageObjManage(player, HeadObj, player.skills[0].skillDamage);
        StartCoroutine("Skill1Stop");
        if (anim.GetBool("fall"))
        {
            player.Skill1Able = false;
            StartCoroutine("Skill1Check");
        }
    }
    IEnumerator Skill1Stop()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("SkullThrowing"))
        {
            player.Skill1Able = false;
            yield return new WaitForEndOfFrame();
        }
        player.Skill1Able = true;
    }
    IEnumerator Skill1Check()
    {
        while (anim.GetBool("fall"))
        {
            player.Skill1Able = false;
            yield return new WaitForEndOfFrame();
        }
        player.Skill1Able = true;
    }
    float RayValue = 1;
    void AnimSkill2()
    {
        Instantiate(player.skills[1].skillObj, transform.position, transform.rotation);
        Vector2 pos = HeadObj.transform.position;
        Ray2D ray = new Ray2D(pos, Vector2.down);
        int layerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, RayValue, layerMask);
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = HeadObj.transform.position;
        }
        Instantiate(player.skills[1].skillObj, transform.position, transform.rotation);
        player.Skill1Timer = 0f;
        StartCoroutine(Skill2Check());
    }
    IEnumerator Skill2Check()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Rebone"))
        {
            player.Skill1Able = false;
            yield return new WaitForEndOfFrame();
        }
        player.Skill1Able = true;
    }
}
