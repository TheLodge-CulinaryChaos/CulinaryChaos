using System;
using System.Collections;
using System.Collections.Generic;
using CulinaryChaos.Objects;
using TMPro;
using UnityEngine;

public class CookingComponent : MonoBehaviour
{

    public GameObject cookingFood;
    public GameObject pot;
    
    public IngredientProps ingredientProps;
    

    // textmeshpro timer
    public TMP_Text timer;


    private float countdownTime = 5f;
    private bool isCooking = false;
    private bool isFoodReady = false;    

    void Awake()
    {
        cookingFood.SetActive(false);
        timer.SetText("");
    }

    internal bool CanAcceptIngredient(GameObject pickUpObject)
    {
        ingredientProps = GetIngredientProperties(pickUpObject);
        if (ingredientProps == null) return false;

        if (cookingFood.activeSelf)
        {
            return false;
        }

        var ingredientType = ingredientProps.ingredientType;

        return ingredientType == IngredientEnum.Tomato ||
                ingredientType == IngredientEnum.Pumpkin ||
                ingredientType == IngredientEnum.Mushroom ||
                ingredientType == IngredientEnum.GreenPepper;

    }

    internal void CookIngredient(GameObject pickUpObject)
    {
        ingredientProps = GetIngredientProperties(pickUpObject);
        if (ingredientProps == null) return;

        var cookingMaterial = ingredientProps.cookingMaterial;
        cookingFood.GetComponent<Renderer>().material = cookingMaterial;

        cookingFood.SetActive(true);
        pot.SetActive(true);

    }

    internal void RemoveFoodFromPot() {
        cookingFood.SetActive(false);
        isCooking = false;
        isFoodReady = false;
        countdownTime = 5f;
    }

    internal void ResetTimer() {
        timer.SetText("");
        countdownTime = 5f;
    }

    private IngredientProps GetIngredientProperties(GameObject pickUpObject)
    {
        return pickUpObject.GetComponent<IngredientProps>();
    }

    IEnumerator StartCookingTimer()
    {
        if (cookingFood.activeSelf && countdownTime > 0)
        {
            timer.SetText(countdownTime.ToString());

            countdownTime -= Time.deltaTime;
            int displayTime = Mathf.CeilToInt(countdownTime);

            if (displayTime < 1)
            {
                timer.SetText("Ready");
                isCooking = false;
                isFoodReady = true;
            } else {
                timer.SetText(displayTime.ToString());
                isCooking = true;
                isFoodReady = false;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public Boolean isFoodReadyToServe()
    {
        return isFoodReady;
    }

    void Update()
    {
        StartCoroutine(StartCookingTimer());
    }
}
