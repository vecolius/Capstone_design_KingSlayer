using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Price_Text : MonoBehaviour
{
    public Shop_Item shop_item;
    public TextMesh shop_price;
    public string price_text;

    // Start is called before the first frame update
    void Start()
    {
        shop_item = GetComponentInParent<Shop_Item>();
        shop_price = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        price_text = shop_item.price.ToString();
        shop_price.text = price_text;
    }
}
