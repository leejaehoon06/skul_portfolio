using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public int gold {  get; set; }
    public int bone { get; set; }
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
