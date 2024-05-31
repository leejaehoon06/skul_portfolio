using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOn : MonoBehaviour
{
    public IEnumerator HitMaterailOn()
    {
        float timer = 0.15f;
        while (timer >= 0f)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_HitEffectBlend", timer / 0.15f);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<SpriteRenderer>().material.SetFloat("_HitEffectBlend", 0);
    }
}
