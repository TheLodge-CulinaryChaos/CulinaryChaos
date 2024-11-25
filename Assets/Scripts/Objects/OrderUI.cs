using System;
using System.Collections;
using System.Collections.Generic;
using CulinaryChaos.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    public Guid id;
    public TMP_Text tableNumber;
    public TMP_Text orderName;
    public TMP_Text reward;
    public GameObject ingredientImagePrefab;
    public Transform ingredientsParent;
    public TMP_Text timer;

    private float currentTime; // timer tracking
    private bool started;
    private Order order;

    void Start() { }

    void Update()
    {
        // update the timer
        if (started)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                started = false;
                OrderSystem orderSystem = FindObjectOfType<OrderSystem>();
                orderSystem.RemoveOrder(order);
            }
            timer.text = timerToText(currentTime);
        }
    }

    public GameObject CreateOrderPanel(Order order)
    {
        Recipe recipe = order.recipe;
        this.id = recipe.id;

        orderName.text = recipe.recipeName;
        reward.text = recipe.reward.ToString();
        tableNumber.text = $"Table {order.tableNumber}";

        currentTime = recipe.time;
        timer.text = timerToText(currentTime); // fixed timer, 60s
        started = true;
        this.order = order;

        setIngredientImage(recipe);

        return this.gameObject;
    }

    string timerToText(float initialSeconds)
    {
        int minutes = Mathf.FloorToInt(initialSeconds / 60);
        int seconds = Mathf.FloorToInt(initialSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void setIngredientImage(Recipe recipe)
    {
        foreach (Ingredient ing in recipe.ingredients)
        {
            GameObject ingredientImageObj = Instantiate(ingredientImagePrefab, ingredientsParent);
            Image ingredientImage = ingredientImageObj.GetComponent<Image>();
            ingredientImage.sprite = GetIngredientSprite(ing.type);
        }
    }

    public Order getOrder()
    {
        return order;
    }

    public static Sprite GetIngredientSprite(IngredientEnum ingredientEnum)
    {
        switch (ingredientEnum)
        {
            case IngredientEnum.Mushroom:
                return Resources.Load<Sprite>("Images/ingredients/mushroom");

            case IngredientEnum.Tomato:
                return Resources.Load<Sprite>("Images/ingredients/tomato");

            case IngredientEnum.Pumpkin:
                return Resources.Load<Sprite>("Images/ingredients/pumpkin");

            case IngredientEnum.GreenPepper:
                return Resources.Load<Sprite>("Images/ingredients/pepper");

            case IngredientEnum.Potato:
                return Resources.Load<Sprite>("Images/ingredients/potato");

            default:
                return null;
        }
    }
}
