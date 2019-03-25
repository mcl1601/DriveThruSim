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
    public GameObject orderTicketUI, bagPre, bagUI;
    public float bagSpacing = 0.125f;
    public float bagWidth = 0.5f;

    private List<Order> orders;     // collection of Order Objects
    private Transform ticketParent; // where to instantiate the order tickets

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
        // create two orders
        Order o1 = new Order(items, 3f);
        CreateOrderTicket(o1);
        Order o2 = new Order(new List<Item> { Item.Fry }, 5f);
        CreateOrderTicket(o2);
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
        List<Order> toRemove = new List<Order>();
        // update the timer on each bag, remove if necessary
        foreach(Order o in orders)
        {
            if(o.UpdateTimer())
            {
                // save this order to be deleted later
                Destroy(o.ticketUI);
                toRemove.Add(o);
            }
        }
        // delete order objects and their gameobjects
        foreach(Order o in toRemove)
        {
            Destroy(o.bag);
            Destroy(o.bagUI);
            UpdateBags(orders.IndexOf(o));
            orders.Remove(o);
        }
    }

    void AddBagToStation(Order o)
    {
        // check if there is room for a bag
        //if (CheckSpaceForBag()) return;

        float width = transform.localScale.x * 0.5f;
        float numBags = orders.Count;

        // calculate the x coordinate of where the bag should go
        float x = transform.position.x - width + (bagWidth * 0.5f + bagSpacing) + ((bagWidth + (2 * bagSpacing)) * numBags);
        // set the position of the bag
        Vector3 v = new Vector3(x, transform.position.y + (transform.localScale.y * 0.5f) + bagWidth, transform.position.z);
        GameObject g = Instantiate(bagPre, v, Quaternion.identity);

        // create the UI popover or the bag
        GameObject ui = Instantiate(bagUI, GameObject.Find("Canvas").transform);
        // position it above the gameobject
        ui.transform.position =  Camera.main.WorldToScreenPoint(v);

        // set the text of the popover
        Text t = ui.transform.GetChild(0).GetComponent<Text>();
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

    bool CheckSpaceForBag()
    {
        float width = transform.localScale.x * 2f;
        float numBags = orders.Count;
        return (bagWidth + 2 * bagSpacing) * (numBags + 1) > width; 
    }
}
