using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterMove : MonoBehaviour
{
    public Rigidbody2D rigid { get; set; }
    public Animator anim { get; set; }
    public Monster monster { get; set; }
    [SerializeField]
    float MoveSpeed;
    public float _MoveSpeed { get { return MoveSpeed; } }
    float MoveMaxTimer = 2f;
    float MoveRandTimer;
    float MoveTimer;
    public bool IsMove { get; set; } = true;
    private void Start()
    {
        MoveRandTimer = Random.Range(MoveMaxTimer * (4 / _MoveSpeed) * 0.8f, MoveMaxTimer * (4 / _MoveSpeed) * 1.2f);
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monster = GetComponentInParent<Monster>();
    }
    private void FixedUpdate()
    {
        if (GetComponent<TurnHitWall>() != null)
        {
            if (monster.IsAction && IsMove)
            {
                if (monster.targetParent != null && monster.target != null)
                {
                    TargetMove();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
    }
    public void Move()
    {
        anim.SetBool("walk", true);
        MoveTimer += Time.fixedDeltaTime;
        if (MoveTimer <= MoveRandTimer)
        {
            if (transform.rotation.eulerAngles.y == 0)
            {
                rigid.velocity = new Vector2(_MoveSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(-_MoveSpeed, rigid.velocity.y);
            }
        }
        else
        {
            StartCoroutine(MoveStopTurn());
        }
    }
    public virtual void TargetMove()
    {
    }
    public IEnumerator MoveStopTurn()
    {
        if (IsMove == true && monster.IsAction == true)
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
            MoveTimer = 0;
            MoveRandTimer = Random.Range(MoveMaxTimer * (4 / _MoveSpeed) * 0.8f, 
                MoveMaxTimer * (4 / _MoveSpeed) * 1.2f);
            IsMove = true;
        }
    }
}
