using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OrderUI : MonoBehaviour
{
    public Guid id;
    public TMP_Text tableNumber;
    public TMP_Text orderName;
    public TMP_Text ingredients;
    public TMP_Text reward;

    void Start()
    {
        tableNumber = GetComponent<TextMeshProUGUI>();
        orderName = GetComponent<TextMeshProUGUI>();
        ingredients = GetComponent<TextMeshProUGUI>();
        reward = GetComponent<TextMeshProUGUI>();
    }

    public GameObject CreateOrderPanel(Recipe recipe, int tableNum)
    {
        this.id = recipe.id;

        orderName.text = recipe.recipeName;

        string ingredientsStr = "";
        foreach (Ingredient ing in recipe.ingredients)
        {
            ingredientsStr += ing;
        }
        ingredients.text = ingredientsStr;

        reward.text = recipe.reward.ToString();

        tableNumber.text = $"Table {tableNum}";

        return this.gameObject;
    }

}
