using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEffect : MonoBehaviour
{
    [SerializeField]
    GameObject effect;
    [SerializeField]
    bool ParentInstan;
    [SerializeField]
    bool InstanRotation;
    private void Start()
    {
        if (ParentInstan == false)
        {
            if (InstanRotation == false)
            {
                Instantiate(effect, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(effect, transform.position, effect.transform.rotation);
            }
        }
        else
        {
            Instantiate(effect, transform);
        }
    }
}
