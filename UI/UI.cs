using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{

    [System.Serializable]
    public class DisplayTextSlot{
        public List<Image> SlotSprite = new List<Image> ();
    }
    Player_Inventory inven;
    public GameObject level_ui;
    public GameObject menu; //인게임 메뉴
    public GameObject[] slotStatObject;
    public GameObject inventoryPanel; //인벤 및 스텟창 오브젝트
    public GameObject die_ui;
    bool activeInventory = false; //비활성화 bool변수
    bool activedie_ui = false;
    bool activemenu = false; //인게임 메뉴 비활성화 bool 변수
    bool activelevel_ui = false;
    public Player_State State = null;
    public Slot[] slots;
    public Transform slotHolder;
    public Sprite[] StatSprite;
    public DisplayTextSlot[] DisplayTextSlots;
    public Button[] Btn_slot;
    public List<int> startList = new List<int>();
    public List<int> ResultIndexList = new List<int>();
    public Image DisplayResultImage;
    public List<int> StateList = new List<int>();
    public List<int> test_item = new List<int>();
    public Player_Inventory Inventory;
    public bool Level_up = false;
    public GameObject setting;
    bool active_setting = false;
    float time;
    public Text stat_str;
    public Text stat_dex;
    public Text stat_int;
    public Text stat_luck;
    public Text stat_hp;
    public Image Die_Panel;
    public float Die_time = 0f;
    public float F_time = 1f;



    void Start()
    {
        
        inven = Player_Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        //inven.onSlotCountChange += SlotChange();
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);//인벤 및 스텟창 비활성화
        State = GameManager.instance.Player.GetComponent<Player_State>(); 
        
        stat_str.text = State.TotalStr.ToString();
        stat_dex.text = State.TotalDex.ToString();
        stat_int.text = State.TotalInt.ToString();
        stat_luck.text = State.TotalLuck.ToString();
        stat_hp.text = State.TotalHp.ToString();

    }



                
            
        

    void Update()
    {   
            time += Time.deltaTime;
        
        if(Input.GetKeyDown(ControlManager.instance.inputs[6])&& time>3){    //인벤 켜고 끄기(임시)
            if(activemenu == true){ //인게임 메뉴 켜져있음 실행 x
            }else if(activedie_ui == true){ //사망 상태창 켜져 있음 실행 x
            } else if(activelevel_ui == true){
            }else {
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }
            stat_str.text = State.TotalStr.ToString();
            stat_dex.text = State.TotalDex.ToString();
            stat_int.text = State.TotalInt.ToString();
            stat_luck.text = State.TotalLuck.ToString();
            stat_hp.text = State.TotalHp.ToString();
        }

        
        if(DateManager.instance.nowPlayer.Player_Hp <= 0){ // 사망시 데이터 초기화 
            DateManager.instance.nowPlayer.Player_MaxHp = 20; 
            DateManager.instance.nowPlayer.Max_Stamina = 3;
            DateManager.instance.nowPlayer.Stamina = 3;
            DateManager.instance.nowPlayer.Level = 0;
            DateManager.instance.nowPlayer.Str = 0;
            DateManager.instance.nowPlayer.Exp = 0;
            DateManager.instance.nowPlayer.Dex = 0;
            DateManager.instance.nowPlayer.Int = 0;
            DateManager.instance.nowPlayer.Luck = 0;
            DateManager.instance.nowPlayer.Clear_Check = false;
            DateManager.instance.nowPlayer.Stage_number=0;
            DateManager.instance.nowPlayer.Player_Coin =0;
            DateManager.instance.nowPlayer.monster_clear =false;
            DateManager.instance.nowPlayer.weapon = 100;
            DateManager.instance.SaveData();
            Time.timeScale = 0;
            activedie_ui = true;
            StartCoroutine(FadeFlow());
        }
        //인 게임 메뉴
        if(Input.GetKeyDown(ControlManager.instance.inputs[8]) && time>3){
            if(activeInventory == true){
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }else if(activedie_ui == true && level_ui == true){//사망 상태창 켜져 있음 실행 x
            }else {
                Debug.Log(activemenu);
                activemenu =!activemenu;
                menu.SetActive(activemenu);
                if(activemenu == true){
                    Time.timeScale = 0;
                } else{
                    Time.timeScale = 1;
                }
            }
        }
        if(Input.GetKeyDown(ControlManager.instance.inputs[7])){

            Restart();
        }
        /*재욱이 ㅁㄴㅇㄹ*/
        
        float test1_level = State.Level;
        
        if(DateManager.instance.nowPlayer.Level<test1_level){
                if(activedie_ui == false){
                Time.timeScale = 0;
                activelevel_ui = true;
                State.Level -= 1;
                Level_up = true;
                Debug.Log(Level_up);
                Level_slot();
                level_ui.SetActive(activelevel_ui);

                }
                    
            }
        if(DateManager.instance.nowPlayer.monster_clear == true){
            if(Level_up == true){
                State.Level +=1;
                Level_up = false;
                DateManager.instance.nowPlayer.Level = State.Level;
            }
        }
    
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i=0; i<inven.items.Count;i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
    public void Restart(){ // 사망 ui 리스타트 버튼
            DateManager.instance.nowPlayer.Restart = true;
            test_item.Add(1);
            DateManager.instance.nowPlayer.items = test_item;
            DateManager.instance.nowPlayer.items.Clear();
            DateManager.instance.nowPlayer.Player_MaxHp = 20; 
            DateManager.instance.nowPlayer.Player_Hp = 20 ;
            DateManager.instance.nowPlayer.Max_Stamina = 3;
            DateManager.instance.nowPlayer.Stamina = 3;
            DateManager.instance.nowPlayer.Level = 0;
            DateManager.instance.nowPlayer.Hp = 0;
            DateManager.instance.nowPlayer.Str = 0;
            DateManager.instance.nowPlayer.Exp = 0;
            DateManager.instance.nowPlayer.Dex = 0;
            DateManager.instance.nowPlayer.Int = 0;
            DateManager.instance.nowPlayer.Luck = 0;
            DateManager.instance.nowPlayer.Clear_Check = false;
            DateManager.instance.nowPlayer.Stage_number=0;
            DateManager.instance.nowPlayer.Player_Coin =0;
            DateManager.instance.nowPlayer.monster_clear =false;
            DateManager.instance.nowPlayer.weapon = 100;
            DateManager.instance.nowPlayer.Weapon_Hp = 0;
            DateManager.instance.nowPlayer.Weapon_Str = 0;
            DateManager.instance.nowPlayer.Weapon_Dex = 0;
            DateManager.instance.nowPlayer.Weapon_Int = 0;
            DateManager.instance.nowPlayer.Weapon_Luck = 0;
            DateManager.instance.nowPlayer.seed.Clear();
            DateManager.instance.SaveData();
            State.EXP = DateManager.instance.nowPlayer.Exp;
            State.coin = DateManager.instance.nowPlayer.Player_Coin;
            State.Level = DateManager.instance.nowPlayer.Level;
            time = 0;
            Destroy(InventoryManager.instance.gameObject);
            Destroy(GameManager.instance.gameObject);
            Destroy(MapManager.instance.gameObject);
            SceneManager.LoadScene("Test");
            Time.timeScale = 0;
            Time.timeScale = 1;
            
    }
    public void Menu_Main(){
        Debug.Log("click");
        DateManager.instance.SaveData();
        LoadingController.Instance.LoadScene("menu");
        Destroy(InventoryManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(MapManager.instance.gameObject);
        Destroy(ControlManager.instance.gameObject);
        Destroy(Slotdata.instance.gameObject);
    }

    public void Die_Main(){
            int numbers = Slotdata.instance.numbers;
            test_item.Add(1);
            DateManager.instance.nowPlayer.items = test_item;
            DateManager.instance.nowPlayer.items.Clear();
            DateManager.instance.nowPlayer.Player_MaxHp = 20; 
            DateManager.instance.nowPlayer.Player_Hp =20;
            DateManager.instance.nowPlayer.Max_Stamina = 3;
            DateManager.instance.nowPlayer.Stamina = 3;
            DateManager.instance.nowPlayer.Level = 0;
            DateManager.instance.nowPlayer.Hp = 0;
            DateManager.instance.nowPlayer.Str = 0;
            DateManager.instance.nowPlayer.Exp = 0;
            DateManager.instance.nowPlayer.Dex = 0;
            DateManager.instance.nowPlayer.Int = 0;
            DateManager.instance.nowPlayer.Luck = 0;
            DateManager.instance.nowPlayer.Clear_Check = false;
            DateManager.instance.nowPlayer.Stage_number=0;
            DateManager.instance.nowPlayer.Player_Coin =0;
            DateManager.instance.nowPlayer.monster_clear =false;
            DateManager.instance.nowPlayer.weapon = 100;
            DateManager.instance.nowPlayer.Weapon_Hp = 0;
            DateManager.instance.nowPlayer.Weapon_Str = 0;
            DateManager.instance.nowPlayer.Weapon_Dex = 0;
            DateManager.instance.nowPlayer.Weapon_Int = 0;
            DateManager.instance.nowPlayer.Weapon_Luck = 0;

            DateManager.instance.nowPlayer.seed.Clear();
            DateManager.instance.SaveData();
            Destroy(InventoryManager.instance.gameObject);
            Destroy(GameManager.instance.gameObject);
            Destroy(MapManager.instance.gameObject);
            Destroy(ControlManager.instance.gameObject);
            Destroy(Slotdata.instance.gameObject);
            SceneManager.LoadScene("menu");
            Debug.Log("isClicked");
    }
    public void del_pop_up(){
        activemenu = !activemenu;
        menu.SetActive(activemenu);
        Time.timeScale = 1;
    }

    public void On_Click_setting(){
        active_setting = !active_setting;
        setting.SetActive(active_setting);
        
    }
    public void Level_Click(int index){
        DisplayResultImage.sprite = StatSprite[ResultIndexList[index]];
        if(ResultIndexList[index] == 0){
            State.P_State.Hp += 1;
        }else if(ResultIndexList[index] == 1){
            State.P_State.Str += 1;
        }else if(ResultIndexList[index] == 2){
            State.P_State.Dex += 1;
        }else if(ResultIndexList[index] == 3){
            State.P_State.Int += 1;
        }else if (ResultIndexList[index] == 4){
            State.P_State.Luck += 1;
        }
        startList.Clear();
        ResultIndexList.Clear();
        activelevel_ui = !activelevel_ui;
        level_ui.SetActive(activelevel_ui);
        Debug.Log(activelevel_ui);
        Time.timeScale = 1;
    }
    public void Level_slot(){
        if(startList.Count<5){
            for (int i = 0 ; i < 5; i++){
                startList.Add(i);
            } 
            if(ResultIndexList.Count<3) 
            for (int i = 0; i < 3; i++){
                int randomIndex = Random.Range(0,startList.Count);
                ResultIndexList.Add(startList[randomIndex]);
                DisplayTextSlots[i].SlotSprite[0].sprite = StatSprite[startList[randomIndex]];
                startList.RemoveAt(randomIndex);
            }
            
        }
        
    }
    public void MasterSound(float val){
        SoundManager.instance.mixer.SetFloat("MasterVolume", Mathf.Log10(val)*20);
    }


    IEnumerator FadeFlow(){
        Die_Panel.gameObject.SetActive(true);
        Color alpha = Die_Panel.color;
        while(alpha.a<1f){
            Die_time += Time.deltaTime /F_time;
            alpha.a = Mathf.Lerp(0,1,Die_time);
            Die_Panel.color = alpha;
            yield return null;

        }
    }
    
}

