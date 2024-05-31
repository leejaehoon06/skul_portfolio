using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiHitDamage : Damage
{
    [SerializeField]
    int damageNum;
    [SerializeField]
    float damageTimer;
    public List<Monster> monsters = new List<Monster>();
    List<Player> players = new List<Player>();
    List<int> monsterDamageNums = new List<int>();
    List<float> monsterDamageTimers = new List<float>();
    List<Monster> otherMonsters = new List<Monster>();
    List<Player> otherPlayers = new List<Player>();
    List<int> otherMonsterDamageNums = new List<int>();
    List<float> otherMonsterDamageTimers = new List<float>();
    [SerializeField]
    bool DisableOn;
    private void Update()
    {
        if (monster == null)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                monsterDamageTimers[i] += Time.deltaTime;
                if (monsterDamageTimers[i] >= damageTimer && monsterDamageNums[i] <= damageNum)
                {
                    if (monsters[i] != null)
                    {
                        if (monsters[i].gameObject.activeSelf)
                        {
                            monsterDamageTimers[i] = 0;
                            monsterDamageNums[i]++;
                            MonsterDamaged(monsters[i], monsters[i].GetComponent<Collider2D>(), true);
                      
                        }
                    }
                }
            }
            for (int i = 0; i < otherMonsters.Count; i++)
            {
                if (otherMonsterDamageTimers[i] < damageTimer)
                {
                    otherMonsterDamageTimers[i] += Time.deltaTime;
                }
            }
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                monsterDamageTimers[i] += Time.deltaTime;
                if (monsterDamageTimers[i] >= damageTimer && monsterDamageNums[i] < damageNum)
                {
                    monsterDamageTimers[i] = 0;
                    players[i] = PlayerDamaged(players[i], players[i].GetComponent<Collider2D>());
                    monsterDamageNums[i]++;
                }
            }
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                if (otherMonsterDamageTimers[i] < damageTimer)
                {
                    otherMonsterDamageTimers[i] += Time.deltaTime;
                }
            }
        }
    }
    private void OnDisable()
    {
        if(DisableOn == false)
        {
            Destroy(gameObject);
        }
    }
    public override void MonsterDestroy(Monster monster)
    {
        if (monster != null)
        {
            monsterDamageTimers.RemoveAt(monsters.IndexOf(monster));
            monsterDamageNums.RemoveAt(monsters.IndexOf(monster)); ;
            monsters.Remove(monster);
        }

        for(int i=0; i < monsters.Count;)
        {
            if (monsters[i] == null)
            {
                monsters.RemoveAt(i);
            }
            else
                ++i;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (monsters.IndexOf(collision.GetComponent<Monster>()) == -1)
            {
                if (otherMonsters.IndexOf(collision.GetComponent<Monster>()) == -1)
                {
                    monsters.Add(collision.GetComponent<Monster>());
                    monsterDamageNums.Add(0);
                    monsterDamageTimers.Add(damageTimer);
                }
                else
                {
                    monsters.Add(collision.GetComponent<Monster>());
                    monsterDamageNums.Add(otherMonsterDamageNums[otherMonsters.IndexOf(collision.GetComponent<Monster>())]);
                    monsterDamageTimers.Add(otherMonsterDamageTimers[otherMonsters.IndexOf(collision.GetComponent<Monster>())]);
                    otherMonsterDamageNums.RemoveAt(otherMonsters.IndexOf(collision.GetComponent<Monster>()));
                    otherMonsterDamageTimers.RemoveAt(otherMonsters.IndexOf(collision.GetComponent<Monster>()));
                    otherMonsters.RemoveAt(otherMonsters.IndexOf(collision.GetComponent<Monster>()));
                }
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer"))
        {
            if (players.IndexOf(collision.GetComponent<Player>()) == -1)
            {
                if (otherPlayers.IndexOf(collision.GetComponent<Player>()) == -1)
                {
                    players.Add(collision.GetComponent<Player>());
                    monsterDamageNums.Add(0);
                    monsterDamageTimers.Add(damageTimer);
                }
                else
                {
                    players.Add(collision.GetComponent<Player>());
                    monsterDamageNums.Add(otherMonsterDamageNums[otherPlayers.IndexOf(collision.GetComponent<Player>())]);
                    monsterDamageTimers.Add(otherMonsterDamageTimers[otherPlayers.IndexOf(collision.GetComponent<Player>())]);
                    otherMonsterDamageNums.RemoveAt(otherPlayers.IndexOf(collision.GetComponent<Player>()));
                    otherMonsterDamageTimers.RemoveAt(otherPlayers.IndexOf(collision.GetComponent<Player>()));
                    otherPlayers.RemoveAt(otherPlayers.IndexOf(collision.GetComponent<Player>()));
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster")
            && monsters.IndexOf(collision.GetComponent<Monster>()) != -1)
        {
            otherMonsters.Add(collision.GetComponent<Monster>());
            otherMonsterDamageNums.Add(monsterDamageNums[monsters.IndexOf(collision.GetComponent<Monster>())]);
            otherMonsterDamageTimers.Add(monsterDamageTimers[monsters.IndexOf(collision.GetComponent<Monster>())]);
            monsterDamageNums.RemoveAt(monsters.IndexOf(collision.GetComponent<Monster>()));
            monsterDamageTimers.RemoveAt(monsters.IndexOf(collision.GetComponent<Monster>()));
            monsters.RemoveAt(monsters.IndexOf(collision.GetComponent<Monster>()));
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            && collision.gameObject.layer == LayerMask.NameToLayer("PlatformPlayer")
            && monsters.IndexOf(collision.GetComponent<Monster>()) != -1)
        {
            otherPlayers.Add(collision.GetComponent<Player>());
            otherMonsterDamageNums.Add(monsterDamageNums[players.IndexOf(collision.GetComponent<Player>())]);
            otherMonsterDamageTimers.Add(monsterDamageTimers[players.IndexOf(collision.GetComponent<Player>())]);
            monsterDamageNums.RemoveAt(players.IndexOf(collision.GetComponent<Player>()));
            monsterDamageTimers.RemoveAt(players.IndexOf(collision.GetComponent<Player>()));
            players.RemoveAt(players.IndexOf(collision.GetComponent<Player>()));
        }
    }
}
