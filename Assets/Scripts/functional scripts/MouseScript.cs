using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public static void HideMouse()
    {
        Cursor.visible = false;
    }

    public static void ShowMouse()
    {
        Cursor.visible = true;
    }
}
