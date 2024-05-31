using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject monster;
    private void MonsterInstan()
    {
        Instantiate(monster, transform.position, transform.rotation);
    }
}
