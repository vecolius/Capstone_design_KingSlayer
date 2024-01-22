using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_open : MonoBehaviour
{
    public GameObject Item;
    public Player_Move Move;
    public Player_State State;
    public Animator anim;
    public bool isOpen = false;
    public int Rare;
    public GameObject[] Accessory;

    private bool income = false;
    public ItemStat ItemStat;
    private int Normal_Maxstate = 0;
    private int Rare_Maxstate = 0;
    private int Unique_Maxstate = 0;
    private Vector3 Pos;




    void Awake() {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Move = GameManager.instance.Player.GetComponent<Player_Move>();//GameObject.Find("Player").GetComponent<Player_Move>();
        State = GameManager.instance.Player.GetComponent<Player_State>();
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(income && Input.GetKeyDown(ControlManager.instance.inputs[4]))
        {
            income = false;
            anim.SetBool("Open", true);
            isOpen = true;
            if(isOpen == true) {
                Rare = Random.Range(0,100);
                StartCoroutine(ItemSpawn());
                isOpen = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")) {
            income = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player")) {
            income = false;
        }        
    }

    IEnumerator ItemSpawn() {
        yield return new WaitForSeconds(1.3f);
                if(Rare < 60) {
                    GameObject accessory = Instantiate(Accessory[Random.Range(0,10)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-1,2);
                        acc.itemstat.Str = Random.Range(-1,2);
                        acc.itemstat.Dex = Random.Range(-1,2);
                        acc.itemstat.Int = Random.Range(-1,2);
                        acc.itemstat.Luck = Random.Range(-1,2);
                        Normal_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        if(Normal_Maxstate == 1) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }
                else if (Rare < 95) {
                    GameObject accessory = Instantiate(Accessory[Random.Range(10,20)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-2,3);
                        acc.itemstat.Str = Random.Range(-2,3);
                        acc.itemstat.Dex = Random.Range(-2,3);
                        acc.itemstat.Int = Random.Range(-2,3);
                        acc.itemstat.Luck = Random.Range(-2,3);
                        Rare_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        if(Rare_Maxstate == 3) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }
                else{
                    GameObject accessory = Instantiate(Accessory[Random.Range(20,30)]);
                    Accessory acc = accessory.GetComponent<Accessory>();
                    while (Normal_Maxstate != 1) {
                        acc.itemstat.Hp = Random.Range(-3,4);
                        acc.itemstat.Str = Random.Range(-3,4);
                        acc.itemstat.Dex = Random.Range(-3,4);
                        acc.itemstat.Int = Random.Range(-3,4);
                        acc.itemstat.Luck = Random.Range(-3,4);
                        Unique_Maxstate = acc.itemstat.Hp + acc.itemstat.Str + acc.itemstat.Dex + acc.itemstat.Int + acc.itemstat.Luck;
                        if(Unique_Maxstate == 5) {
                            break;
                        }
                    }
                    accessory.transform.position = Pos;
                }
                Destroy(this.gameObject);                                        
    }
}
