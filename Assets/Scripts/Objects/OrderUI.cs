using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
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

    public void UpdateOrderUI(Recipe recipe, int tableNum)
    {
        orderName.text = recipe.recipeName;

        string ingredientsStr = "";
        foreach (Ingredient ing in recipe.ingredients)
        {
            ingredientsStr += ing;
        }
        ingredients.text = ingredientsStr;

        reward.text = recipe.reward.ToString();

        tableNumber.text = $"Table {tableNum}";
    }

}
