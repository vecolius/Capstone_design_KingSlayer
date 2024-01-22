using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;
    public GameObject Item;
    public GameObject Weapon;
    // Start is called before the first frame update
    void Start()
    {
        if (null == instance) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
