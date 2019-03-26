using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(Input.GetKeyDown(KeyCode.B))
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
        }
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
}
