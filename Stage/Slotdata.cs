using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slotdata : MonoBehaviour
{
    public static Slotdata instance = null;
    public bool[] savefiles = new bool[3];
    public int numbers;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
   
}
