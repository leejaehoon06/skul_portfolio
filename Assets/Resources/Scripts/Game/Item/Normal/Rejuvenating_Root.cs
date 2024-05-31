public class Rejuvenating_Root : ItemSkill
{
    PlayerManager[] players;
    public override void GetItem()
    {
        players = FindObjectsOfType<PlayerManager>();
        players[0].delAttack += Skill;
    }
    public override void DelItem()
    {
        players[0].delAttack -= Skill;
    }
    public void Skill()
    {

    }
}
