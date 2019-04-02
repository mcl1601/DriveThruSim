using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(BoxCollider))]
public class Station : MonoBehaviour
{
    public float prepTime; // How long the station takes to prepare the food
    public float overflowTime; // How long the station can be left after the item completes before it gets ruined
    protected float timer;

    public Image timerVis; // UI component to represent the timer on prepping the food

    protected bool inUse; // If this station is currently being used

    public GameObject item; // The prefab of the item that will be prepared at this station

    protected BoxCollider activator; // Collider around the station for triggering

    private Color matOriginal;   // original color of this station
    private Material mat;           // material of this station

    // Start is called before the first frame update
    void Start()
    {
        activator = gameObject.GetComponent<BoxCollider>();
        inUse = false;
        timer = 0.0f;
        matOriginal = gameObject.GetComponent<Renderer>().material.color;
        mat = gameObject.GetComponent<Renderer>().material;

        // position it above the gameobject
        timerVis.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(inUse)
        {
            timer += Time.deltaTime;
            timerVis.fillAmount = (timer / prepTime);
        }
    }

    protected void TriggerStation()
    {
        inUse = true;
        timer = 0.0f;

        GameObject itemInit = Instantiate(item, new Vector3(0, gameObject.transform.localScale.y / 2, 0), Quaternion.identity);
        itemInit.transform.parent = gameObject.transform;
        itemInit.transform.localPosition = new Vector3(0f, itemInit.transform.position.y, 0f);

        Debug.Log("Starting station");
    }

    protected void PickUp(Transform player)
    {
        inUse = false;

        gameObject.transform.GetChild(0).transform.position = player.transform.GetChild(1).GetChild(0).transform.position;

        gameObject.transform.GetChild(0).SetParent(player);

        timerVis.fillAmount = 0;

        Debug.Log("Picking up from station");
    }

    protected void HireWorker()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        mat.color = matOriginal + new Color(0.1f, 0.1f, 0.1f);
        if(Input.GetKeyDown(KeyCode.E) && inUse == false)
        {
            TriggerStation();
        }
        else if(Input.GetKeyDown(KeyCode.E) && timer > prepTime)
        {
            //if(other.gameObject.transform.childCount == 0)
            //{
                PickUp(other.transform);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        mat.color = matOriginal;
    }
}
