using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;

    private Rigidbody rb;

    private Quaternion startRotation;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpVec = Vector3.zero;
        if (Input.GetKey("w"))
        {
            tmpVec += Vector3.forward;
            //rb.MovePosition(gameObject.transform.position + (Vector3.forward * Time.deltaTime * speed));
        }
        if (Input.GetKey("s"))
        {
            //rb.MovePosition(gameObject.transform.position + (-Vector3.forward * Time.deltaTime * speed));
            tmpVec += -Vector3.forward;
        }
        if (Input.GetKey("a"))
        {
            //rb.MovePosition(gameObject.transform.position + (-Vector3.right* Time.deltaTime * speed));
            tmpVec += -Vector3.right;
        }
        if (Input.GetKey("d"))
        {
            //rb.MovePosition(gameObject.transform.position + (Vector3.right * Time.deltaTime * speed));
            tmpVec += Vector3.right;
        }


        rb.MovePosition(gameObject.transform.position + (tmpVec.normalized * Time.deltaTime * speed));

        gameObject.transform.rotation = startRotation;
    }
}
