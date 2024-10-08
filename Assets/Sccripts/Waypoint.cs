using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;
    public bool isExplored = false;
    public bool isPlacable = true;
    public Waypoint exploredFrom;
    Vector2Int gridPos;
    public int GetGridSize()
    {
        return gridSize;
    }
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
               Mathf.RoundToInt(transform.position.x / gridSize),
               Mathf.RoundToInt(transform.position.z / gridSize)
               );
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlacable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
