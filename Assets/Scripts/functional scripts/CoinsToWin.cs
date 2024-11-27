using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsToWin : MonoBehaviour
{
    private float PopUpLength;
    private bool PopUpDone;
    public GameObject coinsToWinPanel;
    public int level;
    public TMP_Text levelText;
    public TMP_Text coinsToWinText;

    // Start is called before the first frame update
    void Start()
    {
        PopUpLength = 3.0f;
        PopUpDone = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowCoinsToWin()
    {
        levelText.text = $"Level {level}";
        coinsToWinText.text = $"{Coins.levelsToCoins[level]} to win!";
        coinsToWinPanel.SetActive(true);
        StartCoroutine(HidePopupAfterTime());
    }

    IEnumerator HidePopupAfterTime()
    {
        yield return new WaitForSecondsRealtime(PopUpLength);
        coinsToWinPanel.SetActive(false);
        Time.timeScale = 1f;
        MouseScript.HideMouse();
    }
}
