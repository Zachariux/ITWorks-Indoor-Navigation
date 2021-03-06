﻿using System.Collections;
using UnityEngine;

class DragDrop : MonoBehaviour
{
    private Color mouseOverColor = Color.black;
    private Color originalColor = Color.white;
    public DropDown DropDownScript;
    private bool dragging = false;
    private float distance;
    public Camera mainCamera;
    public LineRenderer path;

    void OnMouseEnter()
    {
      
        gameObject.GetComponent<SpriteRenderer>().color = mouseOverColor;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor; 
    }

    void OnMouseDown()
    {
        gameObject.GetComponent<SpriteRenderer>().color = mouseOverColor;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        mainCamera.GetComponent<MoveCamera>().moveEnabled = false;
        path.enabled = false;
        DropDownScript.ResetDropDowns();

    }

    void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        dragging = false;
        mainCamera.GetComponent<MoveCamera>().moveEnabled = true;
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}