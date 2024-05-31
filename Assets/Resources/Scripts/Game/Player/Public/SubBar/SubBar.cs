using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBar : MonoBehaviour
{
    [SerializeField]
    float _MaxBarNum;
    public float MaxBarNum
    {
        get { return _MaxBarNum; }
        set { _MaxBarNum = value; }
    }
    [SerializeField]
    float _CurBarNum;
    public float CurBarNum
    {
        get { return _CurBarNum; }
        set 
        { 
            _CurBarNum = value;
        }
    }
    [SerializeField]
    bool _BarTextBool;
    public bool BarTextBool { get { return _BarTextBool; } set {  _BarTextBool = value; } }
}
