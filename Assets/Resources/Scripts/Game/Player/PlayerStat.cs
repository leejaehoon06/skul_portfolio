using UnityEngine;

public enum DamageType
{
    Ad,
    Ap,
    TrueDamage
}
public enum PlayerCode
{
    LittleBone,
    Water
}
public class PlayerStat
{
    public PlayerCode unitCode { get; }
    public float MaxHp { get; set; }
    public float CurHp { get; set; }
    public float DamageReceived { get; set; }
    public float Ad { get; set; }
    public float Ap { get; set; }
    public float AttackSpeed { get; set; }
    public float Speed { get; set; }
    public float ChannelSpeed { get; set; }
    public float SkillCd { get; set; }
    public float SwitchCd { get; set; }
    public float QuintessenceCd { get; set; }
    public float CriC { get; set; }
    public float CriD { get; set; }
    public float DamageAmp { get; set; }
    public float AdAmp {  get; set; }
    public float ApAmp { get; set; }
    public float DashCd { get; set; }
    public float AdSpeed { get; set; }
    public float SkillSpeed { get; set; }

    public PlayerStat()
    {
        MaxHp = 100f;
        CurHp = MaxHp;
        DamageReceived = 1f;
        Ad = 100f;
        Ap = 100f;
        AttackSpeed = 100f;
        Speed = 100f;
        ChannelSpeed = 100f;
        SkillCd = 100f;
        SwitchCd = 100f;
        QuintessenceCd = 100f;
        CriC = 0;
        CriD = 1.5f;
        DamageAmp = 1f;
        AdAmp = 1f;
        ApAmp = 1f;
        DashCd = 100f;
        AdSpeed = 1f;
        SkillSpeed = 1f;
    }

    public static PlayerStat SetSkulStats()
    {
        PlayerStat stat = new PlayerStat();
        return stat;
    }
    public static PlayerStat AddSkulStats(PlayerStat playerStat, PlayerCode unit1, PlayerCode unit2, int stack)
    {
        //타입 별 스탯 증가, 일부 스컬 스탯 증가 넣을 곳
        return playerStat;
    }
    public static PlayerStat MinSkulStats(PlayerStat playerStat, PlayerCode unit1, PlayerCode unit2, int stack)
    {
        //타입 별 스탯 증가량 복구, 일부 스컬 스탯 증가량 복구 넣을 곳
        return playerStat;
    }
    public PlayerStat AddItemStats(PlayerStat stat, ItemCode itemCode)
    {
        stat.CurHp += ItemStat.SetItemStats(itemCode).CurHp;
        stat.CurHp += ItemStat.SetItemStats(itemCode).MaxHp * (stat.CurHp / stat.MaxHp);
        stat.MaxHp += ItemStat.SetItemStats(itemCode).MaxHp;
        stat.DamageReceived *= ItemStat.SetItemStats(itemCode).DamageReceived;
        stat.Ad += ItemStat.SetItemStats(itemCode).Ad;
        stat.Ap += ItemStat.SetItemStats(itemCode).Ap;
        stat.AttackSpeed += ItemStat.SetItemStats(itemCode).AtackSpeed;
        stat.Speed += ItemStat.SetItemStats(itemCode).Speed;
        stat.ChannelSpeed += ItemStat.SetItemStats(itemCode).ChannelSpeed;
        stat.SkillCd += ItemStat.SetItemStats(itemCode).SkillCd;
        stat.SwitchCd += ItemStat.SetItemStats(itemCode).SwitchCd;
        stat.QuintessenceCd += ItemStat.SetItemStats(itemCode).OrbCd;
        stat.CriC += ItemStat.SetItemStats(itemCode).CriC;
        stat.CriD += ItemStat.SetItemStats(itemCode).CriD;
        return stat;
    }
    public PlayerStat MinItemStats(PlayerStat stat, ItemCode itemCode)
    {
        stat.CurHp -= ItemStat.SetItemStats(itemCode).CurHp;
        stat.MaxHp -= ItemStat.SetItemStats(itemCode).MaxHp;
        stat.DamageReceived /= ItemStat.SetItemStats(itemCode).DamageReceived;
        stat.Ad -= ItemStat.SetItemStats(itemCode).Ad;
        stat.Ap -= ItemStat.SetItemStats(itemCode).Ap;
        stat.AttackSpeed -= ItemStat.SetItemStats(itemCode).AtackSpeed;
        stat.Speed -= ItemStat.SetItemStats(itemCode).Speed;
        stat.ChannelSpeed -= ItemStat.SetItemStats(itemCode).ChannelSpeed;
        stat.SkillCd -= ItemStat.SetItemStats(itemCode).SkillCd;
        stat.SwitchCd -= ItemStat.SetItemStats(itemCode).SwitchCd;
        stat.QuintessenceCd -= ItemStat.SetItemStats(itemCode).OrbCd;
        stat.CriC -= ItemStat.SetItemStats(itemCode).CriC;
        stat.CriD -= ItemStat.SetItemStats(itemCode).CriD;
        return stat;
    }
    public static float Damaged(PlayerStat stat, MonsterStat monsterStat, float Damage)
    {
        int DamageRandom = Random.Range(8, 12);
        float FinalDamage = Damage * stat.DamageReceived * DamageRandom * 0.1f * monsterStat.DamageAmp;
        return FinalDamage;
    }
}

