using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    bool isRunning = true;
    Waypoint searchCenter;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    Vector2Int[] directions = {
        Vector2Int.up,Vector2Int.down,Vector2Int.left,Vector2Int.right
    };
    void CreatePath()
    {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }

    void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlacable = false;
    }
    void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());

            if (isOverlapping)
                Debug.LogError("Overlapping");
            else
                grid.Add(waypoint.GetGridPos(), waypoint);
        }
        print(grid.Count);
    }
    void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }    
    }

    void ExploreNeighbours()
    {
        if(!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
        }
    }

    void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {

        }
        else
        {
            //neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }
}
