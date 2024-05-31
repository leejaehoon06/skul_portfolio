using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHeal : MonoBehaviour
{
    public Monster monster { get; set; }
    public void heal(int healNum)
    {
        monster.monsterStat.CurHp += healNum;
    }
}
