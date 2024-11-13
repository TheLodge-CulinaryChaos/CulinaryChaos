using System;
using System.Collections.Generic;

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
        time = GenerateOrderTime();
    }

    private float GenerateOrderTime()
    {
        System.Random random = new System.Random();
        int minTime = 60;
        int maxTime = 90;

        float randomFloat = (float)(random.NextDouble() * (maxTime - minTime) + minTime);

        return (float)randomFloat;
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
