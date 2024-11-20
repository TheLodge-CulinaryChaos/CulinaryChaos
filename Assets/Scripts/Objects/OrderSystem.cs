using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderSystem : MonoBehaviour
// one recipe contains a list of ingredients
// and a reward
{
    public List<Recipe> orders; // this list will keep track of the current orders
    public List<Recipe> recipes; // this list is the available recipes in the game
    public List<GameObject> orderPanels; // list of each order panel (UI) in the game

    public GameObject orderPanelPrefab;
    public Transform ordersParent;

    public int levelNumber = 1;

    // Creating instances of the Recipe class
    public Recipe tomatoRecipe = new Recipe($"Tomato Soup", new List<Ingredient> { new Tomato() }, 150);
    public Recipe cornRecipe = new Recipe("Pumpkin Soup", new List<Ingredient> { new Pumpkin() }, 200);
    public Recipe lettuceRecipe = new Recipe("Mushroom Soup", new List<Ingredient> { new Mushroom() }, 250);

    public Recipe greenPepperRecipe = new Recipe("Green Pepper Soup", new List<Ingredient> { new GreenPepper() }, 275);
    public Recipe potatoRecipe = new Recipe("Potato Soup", new List<Ingredient> { new Potato() }, 225);

    void Awake()
    {
        // instantiation
        recipes = new List<Recipe>{
            tomatoRecipe, cornRecipe, lettuceRecipe
        };

        if (levelNumber >= 2)
        {
            recipes.Add(greenPepperRecipe);
        }

        if (levelNumber >= 3)
        {
            recipes.Add(potatoRecipe);
        }

        orders = new List<Recipe>();
    }

    public void PlaceOrder(Recipe recipe, int tableNumber)
    {
        orders.Add(recipe);
        // PrintOrders();

        // Create a new order panel OBJECT
        GameObject orderPanel = Instantiate(orderPanelPrefab, ordersParent);
        OrderUI orderUI = orderPanel.GetComponent<OrderUI>();
        GameObject orderPanelObj = orderUI.CreateOrderPanel(recipe, tableNumber);

        orderPanels.Add(orderPanelObj);
    }

    public void RemoveOrder(Recipe recipe)
    {
        if (recipe == null)
        {
            // Debug.Log("Recipe is null");
            return;
        }
        
        var panel = orderPanels.Find(p => p.GetComponent<OrderUI>().id == recipe.id);

        orders.Remove(recipe);
        orderPanels.Remove(panel);
        Destroy(panel);
    }

    void PrintOrders()
    {
        int i = 0;
        while (i < this.orders.Count)
        {
            // Debug.Log(this.orders[i]);
            i++;
        }
    }
}