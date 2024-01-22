using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    Player_Inventory inven;
    public bool income = false;
    public int coin_value = 0;
    public Player_State State;

    // Start is called before the first frame update
    void Start()
    {
        State = GameManager.instance.Player.GetComponent<Player_State>();
        inven = GameManager.instance.Player.GetComponent<Player_Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(income == true)
        {
            State.coin += coin_value;
            income = false;
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            income = true;
        }
    }
}
