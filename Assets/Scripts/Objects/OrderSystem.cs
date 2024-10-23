using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderSystem : MonoBehaviour
// one recipe contains a list of ingredients
// and a reward
{
    public List<Recipe> orders; // this list will keep track of the current orders
    public List<Recipe> recipes; // this list is the available recipes in the game

    public GameObject orderPanelPrefab;
    public Transform ordersParent;

    // Creating instances of the Recipe class
    public Recipe tomatoRecipe = new Recipe($"Tomato Soup", new List<Ingredient> { new Tomato() }, 100);
    public Recipe cornRecipe = new Recipe("Pumpkin Soup", new List<Ingredient> { new Pumpkin() }, 200);
    public Recipe lettuceRecipe = new Recipe("Mushroom Soup", new List<Ingredient> { new Mushroom() }, 300);

    void Awake()
    {
        // instantiation
        recipes = new List<Recipe>{
            tomatoRecipe, cornRecipe, lettuceRecipe
        };
        orders = new List<Recipe>();
    }

    public void PlaceOrder(Recipe recipe)
    {
        orders.Add(recipe);
        PrintOrders();
    }

    public void CreateOrderUI(Recipe recipe, int tableNumber)
    {

        GameObject orderPanel = Instantiate(orderPanelPrefab, ordersParent);
        OrderUI orderUI = orderPanel.GetComponent<OrderUI>();
        orderUI.UpdateOrderUI(recipe, tableNumber);
    }

    void PrintOrders()
    {
        int i = 0;
        while (i < this.orders.Count)
        {
            Debug.Log(this.orders[i]);
            i++;
        }
    }
}