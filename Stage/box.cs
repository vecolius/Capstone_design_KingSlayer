using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    // Start is called before the first frame update
    void Start()
    {
        box1 = transform.GetChild(0).gameObject;
        box2 = transform.GetChild(1).gameObject;
        box3 = transform.GetChild(2).gameObject;

        box1.SetActive(false);
        box2.SetActive(false);
        box3.SetActive(false);
    }
}
