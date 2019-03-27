using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(BoxCollider))]
public class Station : MonoBehaviour
{
    protected float prepTime; // How long the station takes to prepare the food
    protected float overflowTime; // How long the station can be left after the item completes before it gets ruined
    protected float timer;
    protected bool inUse; // If this station is currently being used

    public GameObject item; // The prefab of the item that will be prepared at this station

    protected BoxCollider activator; // Collider around the station for triggering

    // Start is called before the first frame update
    void Start()
    {
        activator = gameObject.GetComponent<BoxCollider>();
        inUse = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(inUse)
        {
            timer += Time.deltaTime;
        }
    }

    protected void TriggerStation()
    {
        inUse = true;
        timer = 0.0f;

        GameObject itemInit = Instantiate(item, gameObject.transform);
        itemInit.transform.position = new Vector3(0, gameObject.transform.localScale.y/2, 0);

        Debug.Log("Starting station");
    }

    protected void PickUp(Transform player)
    {
        inUse = false;

        gameObject.transform.GetChild(0).SetParent(player);
        

        Debug.Log("Picking up from station");
    }

    protected void HireWorker()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Space) && inUse == false)
        {
            TriggerStation();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && timer > prepTime)
        {
            if(other.gameObject.transform.childCount == 0)
            {
                PickUp(other.transform);
            }
        }
    }
}
