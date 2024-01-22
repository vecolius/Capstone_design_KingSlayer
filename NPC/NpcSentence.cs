using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;
    public Player_Move Move;
    public bool incomeNpc = false;
    public bool chatsys = true;
    void Start()
    {
        Move = GameManager.instance.Player.GetComponent<Player_Move>();
    }

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences,chatTr);
        chatsys = false;
    }
    void Update()
    {
        if(incomeNpc && Input.GetKeyDown(ControlManager.instance.inputs[4]) && chatsys)
        {
            TalkNpc();
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            incomeNpc = true;
            chatsys = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            incomeNpc = false;
        }
    }
}
