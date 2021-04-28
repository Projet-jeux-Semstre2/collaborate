using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Camera camera;

    public static bool cursorLock;
    public static bool canLock;
    
    void CursorMode()
    {
        Vector3 view = camera.ScreenToViewportPoint(Input.mousePosition);
        bool isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        
        if (Input.GetKeyDown(KeyCode.Escape) && cursorLock)
        {
            cursorLock = false;
        }
        
        if (Input.GetMouseButtonDown(0) && !isOutside && !cursorLock && canLock)
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        CursorMode();
    }
}
