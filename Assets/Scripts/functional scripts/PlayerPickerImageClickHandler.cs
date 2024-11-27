using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickerImageClickHandler : MonoBehaviour
{
    public GameObject player;

    // player rotate around its Y axis
    private float rotateSpeed = 80f;
    public Enums playerType;
    public GameObject indicator;

    void Awake()
    {
        indicator.SetActive(false);
    }

    void LateUpdate()
    {
        if (PlayerSelector.selectedPlayer == playerType)
        {
            indicator.SetActive(true);
            player.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else
        {
            indicator.SetActive(false);
            player.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public void SetPlayer() {
        Debug.Log("PlayerPickerImageClickHandler.OnMouseUp");

        PlayerSelector.selectedPlayer = playerType;
    }
}
