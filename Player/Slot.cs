using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public ItemStat item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImg;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

}
