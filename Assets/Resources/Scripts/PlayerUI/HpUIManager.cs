using UnityEngine;
using UnityEngine.UI;

public class HpUIManager : MonoBehaviour
{
    PlayerManager playerManager;
    [SerializeField]
    Slider HpBar;
    [SerializeField]
    Text HpText;
    [SerializeField]
    Slider SubBar;
    [SerializeField]
    Text SubText;
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Update()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, playerManager.playerStat.CurHp / playerManager.playerStat.MaxHp, 0.3f);
        HpText.text = Mathf.Ceil(playerManager.playerStat.CurHp).ToString() + " / " + 
            Mathf.Ceil(playerManager.playerStat.MaxHp).ToString();
        if (SubBar.gameObject.activeSelf)
        {
            SubBar.value = Mathf.Lerp(SubBar.value, playerManager.players[0].GetComponent<SubBar>().CurBarNum 
                / playerManager.players[0].GetComponent<SubBar>().MaxBarNum, 0.3f);
            if (playerManager.players[0].GetComponent<SubBar>().BarTextBool)
            {
                SubText.text = playerManager.players[0].GetComponent<SubBar>().CurBarNum.ToString() 
                    + " / " + playerManager.players[0].GetComponent<SubBar>().MaxBarNum.ToString();
            }
        }
    }
}
