using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OrderUI : MonoBehaviour
{
    public Guid id;
    public TMP_Text tableNumber;
    public TMP_Text orderName;
    public TMP_Text ingredients;
    public TMP_Text reward;
    public TMP_Text timer;

    private float currentTime; // timer tracking
    private bool started;
    private Recipe order;

    void Start()
    {
    }

    void Update()
    {
        // update the timer
        if (started)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                started = false;
                OrderSystem orderSystem = FindObjectOfType<OrderSystem>();
                orderSystem.RemoveOrder(order);
                NPCManager npcManager = FindObjectOfType<NPCManager>();
                if (npcManager != null)
                {
                    npcManager.RemoveCustomer();
                }
            }
            timer.text = timerToText(currentTime);
        }

    }

    public GameObject CreateOrderPanel(Recipe recipe, int tableNum)
    {
        this.id = recipe.id;

        orderName.text = recipe.recipeName;

        string ingredientsStr = "";
        foreach (Ingredient ing in recipe.ingredients)
        {
            ingredientsStr += ing;
        }
        ingredients.text = ingredientsStr;

        reward.text = recipe.reward.ToString();
        tableNumber.text = $"Table {tableNum}";

        currentTime = recipe.time;
        timer.text = timerToText(currentTime); // fixed timer, 60s
        started = true;
        order = recipe;

        return this.gameObject;
    }

    string timerToText(float initialSeconds)
    {
        int minutes = Mathf.FloorToInt(initialSeconds / 60);
        int seconds = Mathf.FloorToInt(initialSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
