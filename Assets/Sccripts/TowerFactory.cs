using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimits = 3;
    [SerializeField] GameObject towerParnetTransform;
    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        int numTower = towerQueue.Count;

        if (numTower < towerLimits)
            InstantiateNewTower(baseWaypoint);
        else
            MoveExistingTower(baseWaypoint);
    }

    void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParnetTransform.transform;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlacable = false;
        towerQueue.Enqueue(newTower);
    }
    void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlacable = true;
        newBaseWaypoint.isPlacable = false;
        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        towerQueue.Enqueue(oldTower);
    }
}
