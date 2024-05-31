using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureHpBar : MonoBehaviour
{
    [SerializeField]
    Sprite DeadImage;
    [SerializeField]
    Image image;
    [SerializeField]
    Image Hpbar;
    [SerializeField]
    Text Name;
    [SerializeField]
    Text Level;
    public Monster monster { get; set; }
    private void Start()
    {
        
    }
    private void Update()
    {
        Hpbar.fillAmount = Mathf.Lerp(Hpbar.fillAmount, monster.monsterStat.CurHp / monster.monsterStat.MaxHp, 0.3f);
        if (monster.monsterStat.CurHp <= 0)
        {
            image.sprite = DeadImage;
        }
    }
}
