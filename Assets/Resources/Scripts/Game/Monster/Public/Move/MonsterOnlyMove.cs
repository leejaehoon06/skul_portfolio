using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnlyMove : MonsterMove
{
    public override void TargetMove()
    {
        Move();
    }
}
