using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake() 
    {
        instance = this;
    }
    public GameObject fieldItemprefab;
    public List<ItemStat> itemDB = new List<ItemStat>();
    public List<Sprite> weaponDB = new List<Sprite>();
}
