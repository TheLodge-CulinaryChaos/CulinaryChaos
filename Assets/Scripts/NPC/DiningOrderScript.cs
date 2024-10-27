using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiningOrderScript : MonoBehaviour
{
    public GameObject holdingObject;
    public GameObject cookedFood;
    public Coins coins;

    // define order here
    private Recipe order;

    void Awake()
    {
        coins = FindObjectOfType<Coins>();
        holdingObject.SetActive(false);
        cookedFood.SetActive(false);
        // remove material from cooked food
        cookedFood.GetComponent<Renderer>().material = null;
    }

    internal void SetOrder(Recipe recipe)
    {
        if (recipe == null)
        {
            return;
        }
        if (order == null)
        {
            // Debug.Log("Set order: " + recipe.recipeName);
            order = recipe;
        }
    }

    internal void ClearOrder()
    {
        order = null;
        holdingObject.SetActive(false);
        cookedFood.SetActive(false);
    }

    internal bool IsOrderAccepted(IngredientProps ingredientProps)
    {
        if (order.ingredients.Any(ingr => ingr.type.Equals(ingredientProps.ingredientType)))
        {

            coins.addMoney(order.reward);
            return true;
        }
        else
        {
            return false;
        };
    }

    internal void CompleteOrder(IngredientProps ingredientProps)
    {
        holdingObject.SetActive(true);
        cookedFood.SetActive(true);
        cookedFood.GetComponent<Renderer>().material = ingredientProps.cookingMaterial;
    }

    internal bool IsOrderComplete()
    {
        return holdingObject.activeSelf;
    }
}
