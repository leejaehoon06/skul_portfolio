using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack_Stomp : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    Player player;
    [SerializeField]
    Attack jumpAttack;
    [SerializeField]
    Attack jumpAttack_Land;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    float JumpAttackValueUp = 400;
    float JumpAttackValueDown = 2000;
    float JumpAttackTimer;
    IEnumerator JumpAttack()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        rigid.velocity = Vector2.zero;
        JumpAttackTimer = 15f / 60f / anim.GetFloat("attackspeed");
        while (JumpAttackTimer > 0)
        {
            rigid.AddForce(new Vector2(0, JumpAttackValueUp * JumpAttackTimer / anim.GetFloat("attackspeed")));
            JumpAttackTimer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        player.ClampVelocity = false;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
        {
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, 0, 0), Mathf.Clamp(rigid.velocity.y, -25f, 25f));
            rigid.AddForce(new Vector2(0, -JumpAttackValueDown * JumpAttackTimer));
            JumpAttackTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        player.ClampVelocity = true;
    }
    GameObject JumpAttackObj;
    void JumpAttackStart()
    {
        JumpAttackObj = Instantiate(jumpAttack.attackObj, transform);
        if (GetComponent<SubBar>() != null)
        {
            JumpAttackObj.GetComponent<WaterSubBar>().subBar = GetComponent<SubBar>();
        }
        JumpAttackObj = player.playerManager.PlayerDamageObjManage(player, JumpAttackObj, jumpAttack.attackDamage);
    }
    void JumpAttackLand()
    {
        GameObject obj = Instantiate(jumpAttack_Land.attackObj, transform.position, transform.rotation);
        Destroy(JumpAttackObj, 0.1f);
        if(GetComponent<SubBar>() != null)
        {
            obj.GetComponent<WaterSubBar>().subBar = GetComponent<SubBar>();
        }
        obj.transform.Translate(Vector3.right * 0.774f);
        obj = player.playerManager.PlayerDamageObjManage(player, obj, jumpAttack_Land.attackDamage);
    }
}
