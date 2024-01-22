using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stage : MonoBehaviour
{
    public static Stage instance;
    public GameObject teleport;
    public GameObject monster;
    public int monster_count;
    public bool monster_clear;
    public Player_State State; // 추가내용
    public bool roomcheck;
    public Teleport Teleport;
    public int Stage_nb = 0;

    public enum Stage_number
    {
        Stage0,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        Stage6,
        Box,
        Store,
        Boss,
    }

    public Stage_number stage_Number;

    void Start()
    {
        roomcheck=false;
        teleport = transform.GetChild(2).gameObject;
        monster = transform.GetChild(3).gameObject;
       // Teleport= GetComponent<Teleport>();
        State = GameManager.instance.Player.GetComponent<Player_State>();
        if(DateManager.instance.nowPlayer.monster_clear){// 현재 방에서 몬스터를 다 잡았을 경우
            teleport.SetActive(true);
        }

        if(stage_Number==Stage_number.Box||stage_Number == Stage_number.Store||stage_Number == Stage_number.Stage0){
            roomcheck=true;
        }
    }

    public GameObject Setstart()
    {
        return transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(!roomcheck){
            if(monster_count == 0)
            {          
                datersave();
                monster_count=-1;
            }
        }
    }
    void datersave(){
        teleport.SetActive(true);
        monster_clear = true;
        DateManager.instance.nowPlayer.monster_clear = monster_clear;
        DateManager.instance.nowPlayer.Level = State.Level;
        DateManager.instance.nowPlayer.Player_Coin = State.coin;
        DateManager.instance.nowPlayer.Exp = State.EXP;
        DateManager.instance.nowPlayer.Str = State.P_State.Str;
        DateManager.instance.nowPlayer.Dex = State.P_State.Dex;
        DateManager.instance.nowPlayer.Int = State.P_State.Int;
        DateManager.instance.nowPlayer.Luck = State.P_State.Luck;
        DateManager.instance.nowPlayer.Hp = State.P_State.Hp;
        DateManager.instance.nowPlayer.Weapon_Hp = State.W_Hp;
        DateManager.instance.nowPlayer.Weapon_Str = State.W_Str;
        DateManager.instance.nowPlayer.Weapon_Dex = State.W_Dex;
        DateManager.instance.nowPlayer.Weapon_Int = State.W_Int;
        DateManager.instance.nowPlayer.Weapon_Luck = State.W_Luck;
        DateManager.instance.nowPlayer.items = Player_Inventory.instance.savelist;
        DateManager.instance.SaveData();

    }
}
