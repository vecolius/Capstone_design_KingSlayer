using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Player_Date {
    public float Player_MaxHp = 20 ;
    public float Player_Hp = 20;
    public float Max_Stamina = 3;
    public float Stamina = 3;
    public float Level = 0;
    public float Exp = 0;
    public int Hp = 0;
    public int Str = 0 ;
    public int Dex = 0;
    public int Int = 0;
    public int Luck = 0;
    public List<int> seed;
    public int Stage_number;
    public float Clear_Count = 0;
    public bool Clear_Check;
    public float Player_Coin = 0;
    public float Max_Exp = 100 ;  
    public bool monster_clear = false;
    public bool Restart = false;
    public List<int> items;
    public int weapon = 100;
    public int Weapon_Hp = 0;
    public int Weapon_Str = 0;
    public int Weapon_Dex = 0;
    public int Weapon_Int = 0;
    public int Weapon_Luck = 0;

}

    
public class DateManager : MonoBehaviour
{
    // 싱글론
    public static DateManager instance;

    public Player_Date nowPlayer = new Player_Date();

    public string path;
    public int nowSlot;
    // Start is called before the first frame update
    void Awake()
    {
        var objs = FindObjectsOfType<DateManager>();
        if (objs.Length == 1){
            if (instance == null)
            {
                instance = this;
            } else if(instance != this)
            {
                Destroy(instance.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        } else{
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/save";
    }
    void Start() {
        InvokeRepeating ("Stamina_Recovery",1.0f,4.0f);
        print(path);
    }

    public void SaveData(){
        nowPlayer.Max_Exp = 100 + nowPlayer.Level*20;
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData(){
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<Player_Date>(data);
    }
    public void DataClear(){
        nowSlot = -1;
        nowPlayer = new Player_Date();
    }
    void Stamina_Recovery() {
        if(nowPlayer.Stamina < nowPlayer.Max_Stamina) {
            nowPlayer.Stamina += 1;
        }
    }

    

     /*   Level += DateManager.instance.nowPlayer.Level;
        Exp += DateManager.instance.nowPlayer.Exp;
        Str += DateManager.instance.nowPlayer.Str;
        Dex += DateManager.instance.nowPlayer.Dex;
        Int += DateManager.instance.nowPlayer.Int;
        Luck += DateManager.instance.nowPlayer.Luck;*/
}