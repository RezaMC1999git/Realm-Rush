using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()
    {
        SnapToGrid();
        UpdateLable();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        
        transform.position = new Vector3(waypoint.GetGridPos().x * gridSize, 0f, waypoint.GetGridPos().y * gridSize);
    }

    private void UpdateLable()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string lableText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textMesh.text = lableText;
        gameObject.name = lableText;
    }
}
