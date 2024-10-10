using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orders : MonoBehaviour
// one recipe contains a list of ingredients
// and a reward
{
    public static List<Recipe> orders; // this list will keep track of the current orders
    public static List<Recipe> recipes; // this list is the available recipes in the game

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

        public override string ToString()
        {
            return "Recipe: " + recipeName + " " + reward;
        }
    }


    // Creating instances of the Recipe class
    public Recipe tomatoRecipe = new Recipe("tomato soup", new List<Ingredient> { new Tomato() }, 100);
    public Recipe cornRecipe = new Recipe("corn cheese", new List<Ingredient> { new Corn() }, 200);
    public Recipe lettuceRecipe = new Recipe("salad", new List<Ingredient> { new Lettuce() }, 300);

    void Start()
    {
        // instantiation
        recipes = new List<Recipe>{
            tomatoRecipe, cornRecipe, lettuceRecipe
        };
        orders = new List<Recipe>();
    }

    public static void PlaceOrder(Recipe recipe)
    {
        orders.Add(recipe);
        PrintOrders();
    }

    static void PrintOrders()
    {
        int i = 0;
        while (i < orders.Count)
        {
            Debug.Log(orders[i]);
            i++;
        }
    }
}