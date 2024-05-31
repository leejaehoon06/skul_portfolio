using System.Collections;
using UnityEngine;

public class Head : Damage
{
    Rigidbody2D rigid;
    [SerializeField] float speed = 12f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        StartCoroutine("HeadMove");
    }
    private void Update()
    {
        if (player.Skill2Timer <= 0)
        {
            player.Skill2Able = true;
        }
        player.GetComponent<Animator>().SetFloat("headless", 1f);
        if (player.Skill1Timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        player.Skill2Able = false;
        player.GetComponent<Animator>().SetFloat("headless", 0f);
    }
    IEnumerator HeadMove()
    {
        yield return new WaitForFixedUpdate();
        if (transform.rotation.eulerAngles.y > 0)
            speed *= -1f;

        float timer = 0;
        while (timer <= 1f)
        {
            transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            transform.Rotate(0, 0, -5 * Time.fixedDeltaTime * 180f);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Bound();
    }
    int FirstTime = 0;
    int GetTime = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bound();
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterDamaged(collision.gameObject.GetComponent<Monster>(), collision.collider, false);
        }
    }
    public void GetHead(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer")) && GetTime == 1)
        {//추후에 멀티 조건 만들기
            player.Skill1Timer = 0;
            Destroy(gameObject);
        }
    }
    
    float BoundX = 100f;
    [SerializeField] float BoundY = 500f;
    void Bound()
    {
        if (FirstTime == 0)
        {
            ++FirstTime;
            StopCoroutine("HeadMove");
            rigid.constraints = RigidbodyConstraints2D.None;
            HitObjInstan(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            rigid.AddForce(new Vector2(Random.Range(-1f, 1f) * BoundX,
                Random.Range(0.5f, 1f) * BoundY));
            gameObject.layer = LayerMask.NameToLayer("OnlyHitGround");
            StartCoroutine(GetOn());
        }
    }
    IEnumerator GetOn()
    {
        float timer = 0;
        while(timer <= 1f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GetTime = 1;
    }
}
