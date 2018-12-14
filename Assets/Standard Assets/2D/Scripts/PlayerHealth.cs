using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingCoin = 0;                            // The amount of Coin the player starts the game with.
    public int currentCoin;                                   // The current Coin the player has.
    public Text Coin;                                       // Reference to the UI's Coin.



    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    Player playerMovement;                              // Reference to the player's movement.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<Player>();

        // Set the initial health of the player.
        if (GameManager.instance)
        {
            currentCoin = GameManager.instance.playerCoinPoints;
        }
        else
        {
            currentCoin = 0;
        }
        
    }


    void Update()
    {
      
    }


    public void TakeDamage(int amount)
    {

        print("Take Damage is triggerd!");
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentCoin -= amount;



        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentCoin <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
    }
}