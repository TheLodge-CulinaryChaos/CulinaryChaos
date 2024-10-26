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

    public Recipe(string name, List<Ingredient> ingr, int rewa)
    {
        id = Guid.NewGuid();
        recipeName = name;
        ingredients = ingr;
        reward = rewa;
    }

    // Constructor
    public Recipe()
    {
        id = Guid.NewGuid();
        recipeName = "";
        ingredients = new List<Ingredient>();
        reward = 0;
    }

    public override string ToString()
    {
        return "Recipe: " + recipeName + " " + reward;
    }
}