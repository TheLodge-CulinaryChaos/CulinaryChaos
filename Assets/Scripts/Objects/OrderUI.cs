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

    void Start()
    {
        tableNumber = GetComponent<TextMeshProUGUI>();
        orderName = GetComponent<TextMeshProUGUI>();
        reward = GetComponent<TextMeshProUGUI>();
    }

    public GameObject CreateOrderPanel(Recipe recipe, int tableNum)
    {
        this.id = recipe.id;

        orderName.text = recipe.recipeName;
        reward.text = recipe.reward.ToString();
        tableNumber.text = $"Table {tableNum}";

        setIngredientImage(recipe);

        return this.gameObject;
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
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/mushroom");
                    break;
                case (IngredientEnum.Tomato):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/tomato");
                    break;
                case (IngredientEnum.Pumpkin):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/pumpkin");
                    break;
                case (IngredientEnum.GreenPepper):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/pepper");
                    break;
                case (IngredientEnum.Potato):
                    ingredientImage.sprite = Resources.Load<Sprite>("Images/potato");
                    break;
            }
        }
    }

}
