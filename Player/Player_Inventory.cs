using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//재욱이 34번줄92번줄 시리얼 넘버 리스트 저장
public class Player_Inventory : MonoBehaviour
{
    public static Player_Inventory instance; //싱글톤 7~16
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public delegate void OnSlotCountChange(int val); //슬롯카운트 대리자 정의
    public OnSlotCountChange onSlotCountChange; // 슬롯 대리자 인스턴스
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public delegate void OnChangeWeapon();
    public OnChangeWeapon onChangeWeapon;
    public SpriteRenderer eWeapon;


    
    public Player_Move Move;
    public Item fielditem;
    public List<ItemStat> items = new List<ItemStat>();
    public List<int> savelist = new List<int>(); // 시리얼넘버저장 리스트
    public List<int> savedata= new List<int>();

    public SpriteRenderer weapon;

    private int slotCnt; //슬롯 개수를 선언하는 변수

    public int SlotCnt
    {
        get=>slotCnt;
        set{
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    //악세서리 필요 변수
    public Accessory accessory;
    public bool get_accessory = false;
    public bool income_acc = false;

    //악세서리 저장 필요 변수
    public GameObject accDB;
    public ItemDatabase loadAcc;

    //악세서리 끝

    //무기공격타입 저장 하기 위한변수모임
    private Player_Attack Atk;
    void Start()
    {
        slotCnt = 40;
        Move = GameManager.instance.Player.GetComponent<Player_Move>();
        Atk = GameManager.instance.Player.GetComponent<Player_Attack>();
        loadAcc = accDB.GetComponent<ItemDatabase>();
        onChangeItem.Invoke();
        get_accessory = false;
        income_acc = false;
        if(DateManager.instance.nowPlayer.items != null)
        {
            savedata = DateManager.instance.nowPlayer.items;
        }
        
        Debug.Log(savedata);
        if(savedata.Count > 0)
        {
            for(int i = 0; i < savedata.Count; i++)
            {
                savelist.Add(savedata[i]);
                items.Add(loadAcc.itemDB[savedata[i]]);
                onChangeItem.Invoke();
            }
        }
        if(DateManager.instance.nowPlayer.weapon != null)
        {
            weapon.sprite = loadAcc.weaponDB[DateManager.instance.nowPlayer.weapon-100];
        }
        switch((DateManager.instance.nowPlayer.weapon-100)%3){
            case 0 :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Sword;
                break;
            case 1 :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Fan;
                break;
            case 2 :
                Atk.My_Weapon = Player_Attack.Weapon_Types.Bow;
                break;
        }
    }

    public bool AddItem(ItemStat _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke(); //여기 수정해야 nullreferenceException
            return true;
        }
        return false;
    }
    
   private void OnTriggerStay2D(Collider2D collision) //아이템을 인식해서 획득하여 인벤에 넣고 이미지 제거
   {
    switch (collision.tag) {
        case "Item" :
        Item item = collision.GetComponent<Item>();
            Debug.Log("획득");
            if(AddItem(item.Getitem()))  //여기 수정 nullreferenceException
                item.DestroyItem();
            Debug.Log("인벤저장");
        break;
        case "Accessory" :
        Accessory acc = collision.GetComponent<Accessory>();
        income_acc = true;
            if(get_accessory && !acc.income_shop)
            {
                items.Add(acc.Getitem());
                savelist.Add(acc.Getitem().Serial_Number); // 시리얼넘버 저장 배열에 저장
                onChangeItem.Invoke();
                get_accessory = false;
                income_acc = false;
                acc.DestroyItem();
            }
        break;
        case "Shop_Box" :
        income_acc = false;
        get_accessory = false;
        break;

    }
   }
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if(collision.tag == "Shop_Box")
    {
        income_acc = false;
        get_accessory = false;   
    }
   }
   
   void Update() 
   {
    if(income_acc && Input.GetKeyDown(ControlManager.instance.inputs[4])){
        get_accessory = true;
        income_acc = false;
    }
   }



}
