using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Material transparentMaterial;
    private Material originalMaterial;
    private Renderer theWall;

    void Start()
    {
        theWall = GetComponent<Renderer>();
        originalMaterial = theWall.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            theWall.material = transparentMaterial;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            theWall.material = originalMaterial;
        }
    }
}
