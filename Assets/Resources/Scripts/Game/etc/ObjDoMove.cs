using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjDoMove : MonoBehaviour
{
    public IEnumerator ObjMove(GameObject Obj, int i = 0)
    {
        Vector3 pos = transform.position;
        Obj.transform.DOMoveX(pos.x + i * 0.6f, 25f / 60f).SetEase(Ease.InQuad);
        Obj.transform.DOMoveY(pos.y + 2.5f, 25f / 60f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(25f / 60f);
        Obj.transform.DOMoveX(pos.x + i * 1.2f, 15f / 60f).SetEase(Ease.OutQuad);
        Obj.transform.DOMoveY(pos.y + 1f, 15f / 60f).SetEase(Ease.InQuad);
        Obj.GetComponent<Collider2D>().enabled = true;
    }
}
