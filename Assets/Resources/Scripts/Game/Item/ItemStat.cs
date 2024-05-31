public class ItemStat
{
    public ItemCode itemCode { get; }
    public float MaxHp { get; set; }
    public float CurHp { get; set; }
    public float DamageReceived { get; set; }
    public float Ad { get; set; }
    public float Ap { get; set; }
    public float AtackSpeed { get; set; }
    public float Speed { get; set; }
    public float ChannelSpeed { get; set; }
    public float SkillCd { get; set; }
    public float SwitchCd { get; set; }
    public float OrbCd { get; set; }
    public float CriC { get; set; }
    public float CriD { get; set; }
    public ItemStat()
    {

    }

    public static ItemStat SetItemStats(ItemCode item)
    {
        ItemStat stat = new ItemStat();
        stat.DamageReceived = 1f;
        switch (item)
        {
            case ItemCode.Carleon_Armor:
                stat.DamageReceived = 0.85f;
                break;
            case ItemCode.Carleon_Bow:
                stat.CriC = 8;
                break;
            case ItemCode.Carleon_Insignia:
                break;
            case ItemCode.Carleon_Staff:
                stat.Ap = 30;
                break;
            case ItemCode.Carleon_Sword:
                stat.Ad = 30;
                break;
            case ItemCode.Discolored_Dark:
                stat.MaxHp = 25;
                break;
            case ItemCode.Kendo_Stick:
                stat.AtackSpeed = 20;
                break;
            case ItemCode.Prayer_of_Grace:
                stat.SkillCd = 40;
                break;
            case ItemCode.Ring_of_Wind:
                stat.AtackSpeed = 15;
                stat.Speed = 15;
                break;
        }
        return stat;
    }


}

