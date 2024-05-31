using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/Boss/Attack", order = 0)]
public class BossAttackInfo : MonsterAttackInfo
{
    public float HpCondition = 1f;
}
