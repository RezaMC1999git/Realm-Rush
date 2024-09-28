using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 4;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] AudioClip playerDamageSFX;
    private void Start()
    {
        healthText.text = health.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        health -= healthDecrease;
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        healthText.text = health.ToString();
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);
    }
}
