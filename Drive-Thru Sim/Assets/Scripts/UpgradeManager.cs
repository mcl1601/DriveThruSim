using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private bool isNight; // Is it the night phase where player can upgrade

    public bool IsNight
    {
        get { return isNight; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
