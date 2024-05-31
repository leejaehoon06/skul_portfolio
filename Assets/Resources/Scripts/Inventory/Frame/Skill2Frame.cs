using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Skill2Frame : MonoBehaviour
{
    public PlayerManager[] playerManager { get; set; }
    [SerializeField]
    Skill2FrameMore skill2FrameMore;
    Skul skul;
    [SerializeField]
    Image SkulIcon;
    [SerializeField]
    Text SkulName;
    [SerializeField]
    Text SkulRare;
    [SerializeField]
    Text SkulType;
    [SerializeField]
    Text SkulExplain;
    [SerializeField]
    TextMeshProUGUI Information;
    [SerializeField]
    GameObject Detail;
    [SerializeField]
    Text SwitchName;
    [SerializeField]
    Image[] SkillImages;
    [SerializeField]
    Text[] SkillNames;
    int index = -1;
    private void Start()
    {
        playerManager = FindObjectsOfType<PlayerManager>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            skill2FrameMore.gameObject.SetActive(true);
            skill2FrameMore.FrameOn(skul);
            gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.G) && index != -1)
        {
            playerManager[0].players[index].SkillChange = !playerManager[0].players[index].SkillChange;
            FrameOn(skul);
        }
    }
    public void FrameOn(Skul _skul)
    {
        skill2FrameMore.IsOnTrue();
        skul = _skul;
        SkulIcon.sprite = skul.skulInvenIcon;
        SkulIcon.SetNativeSize();
        SkulIcon.rectTransform.pivot = new Vector2(skul.skulInvenIcon.pivot.x / SkulIcon.rectTransform.sizeDelta.x,
            skul.skulInvenIcon.pivot.y / SkulIcon.rectTransform.sizeDelta.y);
        SkulName.text = skul.skulName;
        SkulRare.text = skul.skulRare.ToString();
        SkulType.text = skul.skulType;
        SkulExplain.gameObject.SetActive(true);
        Information.gameObject.SetActive(true);
        SkulExplain.text = skul.skulExpl;
        Information.text = skul.skulInfo;
        Detail.SetActive(true);
        SwitchName.text = skul.skulSwitch;
        for(int i = 0; i < playerManager[0].players.Count; i++)
        {
            for(int j = 0; j < skul.skulSkills.Length; j++)
            {
                if (playerManager[0].players[i].skills.IndexOf(skul.skulSkills[j]) != -1)
                {
                    index = i;
                    break;
                }
            }
            if(index > 0)
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
            }
        }
        else
        {
            int j = 1;
            for (int i = 0; i < 2; i++)
            {
                SkillImages[i].sprite = playerManager[0].players[index].skills[j].skillImage;
                SkillNames[i].text = playerManager[0].players[index].skills[j].skillName;
                j--;
            }
        }
    }
}
