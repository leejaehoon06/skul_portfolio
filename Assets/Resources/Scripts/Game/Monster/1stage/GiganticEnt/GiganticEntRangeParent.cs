using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticEntRangeParent : MonoBehaviour
{
    public List<GiganticEntRange> giganticEntRanges { get; set; } = new List<GiganticEntRange>();
    private void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
    public void HitRange(GameObject obj)
    {
        for (int i = 0; i < giganticEntRanges.Count; i++)
        {
            if (giganticEntRanges[i].gameObject != null)
            {
                giganticEntRanges[i].HitPlayer = true;
            }
        }
    }
}
