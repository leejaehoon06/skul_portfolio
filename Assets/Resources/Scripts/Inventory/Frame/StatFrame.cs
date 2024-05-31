using UnityEngine;

public class StatFrame : MonoBehaviour
{
    const int Hp = 0;
    const int DamageReceived = 1;
    const int Ad = 2;
    const int Ap = 3;
    const int AttackSpeed = 4;
    const int Speed = 5;
    const int ChannelSpeed = 6;
    const int SkillCd = 7;
    const int SwitchCd = 8;
    const int QuintessenceCd = 9;
    const int CriC = 10;
    const int CriD = 11;

    [SerializeField]
    PlayerManager player; //추후에 자동으로 플레이어 들어오게 변경
    [SerializeField]
    GameObject[] Inscriptions; //추후에 각인 기능 추가
    [SerializeField]
    UnityEngine.UI.Text[] StatNums;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void FrameOn()
    {
        StatNums[Hp].text = Mathf.Ceil(player.playerStat.MaxHp).ToString();
        StatNums[DamageReceived].text = "x" + player.playerStat.DamageReceived.ToString("0.00");
        StatNums[Ad].text = player.playerStat.Ad.ToString("0");
        StatNums[Ap].text = player.playerStat.Ap.ToString("0");
        StatNums[AttackSpeed].text = player.playerStat.AttackSpeed.ToString("0");
        StatNums[Speed].text = player.playerStat.Speed.ToString("0");
        StatNums[ChannelSpeed].text = player.playerStat.ChannelSpeed.ToString("0");
        StatNums[SkillCd].text = player.playerStat.SkillCd.ToString("0");
        StatNums[SwitchCd].text = player.playerStat.SwitchCd.ToString("0");
        StatNums[QuintessenceCd].text = player.playerStat.QuintessenceCd.ToString("0");
        StatNums[CriC].text = player.playerStat.CriC.ToString("0");
        StatNums[CriD].text = "x" + player.playerStat.CriD.ToString("0.00");
    }
}
