using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Vector2 worldPoint;
    public void SetWorldPoint(Vector2 _worldPoint)
    {
        worldPoint = _worldPoint;
    }
    void Start()
    {
        RandNum = Random.Range(-RandMaxNum, RandMaxNum);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPoint + new Vector2(RandNum * RandTimer, UpDownPoint * UpDownTimer + 1f));
        transform.position = screenPosition;
        Destroy(gameObject, 0.45f);
    }
    [SerializeField]
    float UpDownPoint;
    [SerializeField]
    float RandMaxNum;
    float RandNum;
    [SerializeField]
    float UpDownTimer = 0f;
    float RandTimer = 0f;
    int UpDownSwitch = 0;
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPoint + 
            new Vector2(RandNum * RandTimer, UpDownPoint * Mathf.Sin(UpDownTimer) + 0.7f));
        transform.position = screenPosition;
        if (UpDownTimer >= 0.3f && UpDownSwitch == 0)
        {
            UpDownSwitch++;
        }
        if (UpDownSwitch == 0)
        {
            UpDownTimer += Time.deltaTime;
        }
        else if (UpDownSwitch == 1)
        {
            UpDownTimer -= Time.deltaTime;
        }
        RandTimer += Time.deltaTime;
    }
}
