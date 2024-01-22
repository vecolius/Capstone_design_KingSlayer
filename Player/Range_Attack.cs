using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private Player_State State = null;
    private Player_Attack Atk = null;
    public bool isForce = false;
    public bool isThunder = false;
    void Awake () {
    }
    void Start()
    {
        State = GameManager.instance.Player.GetComponent<Player_State>();//GameObject.Find("Player").GetComponent<Player_State>();
        Atk = GameManager.instance.Player.GetComponent<Player_Attack>();
        if(isThunder == true) Destroy(gameObject, 0.1f);
        else Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (Atk.My_Weapon) {
            case Player_Attack.Weapon_Types.Sword :
            if(other.gameObject.tag == "Monster") {
                other.GetComponent<Monster_State>().P_Damage(2.0f);
            }
            if(other.gameObject.tag == "Ground") {
                Destroy(gameObject);
            }
            break;            
            case Player_Attack.Weapon_Types.Bow :
            if(other.gameObject.tag == "Monster") {
                if(isForce == true) {
                    other.GetComponent<Monster_State>().P_Damage(1.2f);
                    Destroy(gameObject,1.0f);
                }
                else {
                    other.GetComponent<Monster_State>().P_Damage(0.8f);
                    Destroy(gameObject);
                }
            }
            if(other.gameObject.tag == "Ground") {
                Destroy(gameObject);
            }
            break;

            case Player_Attack.Weapon_Types.Fan :
            if(other.gameObject.tag == "Monster") {
                if(isThunder == true ) {
                    other.GetComponent<Monster_State>().M_Damage(10.0f);
                    Destroy(gameObject,0.1f);
                }else other.GetComponent<Monster_State>().M_Damage(0.5f);
            }
            if(other.gameObject.tag == "Ground") {
                Destroy(gameObject);
            }            
            break;
            case Player_Attack.Weapon_Types.God:
            if(other.gameObject.tag == "Monster") {
                other.GetComponent<Monster_State>().P_Damage(100.0f);
                Destroy(gameObject,1.0f);
            }            
            break;
        }
    }
    
}
