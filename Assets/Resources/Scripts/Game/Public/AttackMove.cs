using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    Player player;
    Rigidbody2D rigid;
    void Start()
    {
        if (GetComponent<Player>() != null)
        {
            player = GetComponent<Player>();
        }
        rigid = GetComponent<Rigidbody2D>();
    }
    IEnumerator MovingAttack(float AttackValue)
    {
        if (player != null)
        {
            if (player.moveX != 0 && player.AttackX == player.moveX)
            {
                float timer = 0;
                rigid.velocity = Vector2.zero;
                while (timer <= 0.05f)
                {
                    timer += Time.deltaTime;
                    if (player.CheckingWall == false)
                    {
                        rigid.AddForce(new Vector2(AttackValue * player.AttackX, 0));
                    }
                    yield return new WaitForFixedUpdate();
                }
            }
        }
        else
        {
            int rotate = 1;
            if(transform.rotation.eulerAngles.y == 0)
            {
                rotate = 1;
            }
            else
            {
                rotate = -1;
            }
            float timer = 0;
            rigid.velocity = Vector2.zero;
            while (timer <= 0.05f)
            {
                timer += Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, (rotate - 1) * 90, 0);
                if (GetComponent<TurnHitWall>().CheckHitWall() == false)
                {
                    rigid.AddForce(new Vector2(AttackValue * rotate, 0));
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
