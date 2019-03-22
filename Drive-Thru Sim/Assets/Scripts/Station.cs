using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(BoxCollider2D))]
public class Station : MonoBehaviour
{
    protected float prepTime; // How long the station takes to prepare the food
    protected float overflowTime; // How long the station can be left after the item completes before it gets ruined
    protected bool inUse; // If this station is currently being used

    public GameObject item; // The prefab of the item that will be prepared at this station

    protected BoxCollider2D activator; // Collider around the station for triggering

    // Start is called before the first frame update
    void Start()
    {
        activator = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void TriggerStation()
    {

    }

    protected void HireWorker()
    {

    }
}
