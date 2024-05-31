using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInstanEffect : MonoBehaviour
{
    [SerializeField]
    GameObject destroyEffect;
    private void OnDisable()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }
}
