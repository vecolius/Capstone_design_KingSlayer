using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    weapon,
    accessory
}

[System.Serializable]
public class ItemStat
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImg;
    public int Hp;
    public int Str;
    public int Dex;
    public int Int;
    public int Luck;

    public int Serial_Number; //고유식별
}
