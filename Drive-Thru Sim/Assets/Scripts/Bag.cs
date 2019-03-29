using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bag : MonoBehaviour
{
    public Order order;

    private OrderManager om;

    private void Start()
    {
        om = GameObject.Find("Bagging").GetComponent<OrderManager>();
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.B))
        {
            AddItemToBag(Item.Burger);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddItemToBag(Item.Fry);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddItemToBag(Item.Drink);
        }*/
    }

    // probably needs an item parameter
    private void AddItemToBag(Item item)
    {
        // is this item valid?
        if(order.items.Contains(item) && !order.completedItems.Contains(item))
        {
            // pass the item to the manager
            om.AddItemToBag(item, order);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.childCount != 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            if(Input.GetKeyDown(KeyCode.E))
            {
                // check if the object has a tag
                string tag = other.gameObject.transform.GetChild(0).tag;
                if (tag == null) return;
                Item item = Item.Burger;
                switch (tag)
                {
                    case "Burgers":
                        item = Item.Burger;
                        break;
                    case "Fries":
                        item = Item.Fry;
                        break;
                    case "Drinks":
                        item = Item.Drink;
                        break;
                }
                // is this item valid?
                if (order.items.Contains(item) && !order.completedItems.Contains(item))
                {
                    // pass the item to the manager
                    om.AddItemToBag(item, order);
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
