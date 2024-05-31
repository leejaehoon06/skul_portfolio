using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    PlayerManager[] playerManager;
    [SerializeField]
    GameObject MainSkulSkill2;
    [SerializeField]
    GameObject SubSkulObj;
    [SerializeField]
    GameObject SubSkulSkill2;
    [SerializeField]
    GameObject SubBar;
    [SerializeField]
    GameObject QuintessenceObj;
    private void Start()
    {
        playerManager = FindObjectsOfType<PlayerManager>();
    }
    void Update()
    {
        MainSkul();
        SubSkul();
    }
    void MainSkul()
    {
        if (playerManager[0].players[0].skills.Count == 1)
        {
            MainSkulSkill2.gameObject.SetActive(false);
        }
        else
        {
            MainSkulSkill2.gameObject.SetActive(true);
        }
        if (playerManager[0].players[0].GetComponent<SubBar>() != null)
        {
            SubBar.gameObject.SetActive(true);
        }
        else
        {
            SubBar.gameObject.SetActive(false);
        }
    }
    void SubSkul()
    {
        if (playerManager[0].players.Count == 2)
        {
            SubSkulObj.SetActive(true);
            if (playerManager[0].players[1].skills.Count == 1)
            {
                SubSkulSkill2.gameObject.SetActive(false);
            }
            else
            {
                SubSkulSkill2.gameObject.SetActive(true);
            }
        }
        else
        {
            SubSkulObj.SetActive(false);
        }
    }
}
