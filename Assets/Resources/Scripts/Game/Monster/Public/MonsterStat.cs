using UnityEngine;

public enum MonsterCode
{
    Dummy,
    CarleonRecruit,
    CarleonArcher,
    CarleonManAtArms,
    Ent,
    RootEnt,
    BlossomEnt,
    GiganticEnt,
    FlameWizard,
    GlacialWizard,
    CarleonAssasin,
    AdventureHero,
    AdventureWarrior,
    AdventureHunter,
    AdventureThief,
    AdventureMage,
    AdventureCleric
}
public class MonsterStat
{
    public MonsterCode monsterCode { get; }
    public float MaxHp { get; set; }
    public float CurHp { get; set; }
    public float DamageAmp { get; set; }


    public MonsterStat(float MaxHp)
    {
        this.MaxHp = MaxHp;
        CurHp = MaxHp;
        DamageAmp = 1f;
    }

    public static MonsterStat SetMonsterStats(MonsterCode monster)
    {
        MonsterStat stat = null;
        switch (monster)
        {
            case MonsterCode.CarleonRecruit:
                stat = new MonsterStat(40f);
                break;
            case MonsterCode.CarleonArcher:
                stat = new MonsterStat(35f);
                break;
            case MonsterCode.CarleonManAtArms:
                stat = new MonsterStat(150f);
                break;
            case MonsterCode.Ent:
                stat = new MonsterStat(55f);
                break;
            case MonsterCode.RootEnt:
                stat = new MonsterStat(45f);
                break;
            case MonsterCode.BlossomEnt:
                stat = new MonsterStat(55f);
                break;
            case MonsterCode.GiganticEnt:
                stat = new MonsterStat(200f);
                break;
            case MonsterCode.FlameWizard:
                stat = new MonsterStat(35f);
                break;
            case MonsterCode.GlacialWizard:
                stat = new MonsterStat(35f);
                break;
            case MonsterCode.Dummy:
                stat = new MonsterStat(1000000000);
                break;
            case MonsterCode.AdventureHero:
                stat = new MonsterStat(1160);
                break;
        }
        return stat;
    }

    public static float Damaged(MonsterStat stat, PlayerStat playerStat, float Damage, DamageType damageType, bool CriChance)
    {
        int DamageRandom = Random.Range(8, 12);
        float FinalDamage = 0;
        if(damageType == DamageType.TrueDamage)
        {
            return Damage;
        }
        if (CriChance)
        {
            if (damageType == DamageType.Ad)
            {
                FinalDamage = Damage * (playerStat.Ad / 100) * playerStat.AdAmp * DamageRandom * 0.1f * playerStat.CriD;
            }
            else if (damageType == DamageType.Ap)
            {
                FinalDamage = Damage * (playerStat.Ap / 100) * playerStat.ApAmp * DamageRandom * 0.1f * playerStat.CriD;
            }
        }
        else
        {
            if (damageType == DamageType.Ad)
            {
                FinalDamage = Damage * (playerStat.Ad / 100) * playerStat.AdAmp * DamageRandom * 0.1f;
            }
            else if (damageType == DamageType.Ap)
            {
                FinalDamage = Damage * (playerStat.Ap / 100) * playerStat.ApAmp * DamageRandom * 0.1f;
            }
        }
        return FinalDamage;
    }

}

