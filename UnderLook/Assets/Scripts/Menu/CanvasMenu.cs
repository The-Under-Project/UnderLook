using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}

