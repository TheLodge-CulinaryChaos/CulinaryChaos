using UnityEngine;
using System.Collections;

public class Ingredient
{
    public string name;

    public void Chop()
    {
        Debug.Log("The " + name + " ingredient has been chopped.");
    }
}