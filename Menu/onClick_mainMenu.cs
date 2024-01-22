using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


    public class onClick_mainMenu : MonoBehaviour
    {   
        public GameObject optionmenu;
        public GameObject ExitMenu;
        public GameObject playMenu;
        public GameObject achievements;
        public bool activate_slot = false;
        public bool activate_Setting = false;  
        float time = 0f;    

        void Update(){
            string path = Application.persistentDataPath+"/achievements";

            if(true == File.Exists(path)){
                string data = File.ReadAllText(path);
                SaveData SaveData = JsonUtility.FromJson<SaveData>(data);
                if(SaveData.achievement_check == true){
                    achievements.SetActive(true);
                    SaveData achieve = new SaveData(false);
                    string datareset = JsonUtility.ToJson(achieve);
                    File.WriteAllText(path, datareset);
                }
            }
            while(time >4f){
                time += Time.deltaTime;
                if(time>3f){
                    achievements.SetActive(false);
                    break;
                }
                
             }
        }

        public void play_btn_Clicked ()
        {       
            activate_slot = !activate_slot;
            playMenu.SetActive(activate_slot);
            activate_slot = !activate_slot;
        }
        public void exit_btn_Clicked ()
        {   
            Application.Quit();
        }
        public void option_btn_Clicked ()
        {   
            activate_Setting = !activate_Setting;
            optionmenu.SetActive(activate_Setting);
            activate_Setting = !activate_Setting;
        }
        public void back(int i){
            if (i == 0 ){
                playMenu.SetActive(activate_slot); 
            } else if (i ==1){
                optionmenu.SetActive(activate_Setting);
            }
            
            
        }



        
    }
