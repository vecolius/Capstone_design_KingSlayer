using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Potion : MonoBehaviour
{
    public Player_State state;
    public bool income = false;
    // Start is called before the first frame update
    void Start()
    {
        state = GameManager.instance.Player.GetComponent<Player_State>();
    }

    // Update is called once per frame
    void Update()
    {
        if(income)
        {
            if(state.P_State.Max_Hp - DateManager.instance.nowPlayer.Player_Hp < 10)
            {
                DateManager.instance.nowPlayer.Player_Hp = (int)state.P_State.Max_Hp;
                Destroy(gameObject);
            }
            else if((int)state.P_State.Max_Hp == DateManager.instance.nowPlayer.Player_Hp)
            {
                Destroy(gameObject);
            }
            else
            {
            DateManager.instance.nowPlayer.Player_Hp += 10;
            Destroy(gameObject);
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
}
