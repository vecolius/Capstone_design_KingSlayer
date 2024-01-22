using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    public Player_State State;
    public ItemStat itemstat;
    public SpriteRenderer image;
    public Player_Inventory inventory;

    public bool income;
    public bool income_shop;
    public AudioClip clip;


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
        State = GameManager.instance.Player.GetComponent<Player_State>();
        inventory = GameManager.instance.Player.GetComponent<Player_Inventory>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(income && Input.GetKeyDown(ControlManager.instance.inputs[4]) && !income_shop)
        {
            SoundManager.instance.SFXplay("템먹기", clip);
            State.P_State.Hp += itemstat.Hp;
            State.P_State.Str += itemstat.Str;
            State.P_State.Dex += itemstat.Dex;
            State.P_State.Int += itemstat.Int;
            State.P_State.Luck += itemstat.Luck;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player" :
                income = true;
            break;
            
            case "Shop_Box":
                income_shop = true;
            break;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player" :
                income = false;
            break;
            
            case "Shop_Box" :
                income_shop = false;
            break;
        }
    }
}
