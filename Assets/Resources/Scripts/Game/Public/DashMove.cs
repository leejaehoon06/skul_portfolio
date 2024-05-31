using SpriteTrail;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    Rigidbody2D rigid;
    TurnHitWall turnHitWall;
    [SerializeField]
    float DashFrame;
    [SerializeField]
    float DashDistance = 1300f;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        turnHitWall = GetComponent<TurnHitWall>();
    }
    IEnumerator Dash()
    {
        float DashPower = 1f;
        float DashTimer = DashFrame / 60f;
        if (transform.rotation.eulerAngles.y == 180)
        {
            while (DashTimer > 0)
            {
                if (turnHitWall.CheckHitWall() == false)
                {
                    rigid.AddForce(new Vector2(-DashDistance * DashPower, 0));
                }
                DashPower -= Time.fixedDeltaTime * (60f / DashFrame);
                DashTimer -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (DashTimer >= 0)
            {
                if (turnHitWall.CheckHitWall() == false)
                {
                    rigid.AddForce(new Vector2(DashDistance * DashPower, 0));
                }
                DashPower -= Time.fixedDeltaTime * (60f / DashFrame);
                DashTimer -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
