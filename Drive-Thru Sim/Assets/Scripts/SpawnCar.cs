using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(15);
            Instantiate(car);
        }
    }
}
