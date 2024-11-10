using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickerImageClickHandler : MonoBehaviour
{

public GameObject player;

    // player rotate around its Y axis
    private float rotateSpeed = 100f;
    public Enums playerType;
    public GameObject indicator;

    void Awake() {
        indicator.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        player.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (PlayerSelector.selectedPlayer == playerType)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void OnMouseUp() {
        PlayerSelector.selectedPlayer = playerType;
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        sceneSwitcher.GeneralMenu();
    }
}
