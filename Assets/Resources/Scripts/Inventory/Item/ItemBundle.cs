using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemBundle", order = 1)]
public class ItemBundle : ScriptableObject
{
    public Item[] items;
}
