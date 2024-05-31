using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMeleeMove : MonsterMove
{
    public override void TargetMove()
    {
        int TargetRotate = 1;
        if(monster.target.transform.rotation.eulerAngles.y == 0)
        {
            TargetRotate = 1;
        }
        else
        {
            TargetRotate = -1;
        }
        int rotate = 1;
        if(transform.rotation.eulerAngles.y == 0)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
        float TargetPosX = monster.target.transform.position.x + monster.target.GetComponent<BoxCollider2D>().offset.x 
            * TargetRotate * monster.target.transform.localScale.x;
        float PosX = transform.position.x + GetComponent<BoxCollider2D>().offset.x * rotate * transform.localScale.x;
        if (TargetPosX - 0.3f <= PosX && TargetPosX + 0.3f >= PosX)
        {
            anim.SetBool("walk", false);
        }
        else if (TargetPosX < PosX)
        {
            anim.SetBool("walk", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.rotation.eulerAngles.y == 0)
            {
                rigid.velocity = new Vector2(_MoveSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(-_MoveSpeed, rigid.velocity.y);
            }
        }
        else if (TargetPosX > PosX)
        {
            anim.SetBool("walk", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.rotation.eulerAngles.y == 0)
            {
                rigid.velocity = new Vector2(_MoveSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(-_MoveSpeed, rigid.velocity.y);
            }
        }
    }
}
