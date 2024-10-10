using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TMP_Text orderName;
    public TMP_Text ingredients;
    public TMP_Text reward;

    void Start()
    {
        orderName = GetComponent<TextMeshProUGUI>();
        ingredients = GetComponent<TextMeshProUGUI>();
        reward = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateOrderUI(Recipe recipe)
    {
        orderName.text = recipe.recipeName;

        string ingredientsStr = "";
        foreach (Ingredient ing in recipe.ingredients)
        {
            ingredientsStr += ing;
        }
        ingredients.text = ingredientsStr;

        reward.text = recipe.reward.ToString();
    }

}
