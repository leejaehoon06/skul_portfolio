using UnityEngine;

public enum Rare
{
    �븻,
    ����,
    ����ũ,
    ��������
}
[CreateAssetMenu(menuName = "Item/Item", order = 0)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemInvenIcon;
    public Sprite itemImage;
    [TextArea]
    public string itemExpl;
    [TextArea]
    public string itemInfo;
    public Rare itemRare;
    public ItemCode itemCode;
    public ItemSkill ItemSkill;
    public Inscription[] itemInscriptions = new Inscription[2];
    public GameObject ItemObj;
}
