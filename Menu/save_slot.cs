using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class save_slot : MonoBehaviour
{   
    //변수 선언
    
    public int numbers;
    public GameObject Save_Pop, Just_Pop;
    public Text[] slotText;
    public bool[] savefile = new bool[3];
    public bool check_bool = false;
    public GameObject optionmenu;
    public GameObject ExitMenu;
    public GameObject playMenu;
    public GameObject deleteUI;
    public bool activate_slot = false;
    // Start is called before the first frame update
    void Awake()
    {  
         
        // 데이터 슬롯 파일 존재 유무 
        for (int i = 0; i < 3; i++){
            if (File.Exists(DateManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DateManager.instance.nowSlot = i;
                DateManager.instance.LoadData();
                slotText[i].text = "진행중인 게임 슬롯";
            } else{
                slotText[i].text = "비어있는 게임 슬롯";
            }
        }
        DateManager.instance.DataClear();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(ControlManager.instance.inputs[8])){
                Debug.Log("esc");
                if(check_bool == true){
                Activate(false);
                Saveactivate(false);
                }
            }
        
    }
    // 슬롯 버튼 활성화 함수
    public void Slot(int number)
    {
        activate_slot = !activate_slot;
        playMenu.SetActive(activate_slot);
        DateManager.instance.nowSlot = number;
        numbers = number;
        if(savefile[number]){
            Slotdata.instance.savefiles[number] = savefile[number];
            DateManager.instance.LoadData();
            Slotdata.instance.numbers=number;
            Activate(true);
        } else{
            Slotdata.instance.savefiles[number] =false;
            Saveactivate(true);
            playMenu.SetActive(false);
        }
        
        
    }

    public void Saveactivate(bool check_bool)
    {
        Save_Pop.gameObject.SetActive(check_bool);
    }
    public void Activate(bool check_bool){
        Just_Pop.gameObject.SetActive(check_bool);
    }
    public void Return(){
        Save_Pop.gameObject.SetActive(false);
    }
    public void GoNewGame(){
        Saveactivate(false);
        LoadingController.Instance.LoadScene("test");
        Time.timeScale = 0;
        Time.timeScale = 1;
    }
    public void GoGame(){
        Activate(false);
        LoadingController.Instance.LoadScene("test");
        Time.timeScale = 0;
        Time.timeScale = 1;
    }
    public void Deletedata(){
        System.IO.File.Delete(DateManager.instance.path + numbers );
        Debug.Log(DateManager.instance.path +  numbers);
        Activate(false);
         
        DateManager.instance.DataClear();
        Destroy(DateManager.instance.gameObject);

        LoadingController.Instance.LoadScene("menu");
    }
    
    public void play_btn_Clicked ()
        {       
            activate_slot = !activate_slot;
            playMenu.SetActive(activate_slot);
        }
    public void exit_btn_Clicked ()
        {   
            Application.Quit();
        }
    public void activate_slotback(){
            activate_slot = !activate_slot;
            playMenu.SetActive(activate_slot);
        }
    public void Activate_back(){
            Activate(false);
    }
    public void Delete_back(){
            deleteUI.SetActive(false);
    }
    public void Delete_on(){
        deleteUI.SetActive(true);
    }
    
}
