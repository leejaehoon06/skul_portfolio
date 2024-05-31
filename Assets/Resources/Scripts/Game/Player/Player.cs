using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Skul MyImport;
    public Skul _MyImport 
    { 
        get { return MyImport; }
        set { MyImport = value; }
    }
    [SerializeField]
    GameObject DoubleJumpEffect;
    [SerializeField]
    GameObject DashEffect;
    [SerializeField]
    GameObject PowerDashEffect;
    SpriteTrail.SpriteTrail spriteTrail;
    
    public PlayerManager playerManager { get; set; }
    public Animator anim { get; set; }
    BoxCollider2D coll;
    Rigidbody2D rigid;
    float speed = 5;
    int DashValue;
    Interaction LastInteract;
    [SerializeField]
    List<Skill> _skills;
    public List<Skill> skills 
    { 
        get { return _skills; } 
        set { _skills = value; } 
    }
    public float Skill1Timer { get; set; }
    public float Skill2Timer { get; set; }
    public bool Skill1Able { get; set; } = true;
    public bool Skill2Able { get; set; } = true;
    public bool SkillChange { get; set; }
    public bool IsGround { get; set; }
    float MaxFallSpeed = 20f;
    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        GetComponent<SpriteRenderer>().material = Instantiate(GetComponent<SpriteRenderer>().material);
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteTrail = GetComponent<SpriteTrail.SpriteTrail>();
        if (_MyImport.unitCode == PlayerCode.LittleBone)
        {
            Skill2Able = false;
        }
    }
    private void OnEnable()
    {
        DashValue = 0;
        if (playerManager != null)
        {
            playerManager.JumpValue = 0;
        }
        GetComponent<SpriteRenderer>().material.SetFloat("_HitEffectBlend", 0);
    }
    public bool ClampVelocity { get; set; } = true;
    private void FixedUpdate()
    {
        if (playerManager.PlayerOn == true)
        {
            if(ClampVelocity == true)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -MaxFallSpeed, MaxFallSpeed));
            }
            Move();
        }
    }
    private void Update()
    {
        if (playerManager.PlayerOn == true)
        {
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftShift) && IsDonJump == false)
            {
                if(HitPlatForm() == false)
                {
                    Jump();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && IsDonJump == false)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Z) && IsDonDash == false)
            {
                Dash();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
            }
            if (SkillChange == false)
            {
                if (Input.GetKeyDown(KeyCode.S) && Skill1Timer <= 0f && Skill1Able == true)
                {
                    Skill1();
                }
                if (Input.GetKeyDown(KeyCode.D) && Skill2Timer <= 0f && Skill2Able == true)
                {
                    Skill2();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D) && Skill1Timer <= 0f && Skill1Able == true)
                {
                    Skill1();
                }
                if (Input.GetKeyDown(KeyCode.S) && Skill2Timer <= 0f && Skill2Able == true)
                {
                    Skill2();
                }
            }
            if(LastInteract != null)
            {
                if (LastInteract.Keydown == false)
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        LastInteract.Interact();
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.G))
                    {
                        LastInteract.KeydownTimer += Time.deltaTime;
                        if (LastInteract.KeydownTimer >= 1f)
                        {
                            LastInteract.KeydownInteract();
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.G))
                    {
                        if (LastInteract.KeydownTimer <= 0.5f)
                        {
                            LastInteract.Interact();
                        }
                        else
                        {
                            LastInteract.KeydownTimer = 0f;
                        }
                    }
                }
            }
        }
    }
    public float moveX { get; set; }
    public bool IsDonMove { get; set; }
    public bool CheckingWall { get; set; }
    public void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if (IsDonMove == false)
        {
            Vector3 movement = new Vector3();
            movement.Set(moveX, 0, 0);
            movement.Normalize();
            if (CheckingWall == false)
            {
                //transform.position += movement * Time.deltaTime * speed * (playerManager.playerStat.Speed / 100);
                rigid.velocity = new Vector2(movement.x * speed * (playerManager.playerStat.Speed / 100), rigid.velocity.y);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack") == false)
            {
                if (moveX != 0)
                {
                    transform.rotation = Quaternion.Euler(0, (moveX - 1) * 90, 0);
                    anim.SetBool("walk", true);
                }
                else
                {
                    anim.SetBool("walk", false);
                }
            }
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }
    public bool IsDonJump { get; set; }
    public void Jump()
    {
        if (anim.GetBool("fall"))
        {
            playerManager.JumpValue++;
        }
        if (playerManager.JumpValue <= 2)
        {
            anim.SetTrigger("jump");
            anim.ResetTrigger("attack");
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, 800f));
            if (anim.GetBool("fall"))
            {
                Instantiate(DoubleJumpEffect, transform.position, transform.rotation);
            }
        }
        playerManager.JumpValue++;
    }
    bool HitPlatForm()
    {
        float scope = 0.1f;
        int layerMask = LayerMask.GetMask("Ground");

        List<Ray2D> rays2D = new List<Ray2D>();
        rays2D.Add(new Ray2D(transform.position + Vector3.right * transform.localScale.x * coll.size.x * -0.5f, Vector2.down));
        rays2D.Add(new Ray2D(transform.position + Vector3.right * transform.localScale.x * coll.size.x * 0.5f, Vector2.down));
        foreach (Ray2D ray in rays2D)
        {
            Debug.DrawRay(ray.origin, new Vector2(ray.direction.x, ray.direction.y * scope), Color.yellow, 1f);
        }
        foreach (Ray2D ray in rays2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, scope, layerMask);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.GetComponent<PlatformEffector2D>() != null && rigid.velocity.y == 0)
                {
                    gameObject.layer = LayerMask.NameToLayer("PlatformPlayer");
                    playerManager.StartCoroutine("ChangePlayerLayer", this);
                    return true;
                }
            }
        }
        return false;
    }
    [SerializeField]
    float DashTimer;
    public void Dash()
    {
        if (DashValue <= 1)
        {
            if (DashValue == 0)
            {
                StartCoroutine("DashMove");
                StartCoroutine(DashValueReset());
            }
            else if (DashValue == 1 && DashTimer > 0 && _MyImport.skulPowerDash == false)
            {
                StopCoroutine("DashMove");
                StartCoroutine("DashMove");
            }
        }
        DashValue++;
    }
    public bool DashTime { get; set; }
    public bool IsDonDash { get; set; }
    float DashDistance = 1300f;
    float PowerDashDistance = 1700f;
    IEnumerator DashMove()
    {
        spriteTrail.enabled = true;
        if (moveX != 0)
        {
            transform.rotation = Quaternion.Euler(0, (moveX - 1) * 90, 0);
        }
        DashTimer = 25f / 60f;
        Instantiate(DashEffect, transform.position, transform.rotation);
        anim.SetTrigger("dash");
        anim.ResetTrigger("attack");
        DashTime = true;
        StartCoroutine("DashCheck");
        if (_MyImport.skulPowerDash == false)
        {
            if (transform.rotation.eulerAngles.y == 180)
            {
                while (DashTimer >= 0)
                {
                    if (CheckingWall == false)
                    {
                        rigid.AddForce(new Vector2(-DashDistance * DashTimer, 0));
                        //transform.Translate(new Vector2(DashDistance * DashTimer * Time.deltaTime, 0));
                    }
                    DashTimer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                while (DashTimer >= 0)
                {
                    if (CheckingWall == false)
                    {
                        rigid.AddForce(new Vector2(DashDistance * DashTimer, 0));
                        //transform.Translate(new Vector2(DashDistance * DashTimer * Time.deltaTime, 0));
                    }
                    DashTimer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
            }
        }
        else
        {
            GameObject obj = Instantiate(PowerDashEffect, transform);
            obj.GetComponent<Damage>().player = this;
            if (transform.rotation.eulerAngles.y == 180)
            {
                while (DashTimer >= 0)
                {
                    if (CheckingWall == false)
                    {
                        //rigid.velocity = new Vector2(PowerDashDistance * DashTimer * Time.deltaTime, rigid.velocity.y);
                        rigid.AddForce(new Vector2(-PowerDashDistance * DashTimer, 0));
                    }
                    DashTimer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("dash") == false)
                    {
                        break;
                    }
                }
            }
            else
            {
                while (DashTimer >= 0)
                {
                    if (CheckingWall == false)
                    {
                        //rigid.velocity = new Vector2(PowerDashDistance * DashTimer * Time.deltaTime, rigid.velocity.y);
                        rigid.AddForce(new Vector2(PowerDashDistance * DashTimer, 0));
                    }
                    DashTimer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("dash") == false)
                    {
                        break;
                    }
                }
            }
        }
    }
    IEnumerator DashCheck()
    {
        yield return new WaitForEndOfFrame();
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("dash"))
        {
            yield return new WaitForEndOfFrame();
        }
        spriteTrail.enabled = false;
        DashTime = false;
    }
    IEnumerator DashValueReset()
    {
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime * (playerManager.playerStat.DashCd / 100);
            yield return new WaitForEndOfFrame();
        }
        DashValue = 0;
    }
    public float AttackX { get; set; }
    void Attack()
    {
        anim.SetTrigger("attack");
        if (moveX != 0)
        {
            AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
            StartCoroutine(AttachCheck(moveX, clipInfo));
        }
    }
    void AttackStart()
    {
        anim.SetFloat("attackspeed", (playerManager.playerStat.AttackSpeed / 100) * playerManager.playerStat.AdSpeed);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (transform.rotation.eulerAngles.y == 0)
        {
            AttackX = 1;
        }
        else
        {
            AttackX = -1;
        }
    }
    IEnumerator AttachCheck(float moveX, AnimatorClipInfo[] clipInfo)
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName(clipInfo[0].clip.name))
        {
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = Quaternion.Euler(0, (moveX - 1) * 90, 0);
        if (transform.rotation.eulerAngles.y == 0)
        {
            AttackX = 1;
        }
        else
        {
            AttackX = -1;
        }
    }
    void Skill1()
    {
        anim.Play(skills[0].name);
        anim.SetFloat("skillspeed", (playerManager.playerStat.AttackSpeed / 100) * playerManager.playerStat.SkillSpeed);
        playerManager.StartCoroutine("SkulSkillCoolDown", 1);
    }
    void Skill2()
    {
        anim.Play(skills[1].name);
        anim.SetFloat("skillspeed", (playerManager.playerStat.AttackSpeed / 100) * playerManager.playerStat.SkillSpeed);
        playerManager.StartCoroutine("SkulSkillCoolDown", 2);
    }
    
    public GameObject LastInstanSkillObj { get; set; }
    void SkillObjInstan()
    {
        AnimatorClipInfo[] clipInfo;
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        int index = SkillIndexCheck(clipInfo[0].clip.name);
        if (skills[index].skillObj != null)
        {
            LastInstanSkillObj = Instantiate(skills[index].skillObj, transform.position, transform.rotation);
            LastInstanSkillObj = playerManager.PlayerDamageObjManage(this, LastInstanSkillObj, skills[index].skillDamage);
        }
    }
    void SkillObjInstan_Name(string AnimName)
    {
        int index = SkillIndexCheck(AnimName);
        if (skills[index].skillObj != null)
        {
            LastInstanSkillObj = Instantiate(skills[index].skillObj, transform.position, transform.rotation);
            LastInstanSkillObj = playerManager.PlayerDamageObjManage(this, LastInstanSkillObj, skills[index].skillDamage);
        }
    }
    int SkillIndexCheck(string AnimName)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if (skills[i].name == AnimName)
            {
                return i;
            }
        }
        return -1;
    }
    public IEnumerator HitKnockback(float KnockbackX, float KnockbackY)
    {
        float timer = 0.1f;
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        while (timer >= 0)
        {
            rigid.AddForce(new Vector2(KnockbackX, KnockbackY));
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Interaction>() != null && collision.gameObject.GetComponent<Interaction>().enabled == true)
        { 
            if(LastInteract == null)
            {
                LastInteract = collision.gameObject.GetComponent<Interaction>();
                LastInteract.PlayerIn();
            }
            else if (LastInteract != collision.gameObject.GetComponent<Interaction>())
            {
                LastInteract.PlayerOut();
                LastInteract = collision.gameObject.GetComponent<Interaction>();
                LastInteract.PlayerIn();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Interaction>() != null && collision.gameObject.GetComponent<Interaction>().enabled == true)
        {
            if (LastInteract != null && LastInteract == collision.gameObject.GetComponent<Interaction>())
            {
                LastInteract.PlayerOut();
                LastInteract = null;
            }
        }
    }
}
