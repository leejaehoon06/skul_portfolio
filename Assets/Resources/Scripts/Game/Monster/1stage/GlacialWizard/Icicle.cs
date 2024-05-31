using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField]
    float Speed = 1f;
    float timer = -0.6f;
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 0f)
        {
            transform.position += Vector3.down * Speed * Time.fixedDeltaTime * timer;
        }
    }
}
