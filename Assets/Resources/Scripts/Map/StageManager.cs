using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager current;
    public int[] stage { get; set; } = new int[2];
    public int maps { get; set; } = 1;
    void Awake()
    {
        current = this;
    }
    private void Start()
    {
        
    }
    void Update()
    {
        
    }
}
