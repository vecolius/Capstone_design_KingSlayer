using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Player_Move Move;
    public List<int> test_item = new List<int>();
    public void Start()
    {
        Move = GameManager.instance.Player.GetComponent<Player_Move>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Move.Ischeck)
        {
            test_item.Add(1);
            SceneManager.LoadScene("ending");
        }
    }
}
