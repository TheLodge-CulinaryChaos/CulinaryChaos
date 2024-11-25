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
    public GameObject timerCube;

    private Dictionary<IngredientEnum, float> ingredientCookingTimes = new Dictionary<IngredientEnum, float>
    {
        { IngredientEnum.Tomato, 5f },
        { IngredientEnum.Pumpkin, 8f },
        { IngredientEnum.Mushroom, 10f },
        { IngredientEnum.GreenPepper, 13f },
        { IngredientEnum.Potato, 10f }
    };


    private float countdownTime;
    private bool isCooking = false;
    private bool isFoodReady = false;    

    void Awake()
    {
        cookingFood.SetActive(false);
        timer.SetText("");
        timerCube.SetActive(false);
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

        return ingredientCookingTimes.ContainsKey(ingredientType);

    }

    internal void CookIngredient(GameObject pickUpObject)
    {
        ingredientProps = GetIngredientProperties(pickUpObject);
        if (ingredientProps == null) return;

        var cookingMaterial = ingredientProps.cookingMaterial;
        cookingFood.GetComponent<Renderer>().material = cookingMaterial;

        cookingFood.SetActive(true);
        pot.SetActive(true);
        timerCube.SetActive(true);

        countdownTime = ingredientCookingTimes[ingredientProps.ingredientType];
        StartCoroutine(StartCookingTimer());

    }

    internal void RemoveFoodFromPot() {
        cookingFood.SetActive(false);
        timerCube.SetActive(false);
        
        isCooking = false;
        isFoodReady = false;
        countdownTime = ingredientCookingTimes[ingredientProps.ingredientType];;
    }

    internal void ResetTimer() {
        timer.SetText("");
        countdownTime = ingredientCookingTimes[ingredientProps.ingredientType];;
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
                timerCube.SetActive(false);
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
        if (timerCube.activeSelf)
        {
            Vector3 currentRotation = timerCube.transform.rotation.eulerAngles;
            timerCube.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + 90 * Time.deltaTime, currentRotation.z);
        }
    }
}
