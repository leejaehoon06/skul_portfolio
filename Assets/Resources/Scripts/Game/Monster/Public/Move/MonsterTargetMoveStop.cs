using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTargetMoveStop : MonsterMove
{
    public override void TargetMove()
    {
        anim.SetBool("walk", false);
    }
}
