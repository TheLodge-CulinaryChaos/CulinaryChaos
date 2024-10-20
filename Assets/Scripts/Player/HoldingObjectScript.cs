using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjectScript : MonoBehaviour
{

    public GameObject holdingObject;
    public GameObject cookedObject;

    internal void GrabABowl()
    {
        holdingObject.SetActive(true);
        cookedObject.SetActive(false);
    }

    // Start is called before the first frame update

    void Awake() {
        holdingObject.SetActive(false);
        cookedObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
