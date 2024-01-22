using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public Player_State state;
    public bool income = false;
    public bool spend_Gold = false;
    public int price = 10;
    // Start is called before the first frame update
    void Start()
    {
        state = GameManager.instance.Player.GetComponent<Player_State>();
    }

    // Update is called once per frame
    void Update()
    {
        if(income && state.coin >= price) {
            spend_Gold = true;
        }
        if(income && Input.GetKeyDown(ControlManager.instance.inputs[4]) && spend_Gold)
        {
            DateManager.instance.nowPlayer.Player_Hp = (int)state.P_State.Max_Hp;
            state.coin -= price;
            Destroy(gameObject);
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
}
