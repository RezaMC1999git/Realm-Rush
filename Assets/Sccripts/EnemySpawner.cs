using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpanws = 5f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform parent;
    [SerializeField] AudioClip spawnedEnemySFX;

    private void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
    }
    IEnumerator RepeatedlySpawnEnemies()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
        EnemyMovement enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.gameObject.transform.parent = parent;
        yield return new WaitForSeconds(secondsBetweenSpanws);
        StartCoroutine(RepeatedlySpawnEnemies());
    }

}
