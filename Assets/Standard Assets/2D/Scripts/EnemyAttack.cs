﻿using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerHealth.TakeDamage(attackDamage);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // If the player has zero or less health...
        if (playerHealth.currentCoin <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
        }

        if (playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Reset the timer.
        timer = 0f;
       
        // If the player has health to lose...
        if (playerHealth.currentCoin > 0)
        {
            print("Attack is triggerd!");
            // ... damage the player.
            //playerHealth.TakeDamage(attackDamage);
        }
    }
}