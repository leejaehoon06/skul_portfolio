using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Skill1Frame : MonoBehaviour
{
    public PlayerManager[] playerManager { get; set; }
    [SerializeField]
    Skill1FrameMore skill1FrameMore;
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
    Image SkillImage;
    [SerializeField]
    Text SkillName;
    private void Start()
    {
        playerManager = FindObjectsOfType<PlayerManager>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            skill1FrameMore.gameObject.SetActive(true);
            skill1FrameMore.FrameOn(skul);
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        //skill1FrameMore.gameObject.SetActive(false);
    }
    public void FrameOn(Skul _skul)
    {
        skill1FrameMore.IsOnTrue();
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
        int index = 0;
        for (int i = 0; i < playerManager[0].players.Count; i++)
        {
            for (int j = 0; j < skul.skulSkills.Length; j++)
            {
                if (playerManager[0].players[i].skills.IndexOf(skul.skulSkills[j]) != -1)
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
        SkillImage.sprite = playerManager[0].players[index].skills[0].skillImage;
        SkillName.text = playerManager[0].players[index].skills[0].skillName;
    }
}
