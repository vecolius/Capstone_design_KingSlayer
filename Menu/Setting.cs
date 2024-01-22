using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setting : MonoBehaviour
{
    FullScreenMode screenMode; 
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum;
    // Start is called before the first frame update
    void Start()
    {
      intiUI();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    void intiUI(){
        for(int i = 0 ; i<Screen.resolutions.Length; i++){
            if(Screen.resolutions[i].width == 1024 && Screen.resolutions[i].width<= 1920){
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropdown.options.Clear();

        int optionNum =0;

        foreach( Resolution item in resolutions){
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width+"x"+item.height+"," +item.refreshRate+"hz";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width && item.height ==Screen.height){
                resolutionDropdown.value = optionNum;
               

            }
           optionNum++; 
        }
        
        resolutionDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow)? true:false;
    }
    public void DropboxOptionChange(int x){
        resolutionNum = x;
    }
    public void OkBtnClick(){
        Screen.SetResolution(resolutions[resolutionNum].width,resolutions[resolutionNum].height,screenMode);
    }
    public void FullScreenBtn(bool isFull){
        screenMode = isFull ? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed;
    }
    public void MasterSound(float val){
        SoundManager.instance.mixer.SetFloat("MasterVolume", Mathf.Log10(val)*20);
    }
}
