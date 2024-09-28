using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        deathParticlePrefab.Play();
        AudioSource.PlayClipAtPoint(enemyDeathSFX,transform.position);
        Destroy(gameObject);    
    }

    void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        audioSource.PlayOneShot(enemyHitSFX);
        hitParticlePrefab.Play();
    }
}
