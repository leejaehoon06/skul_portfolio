using UnityEngine;
using UnityEngine.UI;

public class SkulUIManager : MonoBehaviour
{
    Inventory inventory;
    PlayerManager[] playerManager;
    [SerializeField]
    Material SkillImageGray;
    [SerializeField]
    Image SwitchCool;
    [SerializeField]
    Image MainSkulImage;
    [SerializeField]
    Image MainSkill1Image;
    [SerializeField]
    Image MainSkill1Cool;
    [SerializeField]
    Image MainSkill2Image;
    [SerializeField]
    Image MainSkill2Cool;
    [SerializeField]
    Image SubSkulImage;
    [SerializeField]
    Image SubSkill1Image;
    [SerializeField]
    Image SubSkill1Cool;
    [SerializeField]
    Image SubSkill2Image;
    [SerializeField]
    Image SubSkill2Cool;
    [SerializeField]
    GameObject SwitchOn;
    [SerializeField]
    GameObject MainSkill1On;
    [SerializeField]
    GameObject MainSkill2On;
    [SerializeField]
    GameObject SubSkill1On;
    [SerializeField]
    GameObject SubSkill2On;
    bool SwitchOnBool = false;
    bool[] SkillOnBool = new bool[4];
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        playerManager = FindObjectsOfType<PlayerManager>();
    }
    private void Update()
    {
        UISeting();
    }
    void UISeting()
    {
        MainSkulImage.sprite = inventory.skuls[0].skulMainIcon;
        if (playerManager[0].players[0].SkillChange == false)
        {
            MainSkill1Image.sprite = playerManager[0].players[0].skills[0].skillImage;
            MainSkill1Cool.fillAmount = playerManager[0].players[0].Skill1Timer / playerManager[0].players[0].skills[0].skillCool;
            if (playerManager[0].players[0].Skill1Able == false)
            {
                MainSkill1Image.material = SkillImageGray;
            }
            else
            {
                MainSkill1Image.material = null;
            }
            if (playerManager[0].players[0].Skill1Timer <= 0 && SkillOnBool[0] == true)
            {
                MainSkill1On.SetActive(true);
                SkillOnBool[0] = false;
            }
            else if (playerManager[0].players[0].Skill1Timer > 0 && SkillOnBool[0] == false)
            {
                SkillOnBool[0] = true;
            }
        }
        if (playerManager[0].players[0].skills.Count == 2)
        {
            if (playerManager[0].players[0].SkillChange == false)
            {
                MainSkill2Image.sprite = playerManager[0].players[0].skills[1].skillImage;
                MainSkill2Cool.fillAmount = playerManager[0].players[0].Skill2Timer / playerManager[0].players[0].skills[1].skillCool;
                if (playerManager[0].players[0].Skill2Able == false)
                {
                    MainSkill2Image.material = SkillImageGray;
                }
                else
                {
                    MainSkill2Image.material = null;
                }
                if (playerManager[0].players[0].Skill2Timer <= 0 && SkillOnBool[1] == true)
                {
                    MainSkill2On.SetActive(true);
                    SkillOnBool[1] = false;
                }
                else if (playerManager[0].players[0].Skill2Timer > 0 && SkillOnBool[1] == false)
                {
                    SkillOnBool[1] = true;
                }
            }
            else
            {
                MainSkill2Image.sprite = playerManager[0].players[0].skills[0].skillImage;
                MainSkill2Cool.fillAmount = playerManager[0].players[0].Skill1Timer / playerManager[0].players[0].skills[0].skillCool;
                MainSkill1Image.sprite = playerManager[0].players[0].skills[1].skillImage;
                MainSkill1Cool.fillAmount = playerManager[0].players[0].Skill2Timer / playerManager[0].players[0].skills[1].skillCool;
                if (playerManager[0].players[0].Skill1Able == false)
                {
                    MainSkill2Image.material = SkillImageGray;
                }
                else
                {
                    MainSkill2Image.material = null;
                }
                if (playerManager[0].players[0].Skill2Able == false)
                {
                    MainSkill1Image.material = SkillImageGray;
                }
                else
                {
                    MainSkill1Image.material = null;
                }
                if (playerManager[0].players[0].Skill1Timer <= 0 && SkillOnBool[0] == true)
                {
                    MainSkill2On.SetActive(true);
                    SkillOnBool[0] = false;
                }
                else if (playerManager[0].players[0].Skill1Timer > 0 && SkillOnBool[0] == false)
                {
                    SkillOnBool[0] = true;
                }
                if (playerManager[0].players[0].Skill2Timer <= 0 && SkillOnBool[1] == true)
                {
                    MainSkill1On.SetActive(true);
                    SkillOnBool[1] = false;
                }
                else if (playerManager[0].players[0].Skill2Timer > 0 && SkillOnBool[1] == false)
                {
                    SkillOnBool[1] = true;
                }
            }
        }
        if (playerManager[0].players.Count == 2)
        {
            SwitchCool.fillAmount = playerManager[0].SwitchTimer / 5f;
            if (playerManager[0].SwitchTimer <= 0 && SwitchOnBool == true)
            {
                SwitchOn.SetActive(true);
                SwitchOnBool = false;
            }
            else if (playerManager[0].SwitchTimer > 0 && SwitchOnBool == false)
            {
                SwitchOnBool = true;
            }
            SubSkulImage.sprite = inventory.skuls[1].skulSubIcon;
            if (playerManager[0].players[1].SkillChange == false)
            {
                SubSkill1Image.sprite = playerManager[0].players[1].skills[0].skillImage;
                SubSkill1Cool.fillAmount = playerManager[0].players[1].Skill1Timer / playerManager[0].players[1].skills[0].skillCool;
                if (playerManager[0].players[1].Skill1Timer <= 0 && SkillOnBool[2] == true)
                {
                    SubSkill1On.SetActive(true);
                    SkillOnBool[2] = false;
                }
                else if (playerManager[0].players[1].Skill1Timer > 0 && SkillOnBool[2] == false)
                {
                    SkillOnBool[2] = true;
                }
                if (playerManager[0].players[1].Skill1Able == false)
                {
                    SubSkill1Image.material = SkillImageGray;
                }
                else
                {
                    SubSkill1Image.material = null;
                }
            }
            if (playerManager[0].players[1].skills.Count == 2)
            {
                if (playerManager[0].players[1].SkillChange == false)
                {
                    SubSkill2Image.sprite = playerManager[0].players[1].skills[1].skillImage;
                    SubSkill2Cool.fillAmount = playerManager[0].players[1].Skill2Timer / playerManager[0].players[1].skills[1].skillCool;
                    if (playerManager[0].players[1].Skill2Timer <= 0 && SkillOnBool[3] == true)
                    {
                        SubSkill2On.SetActive(true);
                        SkillOnBool[3] = false;
                    }
                    else if (playerManager[0].players[1].Skill2Timer > 0 && SkillOnBool[3] == false)
                    {
                        SkillOnBool[3] = true;
                    }
                    if (playerManager[0].players[1].Skill2Able == false)
                    {
                        SubSkill2Image.material = SkillImageGray;
                    }
                    else
                    {
                        SubSkill2Image.material = null;
                    }
                }
                else
                {
                    SubSkill2Image.sprite = playerManager[0].players[1].skills[0].skillImage;
                    SubSkill2Cool.fillAmount = playerManager[0].players[1].Skill1Timer / playerManager[0].players[1].skills[0].skillCool;
                    SubSkill1Image.sprite = playerManager[0].players[1].skills[1].skillImage;
                    SubSkill1Cool.fillAmount = playerManager[0].players[1].Skill2Timer / playerManager[0].players[1].skills[1].skillCool;
                    if (playerManager[0].players[1].Skill1Timer <= 0 && SkillOnBool[3] == true)
                    {
                        SubSkill2On.SetActive(true);
                        SkillOnBool[3] = false;
                    }
                    else if (playerManager[0].players[1].Skill1Timer > 0 && SkillOnBool[3] == false)
                    {
                        SkillOnBool[3] = true;
                    }
                    if (playerManager[0].players[1].Skill2Timer <= 0 && SkillOnBool[2] == true)
                    {
                        SubSkill1On.SetActive(true);
                        SkillOnBool[2] = false;
                    }
                    else if (playerManager[0].players[1].Skill2Timer > 0 && SkillOnBool[2] == false)
                    {
                        SkillOnBool[2] = true;
                    }
                    if (playerManager[0].players[1].Skill2Able == false)
                    {
                        SubSkill1Image.material = SkillImageGray;
                    }
                    else
                    {
                        SubSkill1Image.material = null;
                    }
                    if (playerManager[0].players[1].Skill1Able == false)
                    {
                        SubSkill2Image.material = SkillImageGray;
                    }
                    else
                    {
                        SubSkill2Image.material = null;
                    }
                }
            }
        }
    }
}
