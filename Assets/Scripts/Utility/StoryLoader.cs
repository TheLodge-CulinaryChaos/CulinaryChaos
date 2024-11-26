using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoader : MonoBehaviour
{
    // Initializations
    private CanvasGroup CGbackstory;
    public CoinsToWin coinsToWin;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        CGbackstory = GetComponent<CanvasGroup>();
        if (CGbackstory == null)
        {
            Debug.LogError("CGbackstory not in inspector");
        }
        CGbackstory.interactable = true;
        CGbackstory.blocksRaycasts = true;
        CGbackstory.alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startLevel()
    {
        CGbackstory.interactable = false;
        CGbackstory.blocksRaycasts = false;
        CGbackstory.alpha = 0f;
        // turn on UI command
        coinsToWin.ShowCoinsToWin();
    }
}
