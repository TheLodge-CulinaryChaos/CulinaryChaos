using System;
using System.Collections;
using System.Collections.Generic;
using CulinaryChaos.Objects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PickUpController : MonoBehaviour
{
    private AnimatorManager animatorManager;

    private Vector3 boxCastSize = new Vector3(1.5f, 2f, 0.1f);
    private Vector3 extPos = new Vector3(0, 1.2f, 0);

    #region Pick up Object Variables
    public Transform holdPosition;
    private GameObject pickUpObject = null;
    public bool isHoldingIngredients = false;
    private Vector3 originalScale;
    #endregion

    #region Cooking Component, Dining Order
    private CookingComponent cookingComponent;
    private DiningOrderScript diningOrderScript;
    #endregion

    #region Holding Object like Bowl on the Player when pick up cooked food
    public GameObject HoldingObject;
    #endregion

    #region LayerMask of all objects the player can interact with for Cooking Process
    public LayerMask cookingLayerMask;
    private float maxDistanceCookingLM = 2.5f;
    RaycastHit hit;
    #endregion

    #region Suggestion Text

    TMP_Text suggestionText;

    #endregion

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();

        suggestionText = GameObject.Find("SuggestionText").GetComponent<TMP_Text>();
    }

    public void HandleAllStates()
    {
        RaycastHit hit = PerformBoxCast();

        if (hit.collider == null)
        {
            if (isHoldingIngredients)
            {
                DropObject();
            }
            return;
        }
        HandleBoxCastOnObjects(hit);
    }

    public void HandleDetection()
    {
        if (suggestionText == null)
            return;

        RaycastHit hit = PerformBoxCast();

        if (hit.collider == null)
        {
            suggestionText.text = "";
            return;
        }

        GameObject gameObject = hit.collider.gameObject;
        if (
            gameObject.tag == GameTags.INGREDIENT
            || gameObject.tag == GameTags.COOKING_COMPONENT
            || gameObject.tag == GameTags.BOWLS
            || gameObject.tag == GameTags.THE_SINK
            || gameObject.tag == GameTags.ORDER_OBJECT
        )
        {
            suggestionText.text = "Q";
        }
        else
        {
            suggestionText.text = "";
        }
    }

    #region Handle Box Cast on Objects
    private void HandleBoxCastOnObjects(RaycastHit hit)
    {
        GameObject gameObject = hit.collider.gameObject;
        Debug.Log("Hit" + gameObject.tag);

        switch (gameObject.tag)
        {
            case GameTags.COOKING_COMPONENT:
                cookingComponent = gameObject.GetComponent<CookingComponent>();
                HandleCookingComponent();
                break;
            case GameTags.BOWLS:
                GrabABowl();
                break;
            case GameTags.THE_SINK:
                DisposeOfBowl();
                break;
            case GameTags.ORDER_OBJECT:
                diningOrderScript = gameObject.GetComponent<DiningOrderScript>();
                ServeFood();
                break;
            case GameTags.INGREDIENT:
                if (isHoldingIngredients)
                {
                    DropObject();
                    break;
                }
                animatorManager.PlayTargetAnimation("Pick Fruit", false);
                pickUpObject = gameObject;
                PickUpObject();
                break;
            default:
                DropObject();
                break;
        }
    }

    private RaycastHit PerformBoxCast()
    {
        Vector3 position = transform.position + extPos;
        Physics.BoxCast(
            position,
            boxCastSize,
            transform.forward,
            out hit,
            transform.rotation,
            maxDistanceCookingLM,
            cookingLayerMask
        );
        return hit;
    }

    #endregion

    #region Handle Pick Up and Drop Ingredient
    void PickUpObject()
    {
        if (pickUpObject == null)
            return;

        IngredientProps ingredientType = pickUpObject.GetComponent<IngredientProps>();

        Rigidbody rb = pickUpObject.GetComponent<Rigidbody>();

        Collider objectCollider = pickUpObject.GetComponent<Collider>();
        Collider playerCollider = GetComponent<Collider>();
        if (objectCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(objectCollider, playerCollider, true);
        }

        rb.isKinematic = true;
        rb.useGravity = false;

        // disable all colliders
        Collider[] colliders = pickUpObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        pickUpObject.transform.SetParent(holdPosition, true);
        pickUpObject.transform.localPosition = Vector3.zero;
        pickUpObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

        originalScale = pickUpObject.transform.lossyScale;
        pickUpObject.transform.localScale *= 0.5f;

        // set image panel
        HoldingPanelScript.SetHoldingImage(ingredientType);

        isHoldingIngredients = true;
    }

    public GameObject DropObject()
    {
        if (pickUpObject == null)
            return null;

        pickUpObject.transform.SetParent(null);
        Rigidbody rb = pickUpObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            // enable all colliders
            Collider[] colliders = pickUpObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }

            rb.AddForce(transform.forward * 2f, ForceMode.Impulse); // Add a little force to the object
        }
        pickUpObject.transform.position =
            transform.position + transform.forward * 1f + transform.up * 1f;
        pickUpObject.transform.localScale = originalScale;

        GameObject temp = pickUpObject;
        pickUpObject = null;

        // hide image panel
        HoldingPanelScript.HideHoldingPanel();

        isHoldingIngredients = false;
        return temp;
    }

    #endregion

    #region Handle Pick up Bowl and Drop Bowl in sink
    private void GrabABowl()
    {
        if (HoldingObject == null)
            return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null)
            return;

        holdingObjectScript.GrabABowl();
        animatorManager.animator.SetBool("isHoldingPlate", true);
    }

    public void DisposeOfBowl()
    {
        if (HoldingObject == null)
            return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null)
            return;

        if (holdingObjectScript.IsHoldingPlate())
        {
            holdingObjectScript.DisposeOfBowl();
            animatorManager.animator.SetBool("isHoldingPlate", false);
            HoldingPanelScript.HideHoldingPanel();
        }
    }
    #endregion

    #region Handle Ingredient -> CookingComponent -> HoldingObject -> OrderObject

    private void HandleCookingComponent()
    {
        if (HoldingObject != null && cookingComponent != null)
        {
            TransferCookedFoodIntoHoldingObject();
        }
        TransferIngredientIntoCookingComponent();
    }

    private void TransferIngredientIntoCookingComponent()
    {
        if (pickUpObject == null)
            return;

        if (cookingComponent == null)
            return;

        if (cookingComponent.CanAcceptIngredient(pickUpObject))
        {
            cookingComponent.CookIngredient(pickUpObject);
            HoldingPanelScript.HideHoldingPanel();
            pickUpObject.transform.SetParent(null);
            Destroy(pickUpObject);
            isHoldingIngredients = false;
        }
    }

    private void TransferCookedFoodIntoHoldingObject()
    {
        if (HoldingObject == null)
            return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();

        if (cookingComponent == null)
            return;

        if (
            cookingComponent.isFoodReadyToServe()
            && !holdingObjectScript.IsFoodInPlate()
            && holdingObjectScript.IsHoldingPlate()
        )
        {
            cookingComponent.RemoveFoodFromPot();
            cookingComponent.ResetTimer();
            holdingObjectScript.PutFoodInPlate(cookingComponent.ingredientProps);
            HoldingPanelScript.SetHoldingImage(cookingComponent.ingredientProps);
        }
    }

    private void ServeFood()
    {
        if (HoldingObject == null)
            return;

        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null)
            return;

        if (diningOrderScript == null)
            return;
        if (
            holdingObjectScript.IsFoodInPlate()
            && diningOrderScript.IsOrderAccepted(holdingObjectScript.ingredientProps)
        )
        {
            diningOrderScript.CompleteOrder(holdingObjectScript.ingredientProps);
            holdingObjectScript.DisposeOfBowl();
            animatorManager.animator.SetBool("isHoldingPlate", false);
            HoldingPanelScript.HideHoldingPanel();
        }
    }

    #endregion

    #region Debug BoxCast
    void OnDrawGizmos()
    {
        Vector3 position = transform.position + extPos;
        bool isHit = Physics.BoxCast(
            position,
            boxCastSize,
            transform.forward,
            out hit,
            transform.rotation,
            maxDistanceCookingLM,
            cookingLayerMask
        );
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(position + transform.forward * hit.distance, boxCastSize);
            Debug.Log(hit.collider.gameObject);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, transform.forward * maxDistanceCookingLM);
        }
    }

    #endregion
}
