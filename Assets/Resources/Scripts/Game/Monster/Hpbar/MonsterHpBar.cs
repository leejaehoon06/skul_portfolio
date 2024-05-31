using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour
{
    public Monster monster { get; set; }
    BoxCollider2D boxColl;
    UnityEngine.UI.Slider slider;
    RectTransform rectTrans;
    void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        rectTrans = GetComponent<RectTransform>();
        boxColl = monster.GetComponent<BoxCollider2D>();
        rectTrans.sizeDelta = new Vector2(boxColl.size.x * 30, rectTrans.sizeDelta.y);
        transform.position = Camera.main.WorldToScreenPoint(boxColl.bounds.center + new Vector3(0
            , -boxColl.bounds.extents.y - 0.35f, 0));
    }
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, monster.monsterStat.CurHp / monster.monsterStat.MaxHp, 0.3f);
        transform.position = Camera.main.WorldToScreenPoint(boxColl.bounds.center + new Vector3(0
            , -boxColl.bounds.extents.y - 0.35f, 0));
    }
}
