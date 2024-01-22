using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

[System.Serializable]
public class SaveData{
    public SaveData(bool check){
        achievement_check = check;
    }
    public bool achievement_check;
}



public class FirstScene : MonoBehaviour
{
    public string path; 
	Text flashingText;
    public bool Press_Any_key= false;
    bool check = false;
    
           
    // Use this for initialization
    void Start () {
    flashingText = GetComponent<Text> ();
    StartCoroutine (BlinkText());       
    path = Application.persistentDataPath+"/achievements";
    }
    void Update(){
        if(Input.anyKeyDown){
            if(false == File.Exists(path)){
                check = true;
                Save_Achieve(true);
                Debug.Log("save");
            }else{
                SceneManager.LoadScene("menu");
            }
        }
    }
        
    public IEnumerator BlinkText(){
        while (true) {
            flashingText.text = " ";
            yield return new WaitForSeconds (.5f);
            flashingText.text = "아무 키나 누르세요";
            yield return new WaitForSeconds (.5f);
            }
    }
    void Save_Achieve(bool check){
        string path = Application.persistentDataPath+"/achievements";       
        SaveData achievements = new SaveData(check);
        string data = JsonUtility.ToJson(achievements);
        File.WriteAllText(path, data);
        SceneManager.LoadScene("menu");
    }
        
          
}

        
          


  