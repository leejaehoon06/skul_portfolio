using UnityEngine;

[CreateAssetMenu(menuName = "Skul/Skill", order = 3)]
public class Skill : ScriptableObject
{
    public Sprite skillImage;
    public string skillName;
    public float skillDamage;
    public float skillCool;
    [TextArea]
    public string skillInfo;
    public bool skillBaskiBool = true;
    public GameObject skillObj;
}
