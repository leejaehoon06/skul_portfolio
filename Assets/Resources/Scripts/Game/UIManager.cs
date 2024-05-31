using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    [SerializeField]
    GameObject _DamageTextObjects;
    public GameObject DamageTextObjects { get {  return _DamageTextObjects; } }
    [SerializeField]
    Text _DamageText;
    public Text DamageText { get { return _DamageText; } }
    [SerializeField]
    GameObject _MonsterHpBarObjs;
    public GameObject MonsterHpBarObjs { get {  return _MonsterHpBarObjs; } }
    [SerializeField]
    Image PlayerHitScreen;
    [SerializeField]
    GameObject _InteractionObj;
    public GameObject InteractionObj { get { return _InteractionObj; } }
    [SerializeField]
    Transform PopupTrans;
    public List<Transform> PopupTransChild { get; set; } = new List<Transform>();
    private void Awake()
    {
        current = this;
        
    }
    private void Start()
    {
        for (int i = 0; i < PopupTrans.childCount; i++) {
            PopupTransChild.Add(PopupTrans.GetChild(i));
        }
    }
    public IEnumerator HitScreen()
    {
        float timer = 0.5f;
        while(timer > 0)
        {
            PlayerHitScreen.color = new Color(1f, 0, 0, timer * 2);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayerHitScreen.color = new Color(1f, 0, 0, 0);
    }

}
