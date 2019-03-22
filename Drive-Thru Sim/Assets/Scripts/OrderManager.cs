using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Burger, Fry, Drink
}

public class OrderManager : MonoBehaviour
{
    public GameObject orderTicketUI;
    private List<Order> orders;
    private Transform ticketParent;

    // Start is called before the first frame update
    void Start()
    {
        orders = new List<Order>();
        ticketParent = GameObject.Find("TicketParent").transform;

        // For testing, create an inital order
        List<Item> items = new List<Item>
        {
            Item.Burger,
            Item.Fry,
            Item.Drink
        };
        Order o1 = new Order(items, 3f);
        CreateOrderTicket(o1);
        Order o2 = new Order(new List<Item> { Item.Fry }, 5f);
        CreateOrderTicket(o2);
    }

    void CreateOrderTicket(Order o)
    {
        GameObject g = Instantiate(orderTicketUI, ticketParent);
        Text t = g.transform.GetChild(0).GetComponent<Text>();
        foreach(Item i in o.items)
        {
            t.text += i.ToString() + "\n";
        }
        o.ticketUI = g;
        orders.Add(o);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOrders();
    }

    void UpdateOrders()
    {
        List<Order> toRemove = new List<Order>();
        foreach(Order o in orders)
        {
            if(o.UpdateTimer())
            {
                Destroy(o.ticketUI);
                toRemove.Add(o);
            }
        }
        foreach(Order o in toRemove)
        {
            orders.Remove(o);
        }
    }
}
