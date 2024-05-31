using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/Attack", order = 0)]
public class MonsterAttackInfo : ScriptableObject
{ 
    public float CoolTime;
    public float DonActionTime;
    public float DamageNum;
    public GameObject AttackColl;
    public GameObject DamageObj;
}
