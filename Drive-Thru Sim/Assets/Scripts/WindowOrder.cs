using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOrder : MonoBehaviour
{
    private OrderManager om;
    private Color matOriginal;      // original color of this station
    private Material mat;           // material of this station

    // Start is called before the first frame update
    void Start()
    {
        om = GameObject.Find("Bagging").GetComponent<OrderManager>();
        matOriginal = gameObject.GetComponent<Renderer>().material.color;
        mat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        mat.color = matOriginal + new Color(0.3f,0.3f,0.3f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            om.OrderWindowNewOrder();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        mat.color = matOriginal;
    }
}
