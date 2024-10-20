using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PickUpController : MonoBehaviour
{
    public float pickUpRange = 2f;
    public float transferIngredientRange = 5f;

    public Transform holdPosition;
    private GameObject pickUpObject = null;
    private bool isHolding = false;
    private AnimatorManager animatorManager;
    private Vector3 originalScale;

    private CookingComponent cookingComponent;

    public GameObject HoldingObject;
    private Boolean canGrabBowl;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleAllStates()
    {
        if (canGrabBowl)
        {
            GrabABowl();
        }
        if (isHolding)
        {
            DropObject();
        }
        else
        {
            animatorManager.PlayTargetAnimation("Pick Fruit", false);
        }
    }

    private void GrabABowl()
    {
        if (HoldingObject == null) return;
        HoldingObjectScript holdingObjectScript = HoldingObject.GetComponent<HoldingObjectScript>();
        if (holdingObjectScript == null) return;
        Debug.Log("Have a holding script");
        holdingObjectScript.GrabABowl();
        animatorManager.animator.SetBool("isHoldingPlate", true);
    }

    public void TryPickUpObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ingredient"))
            {
                pickUpObject = collider.gameObject;
                PickUpObject();
                break;
            }
        }
    }

    void PickUpObject()
    {
        if (pickUpObject != null)
        {
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
    }

    void DropObject()
    {
        // if there is nothing, do nothing
        if (pickUpObject == null) return;

        if (TransferIngredientIntoCookingComponent())
        {
            return;
        }

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

    Boolean TransferIngredientIntoCookingComponent()
    {
        if (pickUpObject == null) return false;

        if (cookingComponent == null) return false;

        if (cookingComponent.CanAcceptIngredient(pickUpObject))
        {
            cookingComponent.CookIngredient(pickUpObject);

            pickUpObject.transform.SetParent(null);
            Destroy(pickUpObject);
            isHolding = false;
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingComponent"))
        {
            cookingComponent = other.GetComponent<CookingComponent>();
        }
        if (other.CompareTag("Bowls"))
        {
            canGrabBowl = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingComponent"))
        {
            cookingComponent = null;
        }
        if (other.CompareTag("Bowls"))
        {
            canGrabBowl = false;
        }
    }

}
