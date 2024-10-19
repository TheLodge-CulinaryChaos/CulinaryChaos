using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour
// one order contains a list of ingredients
// and a reward
{
    public class Recipe
    {
        public string recipeName;
        public List<Ingredient> ingredients;
        public int reward;

        public Recipe(string name, List<Ingredient> ingr, int rewa)
        {
            recipeName = name;
            ingredients = ingr;
            reward = rewa;
        }

        // Constructor
        public Recipe()
        {
            recipeName = "";
            ingredients = new List<Ingredient>();
            reward = 0;
        }
    }


    // Creating instances of the Recipe class
    public Recipe tomatoRecipe = new Recipe($"Tomato Soup", new List<Ingredient> { new Tomato() }, 100);
    public Recipe cornRecipe = new Recipe("Pumpkin Soup", new List<Ingredient> { new Pumpkin() }, 200);
    public Recipe lettuceRecipe = new Recipe("Mushroom Soup", new List<Ingredient> { new Mushroom() }, 300);


    void Start()
    {
    }
}