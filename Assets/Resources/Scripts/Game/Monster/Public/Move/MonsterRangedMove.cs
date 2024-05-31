using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangedMove : MonsterMove
{
    [SerializeField]
    float Range;
    Coroutine coroutine;
    int rotate = 1;
    public override void TargetMove()
    {
        int TargetRotate = 1;
        if (monster.target.transform.rotation.eulerAngles.y == 0)
        {
            TargetRotate = 1;
        }
        else
        {
            TargetRotate = -1;
        }
        if (transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        if (coroutine == null)
        {
            float TargetPosX = monster.target.transform.position.x + monster.target.GetComponent<BoxCollider2D>().offset.x
                * TargetRotate * monster.target.transform.localScale.x;
            float PosX = transform.position.x + GetComponent<BoxCollider2D>().offset.x * rotate * transform.localScale.x;
            if (GetComponent<TurnHitWall>().CheckHitWall() || GetComponent<TurnHitWall>().CheckHitGround() == false)
            {
                StartCoroutine(MoveStop());
            }
            else if (TargetPosX - Range <= PosX && TargetPosX >= PosX)
            {
                anim.SetBool("walk", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rigid.velocity = new Vector2(-_MoveSpeed / 1.5f, rigid.velocity.y);
            }
            else if (TargetPosX + Range >= PosX && TargetPosX <= PosX)
            {
                anim.SetBool("walk", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rigid.velocity = new Vector2(_MoveSpeed / 1.5f, rigid.velocity.y);
            }
            else if (TargetPosX + Range + 1.5 >= PosX && TargetPosX + Range <= PosX)
            {
                anim.SetBool("walk", false);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (TargetPosX - Range - 1.5 <= PosX && TargetPosX - Range >= PosX)
            {
                anim.SetBool("walk", false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (TargetPosX < PosX)
            {
                anim.SetBool("walk", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rigid.velocity = new Vector2(-_MoveSpeed, rigid.velocity.y);
            }
            else if (TargetPosX > PosX)
            {
                anim.SetBool("walk", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rigid.velocity = new Vector2(_MoveSpeed, rigid.velocity.y);
            }
        }
    }
    IEnumerator MoveStop()
    {
        if (IsMove == true)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            float Rand = Random.Range(0.3f, 0.8f);
            float Timer = 0;
            while (Timer <= Rand)
            {
                IsMove = false;
                Timer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            GetComponent<TurnHitWall>().TurnObj();
            IsMove = true;
        }
    }
}
