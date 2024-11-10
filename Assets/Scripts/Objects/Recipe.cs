using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public Guid id;
    public string recipeName;
    public List<Ingredient> ingredients;
    public int reward;
    public float time; // how much time recipe should take, in seconds

    public Recipe(string name, List<Ingredient> ingr, int rewa)
    {
        id = Guid.NewGuid();
        recipeName = name;
        ingredients = ingr;
        reward = rewa;
        time = 60.0f; // setting time as fixed for all orders for now
    }

    // Constructor
    public Recipe()
    {
        id = Guid.NewGuid();
        recipeName = "";
        ingredients = new List<Ingredient>();
        reward = 0;
        time = 0.0f;
    }

    public override string ToString()
    {
        return "Recipe: " + recipeName + " " + reward;
    }
}