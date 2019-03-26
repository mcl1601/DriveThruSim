using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order
{
    public List<Item> items;
    public List<Item> completedItems;
    public float timer;
    private float totalTime;
    public GameObject ticketUI, bag, bagUI;

    public Order(List<Item> i, float time)
    {
        items = i;
        timer = time;
        totalTime = time;
        completedItems = new List<Item>();
    }

    public bool UpdateTimer()
    {
        timer -= Time.deltaTime;
        float percent = timer / totalTime;
        ticketUI.transform.GetChild(2).GetComponent<Image>().fillAmount = percent;
        return timer < 0;
    }
}
