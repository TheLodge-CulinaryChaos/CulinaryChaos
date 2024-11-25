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
    private Order order;

    private string orderStatus = "";
    private float orderStatusClearTime = 3f; // Time in seconds to clear order status

    private Coroutine orderStatusCoroutine;

    void Awake()
    {
        coins = FindObjectOfType<Coins>();
        holdingObject.SetActive(false);
        cookedFood.SetActive(false);
        // remove material from cooked food
        cookedFood.GetComponent<Renderer>().material = null;
    }

    internal void SetOrder(Order order)
    {
        if (order == null)
        {
            return;
        }
        if (this.order == null)
        {
            // Debug.Log("Set order: " + recipe.recipeName);
            this.order = order;
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
        Recipe recipe = order.recipe;
        if (recipe.ingredients.Any(ingr => ingr.type.Equals(ingredientProps.ingredientType)))
        {
            coins.addMoney(recipe.reward);
            return true;
        }
        else
        {
            return false;
        }
        ;
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

    internal void setOrderStatus(string status)
    {
        orderStatus = status;
        if (orderStatusCoroutine != null)
        {
            StopCoroutine(orderStatusCoroutine);
        }
        orderStatusCoroutine = StartCoroutine(ClearOrderStatusAfterDelay());
    }

    private IEnumerator ClearOrderStatusAfterDelay()
    {
        yield return new WaitForSeconds(orderStatusClearTime);
        clearOrderStatus();
    }

    internal void clearOrderStatus()
    {
        orderStatus = "";
        if (orderStatusCoroutine != null)
        {
            StopCoroutine(orderStatusCoroutine);
        }
    }

    internal string getOrderStatus()
    {
        return orderStatus;
    }
}
