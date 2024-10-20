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

    // textmeshpro timer
    public TMP_Text timer;


    private float countdownTime = 5f;

    void Awake()
    {
        cookingFood.SetActive(false);
        timer.SetText("");
    }

    internal bool CanAcceptIngredient(GameObject pickUpObject)
    {
        var ingredientComponent = GetIngredientProperties(pickUpObject);
        if (ingredientComponent == null) return false;

        if (cookingFood.activeSelf)
        {
            return false;
        }

        var ingredientType = ingredientComponent.ingredientType;

        return ingredientType == IngredientEnum.Tomato ||
                ingredientType == IngredientEnum.Pumpkin ||
                ingredientType == IngredientEnum.Mushroom;

    }

    internal void CookIngredient(GameObject pickUpObject)
    {
        var ingredientComponent = GetIngredientProperties(pickUpObject);
        if (ingredientComponent == null) return;

        var cookingMaterial = ingredientComponent.cookingMaterial;
        cookingFood.GetComponent<Renderer>().material = cookingMaterial;

        cookingFood.SetActive(true);
        pot.SetActive(true);

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
            } else {
                timer.SetText(displayTime.ToString());
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartCookingTimer());
    }
}
