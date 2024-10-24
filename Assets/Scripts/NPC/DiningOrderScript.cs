using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiningOrderScript : MonoBehaviour
{
    public GameObject holdingObject;
    public GameObject cookedFood;

    // define order here
    private Recipe order;

    void Awake()
    {
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
            Debug.Log("Set order: " + recipe.recipeName);
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
        return order.ingredients.Any(ingr => ingr.type.Equals(ingredientProps.ingredientType));
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
