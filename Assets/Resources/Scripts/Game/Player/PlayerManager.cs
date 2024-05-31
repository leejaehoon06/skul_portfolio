using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Inventory _inventory;
    public Inventory inventory { get { return _inventory; } }
    [SerializeField]
    GameObject SwitchEffect;
    public List<Player> players { get; set; } = new List<Player>();
    public List<GameObject> playerObjs { get; set; } = new List<GameObject>();
    public PlayerStat playerStat { get; set; } = PlayerStat.SetSkulStats();
    public bool PlayerOn { get; set; } = true;
    public float SwitchTimer { get; set; }
    public int JumpValue { get; set; } = 0;
    public delegate void DelAttack();
    public DelAttack delAttack { get; set; }
    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventory.InvenOn();
        }
        if(playerObjs.Count == 2)
        {
            playerObjs[1].SetActive(false);
        }
        if(PlayerOn)
        {
            if(Input.GetKeyDown(KeyCode.Space) && players.Count == 2 && SwitchTimer <= 0) 
            {
                Switch();
            }
        }
    }
    public IEnumerator ChangePlayerLayer(Player player)
    {
        yield return new WaitForSeconds(0.2f);
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public IEnumerator SkulSkillCoolDown(int SkillIndex)
    {
        Player player = players[0];
        if(SkillIndex == 1)
        {
            player.Skill1Timer = player.skills[0].skillCool;
            while (player.Skill1Timer >= 0)
            {
                player.Skill1Timer -= Time.deltaTime * (playerStat.SkillCd / 100);
                player.Skill1Able = false;
                yield return new WaitForEndOfFrame();
            }
            if (player.skills[0].skillCool > 0)
            {
                player.Skill1Able = true;
            }
        }
        else if (SkillIndex == 2)
        {
            player.Skill2Timer = player.skills[1].skillCool;
            player.Skill2Able = false;
            while (player.Skill2Timer >= 0)
            {
                player.Skill2Timer -= Time.deltaTime * (playerStat.SkillCd / 100);
                if (player._MyImport.unitCode != PlayerCode.LittleBone)
                {
                    player.Skill2Able = false;
                }
                yield return new WaitForEndOfFrame();
            }
            if (player.skills[1].skillCool > 0 && player._MyImport.unitCode != PlayerCode.LittleBone)
            {
                player.Skill2Able = true;
            }
        }
    }
    void Switch()
    {
        _inventory.SwitchSkul();
        Player SwitchPlayer = players[0];
        players[0] = players[1];
        players[1] = SwitchPlayer;
        GameObject SwitchPlayerObject = playerObjs[0];
        playerObjs[0] = playerObjs[1];
        playerObjs[1] = SwitchPlayerObject;
        playerObjs[0].transform.position = playerObjs[1].transform.position;
        playerObjs[0].transform.rotation = playerObjs[1].transform.rotation;
        playerObjs[0].SetActive(true);
        Instantiate(SwitchEffect, playerObjs[0].transform.position, Quaternion.identity);
        playerObjs[0].GetComponent<Animator>().Play("Switch");
        StartCoroutine(SwitchCoolDown());
        StartCoroutine(SwitchSkillAbleOff());
    }
    IEnumerator SwitchCoolDown()
    {
        SwitchTimer = 5f;
        while (SwitchTimer > 0)
        {
            SwitchTimer -= Time.deltaTime * (playerStat.SwitchCd / 100);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator SwitchSkillAbleOff()
    {
        yield return new WaitForEndOfFrame();
        while (players[0].anim.GetCurrentAnimatorStateInfo(0).IsName("Switch"))
        {
            players[0].Skill1Able = false;
            if (players[0].skills.Count == 2)
            {
                 players[0].Skill2Able = false;
            }
            players[1].Skill1Able = false;
            if (players[1].skills.Count == 2)
            {
                players[1].Skill2Able = false;
            }
            yield return new WaitForEndOfFrame();
        }
        if (players[0].skills[0].skillBaskiBool)
        {
            players[0].Skill1Able = true;
        }
        if (players[0].skills.Count == 2)
        {
            if (players[0].skills[1].skillBaskiBool)
            {
                players[0].Skill2Able = true;
            }
        }
        if (players[1].skills[0].skillBaskiBool)
        {
            players[1].Skill1Able = true;
        }
        if (players[1].skills.Count == 2)
        {
            if (players[1].skills[1].skillBaskiBool)
            {
                players[1].Skill2Able = true;
            }
        }
    }
    public GameObject PlayerDamageObjManage(Player player, GameObject obj, float damage)
    {
        Damage playerDamage = obj.GetComponent<Damage>();
        playerDamage.PlayerDamageNum = damage;
        playerDamage.player = player;
        return obj;
    }
}
