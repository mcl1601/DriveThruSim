using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if( other.gameObject.transform.childCount > 2)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<Renderer>().material.color = Color.gray;
                Destroy(other.gameObject.transform.GetChild(2).gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }
}
