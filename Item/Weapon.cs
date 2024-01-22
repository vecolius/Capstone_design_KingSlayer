using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player_State State;
    public Player_Attack Atk;
    public ItemStat itemstat;
    public SpriteRenderer image;
    public bool income = false;
    public AudioClip clip;
    public Player_Inventory inven;
    public int grade;
    public ItemDatabase loadWeapon;

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
    }

    void Start()
    {
        State = GameManager.instance.Player.GetComponent<Player_State>();
        Atk = GameManager.instance.Player.GetComponent<Player_Attack>();
        image = GameManager.instance.Weapon.GetComponent<SpriteRenderer>();
        inven = GameManager.instance.Player.GetComponent<Player_Inventory>();
        if(itemstat.Serial_Number > 100 && itemstat.Serial_Number < 200)
        {
            grade = 0;
        }
        else if(itemstat.Serial_Number >= 200 && itemstat.Serial_Number < 300)
        {
            grade = 1;
        }
        else if(itemstat.Serial_Number >= 300 && itemstat.Serial_Number < 400)
        {
            grade = 2;
        }
        else if(itemstat.Serial_Number >= 400 && itemstat.Serial_Number < 500)
        {
            grade = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(income && Input.GetKeyDown(ControlManager.instance.inputs[4]))
        {
            SoundManager.instance.SFXplay("템먹기", clip);
            switch (gameObject.tag) {
                case "Sword" :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Sword;
                State.W_Hp = itemstat.Hp;
                State.W_Str = itemstat.Str;
                State.W_Dex = itemstat.Dex;
                State.W_Int = itemstat.Int;
                State.W_Luck = itemstat.Luck;
                break;


                case "Bow" :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Bow;
                State.W_Hp = itemstat.Hp;
                State.W_Str = itemstat.Str;
                State.W_Dex = itemstat.Dex;
                State.W_Int = itemstat.Int;
                State.W_Luck = itemstat.Luck;
                break;
                case "Fan" :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Fan;
                State.W_Hp = itemstat.Hp;
                State.W_Str = itemstat.Str;
                State.W_Dex = itemstat.Dex;
                State.W_Int = itemstat.Int;
                State.W_Luck = itemstat.Luck;
                break;
                case "God" :
                Atk.My_Weapon = Player_Attack.Weapon_Types.God;
                State.W_Hp = itemstat.Hp;
                State.W_Str = itemstat.Str;
                State.W_Dex = itemstat.Dex;
                State.W_Int = itemstat.Int;
                State.W_Luck = itemstat.Luck;
                State.Move_Speed = 10;
                break;                
            }
            image.sprite = itemstat.itemImg;
            DateManager.instance.nowPlayer.weapon = itemstat.Serial_Number;
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            income = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            income = false;
        }
    }
}
