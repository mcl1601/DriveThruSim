using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 1.0f;
    private Vector3 position;
    private bool rotating = false;
    void Start()
    {
        //Set the position
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Keep moving it down the driveway
        position += (transform.up * Time.deltaTime * speed);
        transform.position = position;
        if(rotating)
            transform.Rotate(new Vector3(0, -10f, 0) * (2.5f * Time.deltaTime), Space.World);
        if (transform.rotation.eulerAngles.y <= 270f && transform.rotation.eulerAngles.y > 0)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            rotating = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RotationTrigger") rotating = true;
    }
}
