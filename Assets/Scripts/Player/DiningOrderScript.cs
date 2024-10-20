using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiningOrderScript : MonoBehaviour
{
    public GameObject holdingObject;
    public GameObject cookedFood;

    // define order here
    public Recipe tomatoRecipe = new Recipe($"Tomato Soup", new List<Ingredient> { new Tomato() }, 100);

    void Awake()
    {
        holdingObject.SetActive(false);
        cookedFood.SetActive(false);
        // remove material from cooked food
        cookedFood.GetComponent<Renderer>().material = null;
    }

    internal bool IsOrderAccepted(IngredientProps ingredientProps)
    {
        return tomatoRecipe.ingredients.Any(ingr => ingr.type.Equals(ingredientProps.ingredientType));
    }

    internal void CompleteOrder(IngredientProps ingredientProps) {
        if (IsOrderAccepted(ingredientProps)) {
            holdingObject.SetActive(true);
            cookedFood.SetActive(true);
            cookedFood.GetComponent<Renderer>().material = ingredientProps.cookingMaterial;
        }
    }
}
