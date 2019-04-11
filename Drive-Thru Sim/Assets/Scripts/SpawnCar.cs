using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    private bool CarInWay = false;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CarInWay);
    }
    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            if(!CarInWay)
            {
                Instantiate(car);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CarInWay = true;
    }
    private void OnTriggerExit(Collider other)
    {
        CarInWay = false;
    }
}
