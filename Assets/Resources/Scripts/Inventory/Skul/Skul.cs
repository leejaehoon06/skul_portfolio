using UnityEngine;

[CreateAssetMenu(menuName = "Skul/Skul", order = 0)]
public class Skul : ScriptableObject
{
    public string skulName;
    public Sprite skulImage;
    public Sprite skulInvenIcon;
    public Sprite skulMainIcon;
    public Sprite skulSubIcon;
    public bool skulPowerDash;
    [TextArea]
    public string skulExpl;
    [TextArea]
    public string skulInfo;
    public Rare skulRare;
    public string skulType;
    public Skill[] skulSkills = new Skill[4];
    public string skulSwitch;
    [TextArea]
    public string skulSwitchInfo;
    public PlayerCode unitCode;
    public GameObject skulObj;
    public GameObject playerObj;
}
