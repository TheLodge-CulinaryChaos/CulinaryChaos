using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CulinaryChaos.Objects;
using TMPro;
using System;

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
    private Recipe order;

    void Start()
    {
    }
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

    public GameObject CreateOrderPanel(Recipe recipe, int tableNum)
    {
        this.id = recipe.id;

        orderName.text = recipe.recipeName;
        reward.text = recipe.reward.ToString();
        tableNumber.text = $"Table {tableNum}";

        currentTime = recipe.time;
        timer.text = timerToText(currentTime); // fixed timer, 60s
        started = true;
        order = recipe;

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
            switch (ing.type)
            {
                case (IngredientEnum.Mushroom):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/ingredients/mushroom");
                    break;
                case (IngredientEnum.Tomato):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/ingredients/tomato");
                    break;
                case (IngredientEnum.Pumpkin):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/ingredients/pumpkin");
                    break;
                case (IngredientEnum.GreenPepper):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/ingredients/pepper");
                    break;
                case (IngredientEnum.Potato):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/ingredients/potato");
                    break;
            }
        }
    }

}
