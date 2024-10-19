using System;
using System.Collections;
using System.Collections.Generic;
using CulinaryChaos.Objects;
using UnityEngine;

public class CookingComponent : MonoBehaviour
{

    public GameObject cookingFood;
    public GameObject pot;

    void Awake()
    {
    }

    internal bool CanAcceptIngredient(GameObject pickUpObject)
    {
        var ingredientComponent = GetIngredientProperties(pickUpObject);
        if (ingredientComponent == null) return false;

        var ingredientType = ingredientComponent.ingredientType;

        return ingredientType == IngredientEnum.Tomato ||
                ingredientType == IngredientEnum.Pumpkin ||
                ingredientType == IngredientEnum.Mushroom;

    }

    internal void CookIngredient(GameObject pickUpObject)
    {
        var ingredientComponent = GetIngredientProperties(pickUpObject);
        if (ingredientComponent == null) return;

        Debug.Log("Cooking " + ingredientComponent.ingredientType);

        var cookingMaterial = ingredientComponent.cookingMaterial;
        cookingFood.GetComponent<Renderer>().material = cookingMaterial;

        cookingFood.SetActive(true);
        pot.SetActive(true);

    }

    private IngredientProps GetIngredientProperties(GameObject pickUpObject)
    {
        return pickUpObject.GetComponent<IngredientProps>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
