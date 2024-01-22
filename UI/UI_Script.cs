using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Script : MonoBehaviour
{
    public Sprite[] skill_image;   
    public Slider hpbar, stmbar, expbar, boss_hpbar;
    public Text Now_hp,Now_stm, Now_coin;
    public Text[] Select_1, Select_2, Select_3;
    public Button Stat_Str, Stat_Dex, Stat_Int, Stat_Luck, Stat_Hp;
    bool die = false;
    public Player_State State;
    public GameObject boss_hp;
    bool alive_boss= false;
    Stage stage;
    GameObject monster;
    Monster_State boss;
    Player_Attack Attack;
    public Image Skill_icon,icon;
    bool cool_check = true;
    public float time = 0;


    // Start is called before the first frame update
    void Start()
    {   
        State = GameManager.instance.Player.GetComponent<Player_State>();
        Attack = GameManager.instance.Player.GetComponent<Player_Attack>();
        icon.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {   
 
        switch(Attack.My_Weapon){
            case Player_Attack.Weapon_Types.Sword:
                Skill_icon.sprite = skill_image[0];
                break;
            case Player_Attack.Weapon_Types.Bow:
                Skill_icon.sprite = skill_image[1];
                break;
            case Player_Attack.Weapon_Types.Fan:
                Skill_icon.sprite = skill_image[2];
                break;
        }
        if(Input.GetMouseButtonUp(1) && cool_check == true ){
            cool_check = false;
            StartCoroutine(CoolTime(0));


        }
        
        if(die == false){
            float Hp_slider = DateManager.instance.nowPlayer.Player_Hp / State.P_State.Max_Hp ;
            hpbar.value = Hp_slider;
            float player_now_Hp = DateManager.instance.nowPlayer.Player_Hp;
            if (DateManager.instance.nowPlayer.Player_Hp < player_now_Hp){
                HandleHp();
                player_now_Hp = DateManager.instance.nowPlayer.Player_Hp;
            }
            float Stm_slider = DateManager.instance.nowPlayer.Stamina / DateManager.instance.nowPlayer.Max_Stamina;
            float Exp_slider = State.EXP / State.Max_Exp;
            stmbar.value = Stm_slider;
            expbar.value = Exp_slider;
            float player_now_Stamina = DateManager.instance.nowPlayer.Stamina;
            if(DateManager.instance.nowPlayer.Stamina<player_now_Stamina){
                HandleStm();
                player_now_Stamina = DateManager.instance.nowPlayer.Stamina;
            } 
            HandleExp();
            Now_hp.text =DateManager.instance.nowPlayer.Player_Hp.ToString() +"/"+ State.P_State.Max_Hp.ToString() ;
            Now_stm.text = DateManager.instance.nowPlayer.Stamina.ToString();
            Now_coin.text = State.coin.ToString();

        }
        if(DateManager.instance.nowPlayer.Stage_number == 9 ){

            if(DateManager.instance.nowPlayer.monster_clear == false){
                stage = MapManager.instance.PrefabObject[DateManager.instance.nowPlayer.Stage_number].GetComponent<Stage>();
                monster = stage.transform.GetChild(3).gameObject;
                boss = monster.transform.GetChild(0).GetComponent<Monster_State>();
                float boss_HP_slider = boss.Monster_Hp / boss.Monster_Maxhp;
                boss_hpbar.value = boss_HP_slider;
                alive_boss = true;
                boss_hp.SetActive(alive_boss);
            }else{
                alive_boss = false;
                boss_hp.SetActive(alive_boss);
            }
            
  
            
        }else{
            alive_boss = false;
            boss_hp.SetActive(alive_boss);
        }
        
        if(DateManager.instance.nowPlayer.Player_Hp <= 0){
            die = true;
        } else {
            die = false;
        }
        

    }
    void HandleHp(){
        float Hp_slider =DateManager.instance.nowPlayer.Player_Hp / State.P_State.Max_Hp  ;
        hpbar.value = Mathf.Lerp(hpbar.value, Hp_slider, Time.deltaTime * 10f);
    }
    void HandleStm(){
        float Stm_slider = DateManager.instance.nowPlayer.Stamina / DateManager.instance.nowPlayer.Max_Stamina ;
    }
    void HandleExp(){
        float Exp_slider = State.EXP / DateManager.instance.nowPlayer.Max_Exp ;
        expbar.value = Mathf.Lerp(expbar.value, Exp_slider, Time.deltaTime * 10f);
    }

    IEnumerator CoolTime(float cool){
        while(cool<Attack.Skill_cool){
            cool/*이게 스킬 쿨*/ += 1*Time.deltaTime;//초단위로 짜름
            icon.fillAmount =  (Attack.Skill_cool- cool)/Attack.Skill_cool;
            yield return new WaitForFixedUpdate();
        }
        icon.fillAmount = 0;
        cool_check = true;
        
    }
}




