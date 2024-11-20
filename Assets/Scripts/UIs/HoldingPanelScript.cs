using CulinaryChaos.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class HoldingPanelScript : MonoBehaviour
{
    private static GameObject HoldingPanel;
    private static Image holdingImage;

    void Awake()
    {
        HoldingPanel = transform.gameObject;
        HoldingPanel.SetActive(false);
        holdingImage = transform.Find("IngredientSprite").GetComponent<Image>();
        holdingImage.sprite = null;
    }

    public static void SetHoldingImage(Object holdingObject)
    {
        if (holdingObject == null)
        {
            holdingImage.sprite = null;
            HideHoldingPanel();
            return;
        }

        if (holdingObject is IngredientProps)
        {
            IngredientProps ingredientProps = (IngredientProps)holdingObject;
            holdingImage.sprite = OrderUI.GetIngredientSprite(ingredientProps.ingredientType);
        }

        ShowHoldingPanel();
    }

    public static void ShowHoldingPanel()
    {
        HoldingPanel.SetActive(true);
    }

    public static void HideHoldingPanel()
    {
        HoldingPanel.SetActive(false);
    }
}
