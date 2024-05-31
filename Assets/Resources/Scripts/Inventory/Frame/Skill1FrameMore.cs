using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Skill1FrameMore : MonoBehaviour
{
    Skul skul;
    [SerializeField]
    Skill1Frame skill1Frame;
    [SerializeField]
    Text SkulExplain;
    [SerializeField]
    TextMeshProUGUI Information;
    [SerializeField]
    GameObject Detail;
    [SerializeField]
    Text SwitchName;
    [SerializeField]
    TextMeshProUGUI SwitchInfo;
    [SerializeField]
    Image SkillImage;
    [SerializeField]
    Text SkillName;
    [SerializeField]
    Text SkillCool;
    [SerializeField]
    TextMeshProUGUI SkillInfo;
    bool IsOn = true;
    public void IsOnTrue()
    {
        IsOn = true;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            skill1Frame.gameObject.SetActive(true);
            skill1Frame.FrameOn(skul);
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        IsOnTrue();
    }
    public void FrameOn(Skul _skul)
    {
        if (IsOn == true)
        {
            skul = _skul;
            SkulExplain.gameObject.SetActive(false);
            Information.gameObject.SetActive(false);
            Detail.SetActive(false);
            SwitchName.text = skul.skulSwitch;
            SwitchInfo.text = skul.skulSwitchInfo;
            int index = 0;
            for (int i = 0; i < skill1Frame.playerManager[0].players.Count; i++)
            {
                for (int j = 0; j < skul.skulSkills.Length; j++)
                {
                    if (skill1Frame.playerManager[0].players[i].skills.IndexOf(skul.skulSkills[j]) != -1)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > 0)
                {
                    break;
                }
            }
            SkillImage.sprite = skill1Frame.playerManager[0].players[index].skills[0].skillImage;
            SkillName.text = skill1Frame.playerManager[0].players[index].skills[0].skillName;
            SkillCool.text = skill1Frame.playerManager[0].players[index].skills[0].skillCool.ToString();
            SkillInfo.text = skill1Frame.playerManager[0].players[index].skills[0].skillInfo;
            IsOn = false;
        }
    }
}
