using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    private float slideSpeed = 500f;

    private Rigidbody rb;

    private Quaternion startRotation;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody>();

        //Save start rotation to prevent rotating when running into object corners
        startRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpVec = Vector3.zero;
        if (Input.GetKey("w"))
        {
            tmpVec += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            tmpVec += -Vector3.forward;
        }
        if (Input.GetKey("a"))
        {
            tmpVec += -Vector3.right;
        }
        if (Input.GetKey("d"))
        {
            tmpVec += Vector3.right;
        }


        rb.MovePosition(gameObject.transform.position + (tmpVec.normalized * Time.deltaTime * speed));
        //
        //Ice-like movement if we want it
        //rb.AddForce((tmpVec.normalized * Time.deltaTime * slideSpeed));
        gameObject.transform.rotation = startRotation;
    }


    //Speed change and reset for slow zones if we want to use them
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Slow")
        {
            speed = 2.0f;
            slideSpeed = 300f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slow")
        {
            speed = 5.0f;
            slideSpeed = 500f;
        }
    }
}
