using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player_Move Move;
    public ItemStat itemstat;
    public SpriteRenderer image;

    public void SetItem(ItemStat _item)
    {
        itemstat.itemName = _item.itemName;
        itemstat.itemImg = _item.itemImg;
        itemstat.itemType = _item.itemType;
        itemstat.Hp = _item.Hp;
        itemstat.Str = _item.Str;
        itemstat.Dex = _item.Dex;
        itemstat.Int = _item.Int;
        itemstat.Luck = _item.Luck;
        itemstat.Serial_Number = _item.Serial_Number;

        image.sprite = _item.itemImg;
    }
    public ItemStat Getitem()
    {
        return itemstat;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Move = GameManager.instance.Player.GetComponent<Player_Move>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
