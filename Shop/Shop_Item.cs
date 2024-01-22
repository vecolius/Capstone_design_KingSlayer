using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item : MonoBehaviour
{
    //고민중
    public GameObject Item;
    public Player_Move Move;
    public Player_State State;
    public bool isOpen = false;
    public int Rare;
    public GameObject[] Accessory;

    public bool income = false;
    public ItemStat ItemStat;
    private int Normal_Maxstate = 0;
    private int Rare_Maxstate = 0;
    private int Unique_Maxstate = 0;
    private bool R_Control = true;
    public bool Spend_Gold = false;
    public Vector3 Pos;
    public bool in_ac = false;

    public int price = 0;
    // Start is called before the first frame update
    void Start()
    {
        State = GameManager.instance.Player.GetComponent<Player_State>();
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(R_Control)
        {
        Rare = Random.Range(0,100);
        StartCoroutine(ItemSpawn());
        R_Control = false;
        }

        if(income && Input.GetKeyDown(ControlManager.instance.inputs[4]))
        {
            if(Spend_Gold)
            {
            State.coin -=  price;
            Spend_Gold = false;
            Destroy(gameObject);
            }

        }

    }


    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("Player")) {
            income = true;
        }
        if(State.coin >= price)
        {
            Spend_Gold = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player")) {
            income = false;
        }
        Spend_Gold = false;
    }



    IEnumerator ItemSpawn() {
        yield return new WaitForSeconds(1.3f);
                if(Rare < 60) {
                    GameObject accessory = Instantiate(Accessory[Random.Range(0,0)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-1,2);
                        acc.itemstat.Str = Random.Range(-1,2);
                        acc.itemstat.Dex = Random.Range(-1,2);
                        acc.itemstat.Int = Random.Range(-1,2);
                        acc.itemstat.Luck = Random.Range(-1,2);
                        Normal_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        price = 10;
                        if(Normal_Maxstate == 1) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }
                else if (Rare < 95) {
                    GameObject accessory = Instantiate(Accessory[Random.Range(1,1)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-2,3);
                        acc.itemstat.Str = Random.Range(-2,3);
                        acc.itemstat.Dex = Random.Range(-2,3);
                        acc.itemstat.Int = Random.Range(-2,3);
                        acc.itemstat.Luck = Random.Range(-2,3);
                        Rare_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        price = 15;
                        if(Rare_Maxstate == 3) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }
                else{
                    GameObject accessory = Instantiate(Accessory[Random.Range(2,2)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-3,4);
                        acc.itemstat.Str = Random.Range(-3,4);
                        acc.itemstat.Dex = Random.Range(-3,4);
                        acc.itemstat.Int = Random.Range(-3,4);
                        acc.itemstat.Luck = Random.Range(-3,4);
                        Unique_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        price = 20;
                        if(Unique_Maxstate == 5) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }                                       
    }
}
