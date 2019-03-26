﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Burger, Fry, Drink
}

public class OrderManager : MonoBehaviour
{
    public GameObject orderTicketUI, bagPre, bagUI;
    public float bagSpacing = 0.125f;
    public float bagWidth = 0.5f;

    private List<Order> orders;     // collection of Order Objects
    private Transform ticketParent; // where to instantiate the order tickets
    List<Order> toRemove;

    // Start is called before the first frame update
    void Start()
    {
        orders = new List<Order>();
        toRemove = new List<Order>();
        ticketParent = GameObject.Find("TicketParent").transform;

        // For testing, create an inital order
        List<Item> items = new List<Item>
        {
            Item.Burger,
            Item.Fry,
            Item.Drink
        };
        // create two orders
        Order o1 = new Order(items, 10f);
        CreateOrderTicket(o1);
        //Order o2 = new Order(new List<Item> { Item.Fry }, 5f);
        //CreateOrderTicket(o2);
    }

    void CreateOrderTicket(Order o)
    {
        // instantiate the ticket
        GameObject g = Instantiate(orderTicketUI, ticketParent);
        // set the text of the ticket
        Text t = g.transform.GetChild(0).GetComponent<Text>();
        foreach(Item i in o.items)
        {
            t.text += i.ToString() + "\n";
        }
        // set properties
        o.ticketUI = g;
        // create the bag
        AddBagToStation(o);
        // add the order to the list
        orders.Add(o);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOrders();
    }

    void UpdateOrders()
    {
        // update the timer on each bag, remove if necessary
        foreach(Order o in orders)
        {
            if(o.UpdateTimer())
            {
                // save this order to be deleted later
                toRemove.Add(o);
            }
        }
        // delete order objects and their gameobjects
        foreach(Order o in toRemove)
        {
            DeleteOrder(o);
        }
    }

    void DeleteOrder(Order o)
    {
        Destroy(o.bag);
        Destroy(o.bagUI);
        Destroy(o.ticketUI);
        UpdateBags(orders.IndexOf(o));
        orders.Remove(o);
    }

    void AddBagToStation(Order o)
    {
        // get half width of counter
        float width = transform.localScale.x * 0.5f;
        // how many bags to render
        float numBags = orders.Count;

        // calculate the x coordinate of where the bag should go
        float x = transform.position.x - width + (bagWidth * 0.5f + bagSpacing) + ((bagWidth + (2 * bagSpacing)) * numBags);
        // set the position of the bag
        Vector3 v = new Vector3(x, transform.position.y + (transform.localScale.y * 0.5f) + bagWidth, transform.position.z);
        GameObject g = Instantiate(bagPre, v, Quaternion.identity);
        g.GetComponent<Bag>().order = o;

        // create the UI popover or the bag
        GameObject ui = Instantiate(bagUI, GameObject.Find("Canvas").transform);
        // position it above the gameobject
        ui.transform.position =  Camera.main.WorldToScreenPoint(v);

        // set the text of the popover
        Text t = ui.transform.GetChild(0).GetComponent<Text>();
        t.color = Color.red;
        foreach (Item i in o.items)
        {
            t.text += i.ToString()[0] + "\n";
        }

        // set properties on the object
        o.bag = g;
        o.bagUI = ui;
    }

    void UpdateBags(int index)
    {
        // slide each bag down that needs to be moved
        for(int i = index + 1; i < orders.Count; i++)
        {
            orders[i].bag.transform.position -= new Vector3(bagSpacing * 2 + bagWidth, 0, 0);
            orders[i].bagUI.transform.position = Camera.main.WorldToScreenPoint(orders[i].bag.transform.position);
        }
    }

    public void AddItemToBag(Item item, Order o)
    {
        Debug.Log("Adding " + item.ToString() + " to bag");
        o.completedItems.Add(item);
        Text t = o.bagUI.transform.GetChild(0).GetComponent<Text>();
        t.text = "";
        foreach (Item i in o.items)
        {
            t.text += (o.completedItems.Contains(i) ? "<color=#00ff00ff>" : "<color=#ff0000ff>") + i.ToString()[0] + "</color>\n";
        }

        if(o.completedItems.Count == o.items.Count)
        {
            // bag completed
            toRemove.Add(o);
        }
    }
}
