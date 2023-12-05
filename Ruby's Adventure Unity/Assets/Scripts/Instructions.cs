//Coded by Alana Ackley
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public Canvas canvas; // Assign the canvas containing the UI elements in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleChildrenVisibility(canvas);
        }
    }

    void ToggleChildrenVisibility(Canvas canvas)
    {
        foreach (Transform child in canvas.transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }
}