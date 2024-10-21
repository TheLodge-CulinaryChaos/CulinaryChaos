using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjectScript : MonoBehaviour
{

    public GameObject holdingObject;
    public GameObject cookedObject;
    public IngredientProps ingredientProps;

    // Start is called before the first frame update
    void Awake()
    {
        holdingObject.SetActive(false);
        cookedObject.SetActive(false);
    }

    #region status of the holding object
    internal bool IsHoldingPlate()
    {
        return holdingObject.activeSelf;
    }

    internal bool IsFoodInPlate()
    {
        return cookedObject.activeSelf;
    }
    #endregion

    #region Actions on Holding object
    internal void GrabABowl()
    {
        holdingObject.SetActive(true);
        cookedObject.SetActive(false);
    }

    internal void DisposeOfBowl()
    {
        holdingObject.SetActive(false);
        cookedObject.SetActive(false);
    }

    internal void PutFoodInPlate(IngredientProps ingredientProps)
    {
        this.ingredientProps = ingredientProps;
        holdingObject.SetActive(true);
        cookedObject.SetActive(true);
        cookedObject.GetComponent<Renderer>().material = ingredientProps.cookingMaterial;
    }
    #endregion


}
