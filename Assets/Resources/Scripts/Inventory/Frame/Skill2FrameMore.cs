using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Skill2FrameMore : MonoBehaviour
{
    PlayerManager[] playerManager;
    Skul skul;
    [SerializeField]
    Skill2Frame skill2Frame;
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
    Image[] SkillImages;
    [SerializeField]
    Text[] SkillNames;
    [SerializeField]
    Text[] SkillCools;
    [SerializeField]
    TextMeshProUGUI[] SkillInfos;
    bool IsOn = true;
    int index = -1;
    private void Start()
    {
        playerManager = FindObjectsOfType<PlayerManager>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            skill2Frame.gameObject.SetActive(true);
            skill2Frame.FrameOn(skul);
            gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.G) && index != -1)
        {
            playerManager[0].players[index].SkillChange = !playerManager[0].players[index].SkillChange;
            FrameOn(skul);
        }
    }
    private void OnDisable()
    {
        IsOnTrue();
    }
    public void IsOnTrue()
    {
        IsOn = true;
    }
    public void FrameOn(Skul _skul)
    {
        if (IsOn == true)
        {
            skul = _skul;
            SkulExplain.gameObject.SetActive(false);
            Information.gameObject.SetActive(false);
            SkulExplain.text += skul.skulExpl;
            Information.text += skul.skulInfo;
            Detail.SetActive(false);
            SwitchName.text = skul.skulSwitch;
            SwitchInfo.text = skul.skulSwitchInfo;
            
            for (int i = 0; i < skill2Frame.playerManager[0].players.Count; i++)
            {
                for (int j = 0; j < skul.skulSkills.Length; j++)
                {
                    if (skill2Frame.playerManager[0].players[i].skills.IndexOf(skul.skulSkills[j]) != -1)
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
            if (playerManager[0].players[index].SkillChange == false)
            {
                for (int i = 0; i < 2; i++)
                {
                    SkillImages[i].sprite = playerManager[0].players[index].skills[i].skillImage;
                    SkillNames[i].text = playerManager[0].players[index].skills[i].skillName;
                    SkillCools[i].text = skill2Frame.playerManager[0].players[index].skills[i].skillCool.ToString();
                    SkillInfos[i].text = skill2Frame.playerManager[0].players[index].skills[i].skillInfo;
                }
            }
            else
            {
                int j = 1;
                for (int i = 0; i < 2; i++)
                {
                    SkillImages[i].sprite = playerManager[0].players[index].skills[j].skillImage;
                    SkillNames[i].text = playerManager[0].players[index].skills[j].skillName;
                    SkillCools[i].text = skill2Frame.playerManager[0].players[index].skills[j].skillCool.ToString();
                    SkillInfos[i].text = skill2Frame.playerManager[0].players[index].skills[j].skillInfo;
                    j--;
                }
            }
            IsOn = false;
        }
    }
}
