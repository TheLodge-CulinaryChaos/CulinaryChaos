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

    #region Pick up Object Variables
    public Transform holdPosition;
    private GameObject pickUpObject = null;
    private bool isHolding = false;
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
    public float maxDistanceCookingLM = 1f;
    RaycastHit hit;
    #endregion

    public TMP_Text indicatorText;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleAllStates()
    {
        RaycastHit hit = PerformBoxCast();

        if (hit.collider == null)
        {
            if (isHolding)
            {
                DropObject();
            }
            return;
        }
        HandleBoxCastOnObjects(hit);

    }

    #region Handle Box Cast on Objects
    private void HandleBoxCastOnObjects(RaycastHit hit)
    {
        GameObject gameObject = hit.collider.gameObject;

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
        Vector3 position = transform.position + new Vector3(0, 2, 0);
        Physics.BoxCast(position, transform.lossyScale / 2, transform.forward, out hit,
            transform.rotation, maxDistanceCookingLM, cookingLayerMask);
        return hit;
    }

    #endregion

    #region Handle Pick Up and Drop Ingredient
    void PickUpObject()
    {
        if (pickUpObject == null) return;


        Rigidbody rb = pickUpObject.GetComponent<Rigidbody>();

        Collider objectCollider = pickUpObject.GetComponent<Collider>();
        Collider playerCollider = GetComponent<Collider>();
        if (objectCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(objectCollider, playerCollider, true);
        }

        rb.isKinematic = true;
        rb.useGravity = false;

        pickUpObject.transform.SetParent(holdPosition, true);
        pickUpObject.transform.localPosition = Vector3.zero;
        pickUpObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

        originalScale = pickUpObject.transform.lossyScale;
        pickUpObject.transform.localScale = Vector3.one;

        isHolding = true;

    }

    void DropObject()
    {
        if (pickUpObject == null) return;

        pickUpObject.transform.SetParent(null);
        Rigidbody rb = pickUpObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse); // Add a little force to the object
        }
        pickUpObject.transform.position = transform.position + transform.forward * 1f + transform.up * 1f;
        pickUpObject.transform.localScale = originalScale;

        pickUpObject = null;
        isHolding = false;
    }
    #endregion

    #region Handle Pick up Bowl and Drop Bowl in sink
    private void GrabABowl()
    {
        if (HoldingObject == null) return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null) return;

        holdingObjectScript.GrabABowl();
        animatorManager.animator.SetBool("isHoldingPlate", true);
    }

    private void DisposeOfBowl()
    {
        if (HoldingObject == null) return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null) return;

        if (holdingObjectScript.IsHoldingPlate())
        {
            holdingObjectScript.DisposeOfBowl();
            animatorManager.animator.SetBool("isHoldingPlate", false);
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
        if (pickUpObject == null) return;

        if (cookingComponent == null) return;

        if (cookingComponent.CanAcceptIngredient(pickUpObject))
        {
            cookingComponent.CookIngredient(pickUpObject);

            pickUpObject.transform.SetParent(null);
            Destroy(pickUpObject);
            isHolding = false;
        }
    }

    private void TransferCookedFoodIntoHoldingObject()
    {
        if (HoldingObject == null) return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();

        if (cookingComponent == null) return;

        if (cookingComponent.isFoodReadyToServe() && !holdingObjectScript.IsFoodInPlate() && holdingObjectScript.IsHoldingPlate())
        {
            cookingComponent.RemoveFoodFromPot();
            cookingComponent.ResetTimer();
            holdingObjectScript.PutFoodInPlate(cookingComponent.ingredientProps);
        }
    }

    private void ServeFood()
    {

        if (HoldingObject == null) return;

        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null) return;

        if (diningOrderScript == null) return;
        if (holdingObjectScript.IsFoodInPlate()
        && diningOrderScript.IsOrderAccepted(holdingObjectScript.ingredientProps))
        {
            diningOrderScript.CompleteOrder(holdingObjectScript.ingredientProps);
            holdingObjectScript.DisposeOfBowl();
            animatorManager.animator.SetBool("isHoldingPlate", false);
        }
    }

    #endregion

    #region Debug BoxCast
    void OnDrawGizmos()
    {
        Vector3 position = transform.position + new Vector3(0, 2, 0);
        bool isHit = Physics.BoxCast(position, transform.lossyScale / 2, transform.forward, out hit,
            transform.rotation, maxDistanceCookingLM, cookingLayerMask);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(position + transform.forward * hit.distance, transform.lossyScale);
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


