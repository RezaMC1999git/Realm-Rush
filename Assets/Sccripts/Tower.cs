using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectilrParticle;
    public Waypoint baseWaypoint;
    Transform targetEnemy;
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
            Shoot(false);
    }

    void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }
        Transform closestEnemy = sceneEnemies[0].transform;
        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    Transform GetClosest(Transform TransformA, Transform TransformB)
    {
        var distanceToA = Vector3.Distance(transform.position, TransformA.position);
        var distanceToB = Vector3.Distance(transform.position, TransformB.position);
        if (distanceToA < distanceToB)
            return TransformA;
        return TransformB;
    }

    void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position,gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
            Shoot(true);
        else
            Shoot(false);
    }
    void Shoot(bool isActive)
    {
        var emmisionModule = projectilrParticle.emission;
        emmisionModule.enabled = isActive;
    }
}
