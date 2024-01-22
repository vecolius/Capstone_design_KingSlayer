using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ending : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f,10f)]
    private float fadeTime;
    [SerializeField]
    private AnimationCurve fadeCurve;
    private Image image;
    private Text text1, text2;
    private Color color_I,color_T;
    public static ending instance = null;

    private void Awake(){
        if (null == instance){
            instance = this;
        } else{
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        text1 = transform.GetChild(0).GetComponent<Text>();
        text2 = transform.GetChild(1).GetComponent<Text>();
        color_T = Color.white;
        Fadeset();
    }
    
    public void Fadeset(){
        color_I.a= 0;
        color_T.a= 255;
        image.color = color_I;
        text1.color = color_T;
        text2.color = color_T;
        StartCoroutine(Fade(1,0));
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            DateManager.instance.nowPlayer.Clear_Count += DateManager.instance.nowPlayer.Clear_Count;
            DateManager.instance.nowPlayer.Player_MaxHp = DateManager.instance.nowPlayer.Hp * 20 + 20; 
            DateManager.instance.nowPlayer.Player_Hp = 20;
            DateManager.instance.nowPlayer.Max_Stamina = 3;
            DateManager.instance.nowPlayer.Exp = 0;
            DateManager.instance.nowPlayer.Stamina = 3;
            DateManager.instance.nowPlayer.Level = 0;
            DateManager.instance.nowPlayer.Str = 0;
            DateManager.instance.nowPlayer.Hp = 0;
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
            DateManager.instance.nowPlayer.items.Clear();
            DateManager.instance.nowPlayer.seed.Clear();
            DateManager.instance.SaveData();
            Destroy(InventoryManager.instance.gameObject);
            Destroy(GameManager.instance.gameObject);
            Destroy(MapManager.instance.gameObject);
            Destroy(ControlManager.instance.gameObject);
            Destroy(Slotdata.instance.gameObject);
            SceneManager.LoadScene("menu");
                
            }
    }

    private IEnumerator Fade(float start, float end){
        float currentTime = 0.0f;
        float percent = 0.0f;
        while(percent<1){
            currentTime += Time.deltaTime;
            percent = currentTime/ fadeTime;
            color_I.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            color_T.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color_I;
            text1.color = color_T;
            text2.color = color_T;
            yield return null;

        }
    }
}
